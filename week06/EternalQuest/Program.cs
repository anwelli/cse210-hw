using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

// Base class for all goals
[Serializable]
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public string Type => GetType().Name;

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public abstract void RecordEvent();
}

// Class for simple goals
[Serializable]
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded event for {Name}. Earned {Points} points.");
    }
}

// Class for eternal goals
[Serializable]
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded event for {Name}. Earned {Points} points.");
    }
}

// Class for checklist goals
[Serializable]
public class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }

    public ChecklistGoal(string name, int points, int targetCount) : base(name, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
    }

    public override void RecordEvent()
    {
        CurrentCount++;
        Console.WriteLine($"Recorded event for {Name}. Earned {Points} points. ({CurrentCount}/{TargetCount})");
        if (CurrentCount >= TargetCount)
        {
            Console.WriteLine($"Completed {Name}! Earned bonus points.");
        }
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalPoints = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("------------------------");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record an event for a goal");
            Console.WriteLine("3. Display all goals");
            Console.WriteLine("4. View total points");
            Console.WriteLine("5. List goals");
            Console.WriteLine("6. Save goals");
            Console.WriteLine("7. Load goals");
            Console.WriteLine("8. Exit program");
            Console.Write("Choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    CreateNewGoal();
                    break;
                case 2:
                    RecordEventForGoal();
                    break;
                case 3:
                    DisplayAllGoals();
                    break;
                case 4:
                    ViewTotalPoints();
                    break;
                case 5:
                    ListGoals();
                    break;
                case 6:
                    SaveGoals();
                    break;
                case 7:
                    LoadGoals();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }

    static void CreateNewGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal points: ");
        int points = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Choose goal type:");
        Console.WriteLine("1. Simple goal");
        Console.WriteLine("2. Eternal goal");
        Console.WriteLine("3. Checklist goal");
        Console.Write("Choose an option: ");
        int goalType = Convert.ToInt32(Console.ReadLine());

        Goal goal = null;

        switch (goalType)
        {
            case 1:
                goal = new SimpleGoal(name, points);
                break;
            case 2:
                goal = new EternalGoal(name, points);
                break;
            case 3:
                Console.Write("Enter target count: ");
                int targetCount = Convert.ToInt32(Console.ReadLine());
                goal = new ChecklistGoal(name, points, targetCount);
                break;
            default:
                Console.WriteLine("Invalid option. Goal not created.");
                return;
        }

        goals.Add(goal);
        Console.WriteLine("Goal created successfully!");
    }

    static void RecordEventForGoal()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals available. Please create a goal first.");
            return;
        }

        Console.WriteLine("Choose a goal to record an event:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        Console.Write("Choose an option: ");
        int goalIndex = Convert.ToInt32(Console.ReadLine()) - 1;

        if (goalIndex < 0 || goalIndex >= goals.Count)
        {
            Console.WriteLine("Invalid option. Event not recorded.");
            return;
        }

        goals[goalIndex].RecordEvent();
        totalPoints += goals[goalIndex].Points;
        Console.WriteLine($"Total points: {totalPoints}");
    }

    static void DisplayAllGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals available. Please create a goal first.");
            return;
        }

        Console.WriteLine("All Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name} ({goals[i].Points} points)");
        }
    }

    static void ViewTotalPoints()
    {
        Console.WriteLine($"Total points: {totalPoints}");
    }

    static void ListGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals available. Please create a goal first.");
            return;
        }

        Console.WriteLine("Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
    }

    static void SaveGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals available. Please create a goal first.");
            return;
        }

        string jsonString = JsonSerializer.Serialize(goals);
        File.WriteAllText("goals.json", jsonString);

        Console.WriteLine("Goals saved successfully!");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.json"))
        {
            string jsonString = File.ReadAllText("goals.json");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new GoalConverter());
            goals = JsonSerializer.Deserialize<List<Goal>>(jsonString, options);

            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}

public class GoalConverter : JsonConverter<Goal>
{
    public override Goal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;
            string type = root.GetProperty("Type").GetString();

            switch (type)
            {
                case "SimpleGoal":
                    return JsonSerializer.Deserialize<SimpleGoal>(root.GetRawText(), options);
                case "EternalGoal":
                    return JsonSerializer.Deserialize<EternalGoal>(root.GetRawText(), options);
                case "ChecklistGoal":
                    return JsonSerializer.Deserialize<ChecklistGoal>(root.GetRawText(), options);
                default:
                    throw new NotSupportedException($"Goal type '{type}' is not supported.");
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, Goal value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}