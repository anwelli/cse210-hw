
using System;
using System.Collections.Generic;

// Base class for all activities
public abstract class Activity 
{
    private DateTime date;
    private int minutes;

    public Activity(DateTime date, int minutes) 
    {
        this.date = date;
        this.minutes = minutes;
    }

    public int Minutes 
    {
        get { return minutes; }
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary() 
    {
        return $"{date.ToString("dd MMM yyyy")} {GetType().Name} ({minutes} min): Distance {GetDistance():F2} km, Speed {GetSpeed():F2} kph, Pace: {GetPace():F2} min per km";
    }
}

// Class for running activities
public class Running : Activity 
{
    private double distance;

    public Running(DateTime date, int minutes, double distance) : base(date, minutes) 
    {
        this.distance = distance;
    }

    public override double GetDistance() 
    {
        return distance;
    }

    public override double GetSpeed() 
    {
        return distance / Minutes * 60;
    }

    public override double GetPace() 
    {
        return Minutes / distance;
    }

    public override string GetSummary() 
    {
        return base.GetSummary();
    }
}

// Class for cycling activities
public class Cycling : Activity 
{
    private double speed;

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes) 
    {
        this.speed = speed;
    }

    public override double GetDistance() 
    {
        return speed / 60 * Minutes;
    }

    public override double GetSpeed() 
    {
        return speed;
    }

    public override double GetPace() 
    {
        return 60 / speed;
    }

    public override string GetSummary() 
    {
        return base.GetSummary();
    }
}

// Class for swimming activities
public class Swimming : Activity 
{
    private int laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes) 
    {
        this.laps = laps;
    }

    public override double GetDistance() 
    {
        return laps * 50 / 1000.0;
    }

    public override double GetSpeed() 
    {
        return GetDistance() / Minutes * 60;
    }

    public override double GetPace() 
    {
        return Minutes / GetDistance();
    }

    public override string GetSummary() 
    {
        return base.GetSummary();
    }
}

class Program 
{
    static void Main(string[] args) 
    {
        List<Activity> activities = new List<Activity>();

        while (true) 
        {
            Console.WriteLine("Exercise Tracking Program");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Running");
            Console.WriteLine("2. Cycling");
            Console.WriteLine("3. Swimming");
            Console.WriteLine("4. View Activities");
            Console.WriteLine("5. Exit");

            Console.Write("Choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option) 
            {
                case 1:
                    AddRunningActivity(activities);
                    break;
                case 2:
                    AddCyclingActivity(activities);
                    break;
                case 3:
                    AddSwimmingActivity(activities);
                    break;
                case 4:
                    ViewActivities(activities);
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }

    static void AddRunningActivity(List<Activity> activities) 
    {
        Console.Write("Enter date (dd MMM yyyy): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter minutes: ");
        int minutes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter distance (km): ");
        double distance = Convert.ToDouble(Console.ReadLine());

        activities.Add(new Running(date, minutes, distance));
        Console.WriteLine("Running activity added successfully!");
    }

    static void AddCyclingActivity(List<Activity> activities) 
    {
        Console.Write("Enter date (dd MMM yyyy): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter minutes: ");
        int minutes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter speed (kph): ");
        double speed = Convert.ToDouble(Console.ReadLine());

        activities.Add(new Cycling(date, minutes, speed));
        Console.WriteLine("Cycling activity added successfully!");
    }


    static void AddSwimmingActivity(List<Activity> activities)
    {
        Console.Write("Enter date (dd MMM yyyy): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter minutes: ");
        int minutes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter laps: ");
        int laps = Convert.ToInt32(Console.ReadLine());

        activities.Add(new Swimming(date, minutes, laps));
        Console.WriteLine("Swimming activity added successfully!");
    }

    static void ViewActivities(List<Activity> activities)
    {
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

