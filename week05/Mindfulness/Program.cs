

using System;
using System.Threading;

namespace MindfulnessProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mindfulness Program!");
            Console.WriteLine("-------------------------------------");

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Start Breathing Activity");
                Console.WriteLine("2. Start Reflection Activity");
                Console.WriteLine("3. Start Listening Activity");
                Console.WriteLine("4. End Program");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        BreathingActivity();
                        break;
                    case 2:
                        ReflectionActivity();
                        break;
                    case 3:
                        ListeningActivity();
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void BreathingActivity()
        {
            Console.WriteLine("Welcome to Breathing Activity!");
            Console.Write("Enter duration (in seconds): ");
            int duration = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < duration; i++)
            {
                Console.WriteLine("Breathe in...");
                Thread.Sleep(1000);
                Console.WriteLine("Breathe out...");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Activity completed!");
            Console.Write("Do you want to continue? (yes/no): ");
            string response = Console.ReadLine();
            if (response.ToLower() != "yes")
            {
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
            }
        }

        static void ReflectionActivity()
{
    Console.WriteLine("Welcome to Reflection Activity!");
    Console.Write("Enter duration (in seconds): ");
    int duration = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Get ready...");
    Thread.Sleep(2000);

    string[] prompts = new string[]
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    string[] questions = new string[]
    {
        "What did you learn from this experience?",
        "How did you feel during this experience?",
        "What would you do differently if faced with a similar situation?",
        "How did this experience impact your life?"
    };

    int timeElapsed = 0;
    Random random = new Random();

    while (timeElapsed < duration)
    {
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Console.WriteLine("Consider this prompt for a moment...");

        Thread.Sleep(5000); // wait for 5 seconds

        Console.WriteLine(questions[random.Next(questions.Length)]);
        Console.Write("Enter your response (or type 'skip' to skip): ");
        string response = Console.ReadLine();

        if (response.ToLower() == "skip")
        {
            continue;
        }

        timeElapsed += 10; // increment time elapsed by 10 seconds
    }

    Console.WriteLine("Activity completed!");
    Console.Write("Do you want to continue? (yes/no): ");
    string contResponse = Console.ReadLine();
    if (contResponse.ToLower() != "yes")
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }
}



static void ListeningActivity()
{
    Console.WriteLine("Welcome to Listening Activity!");
    Console.Write("Enter duration (in seconds): ");
    int duration = Convert.ToInt32(Console.ReadLine());

    string[] prompts = new string[]
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>();

    int timeElapsed = 0;
    int currentPrompt = 0;

    while (timeElapsed < duration)
    {
        Console.WriteLine(prompts[currentPrompt]);
        Console.Write("Enter your response (or type 'next' to move to the next question): ");
        string response = Console.ReadLine();

        if (response.ToLower() == "next")
        {
            currentPrompt++;
            if (currentPrompt >= prompts.Length)
            {
                currentPrompt = 0;
            }
            timeElapsed += 5; // increment time elapsed by 5 seconds
            continue;
        }

        if (!responses.ContainsKey(prompts[currentPrompt]))
        {
            responses.Add(prompts[currentPrompt], new List<string>());
        }

        responses[prompts[currentPrompt]].Add(response);
        timeElapsed += 10; // increment time elapsed by 10 seconds
    }

    Console.WriteLine("Here are your responses:");
    foreach (var pair in responses)
    {
        Console.WriteLine(pair.Key);
        foreach (var response in pair.Value)
        {
            Console.WriteLine(response);
        }
    }

    Console.WriteLine("Activity completed!");
    Console.Write("Do you want to continue? (yes/no): ");
    string contResponse = Console.ReadLine();
    if (contResponse.ToLower() != "yes")
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);}



        }
    }
}

