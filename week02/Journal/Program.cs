using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Journal(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }
}

public class Entry
{
    public Journal Journal { get; set; }

    public Entry(Journal journal)
    {
        Journal = journal;
    }
}

class Program
{
    static List<Entry> entries = new List<Entry>();
    static string[] prompts = new string[]
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Journal Program");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Search journal entries");
            Console.WriteLine("6. Exit");

            Console.Write("Choose an option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    WriteNewEntry();
                    break;
                case 2:
                    DisplayJournalEntries();
                    break;
                case 3:
                    SaveJournalToFile();
                    break;
                case 4:
                    LoadJournalFromFile();
                    break;
                case 5:
                    SearchJournalEntries();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }

    static void WriteNewEntry()
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToShortDateString();
        Journal journal = new Journal(prompt, response, date);
        Entry entry = new Entry(journal);
        entries.Add(entry);
    }

    static string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(0, prompts.Length);
        return prompts[index];
    }

    static void DisplayJournalEntries()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine($"Prompt: {entry.Journal.Prompt}");
            Console.WriteLine($"Response: {entry.Journal.Response}");
            Console.WriteLine($"Date: {entry.Journal.Date}");
            Console.WriteLine();
        }
    }

    static void SaveJournalToFile()
    {
        Console.Write("Enter filename: ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine($"{entry.Journal.Prompt}|{entry.Journal.Response}|{entry.Journal.Date}");
            }
        }
    }

    static void LoadJournalFromFile()
    {
        Console.Write("Enter filename: ");
        string filename = Console.ReadLine();
        string[] lines = File.ReadAllLines(filename);
        entries.Clear();
        foreach (string line in lines)
        {
            string[] parts = line.Split("|");
            Journal journal = new Journal(parts[0], parts[1], parts[2]);
            Entry entry = new Entry(journal);
            entries.Add(entry);
        }
    }

    static void SearchJournalEntries()
    {
        Console.Write("Enter search term: ");
        string searchTerm = Console.ReadLine();
        bool found = false;
        foreach (Entry entry in entries)
        {
            if (entry.Journal.Prompt.Contains(searchTerm) || entry.Journal.Response.Contains(searchTerm))
            {
                Console.WriteLine($"Prompt: {entry.Journal.Prompt}");
                Console.WriteLine($"Response: {entry.Journal.Response}");
                Console.WriteLine($"Date: {entry.Journal.Date}");
                Console.WriteLine();
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("No matching entries found.");
        }
    }
}
