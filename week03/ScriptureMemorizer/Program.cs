
using System;

// Represents a word in the scripture
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }

    public bool IsHidden()
    {
        return _isHidden;
    }
}

// Represents a scripture reference
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_verse == _endVerse)
        {
            return $"{_book} {_chapter}:{_verse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
    }
}

// Represents a scripture
public class Scripture
{
    private Reference _reference;
    private Word[] _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new Word[text.Split(' ').Length];

        string[] words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            _words[i] = new Word(words[i]);
        }
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        int wordsToHide = random.Next(1, 4);

        for (int i = 0; i < wordsToHide; i++)
        {
            int wordIndex = random.Next(_words.Length);
            if (!_words[wordIndex].IsHidden())
            {
                _words[wordIndex].Hide();
            }
            else
            {
                i--;
            }
        }
    }

    public string GetDisplayText()
    {
        string text = "";
        foreach (Word word in _words)
        {
            text += word.GetDisplayText() + " ";
        }
        return $"{_reference.GetDisplayText()}\n{text}";
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // List of scriptures
        (Reference reference, string text)[] scriptures = new[]
        {
            (new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son that whoever believes in him shall not perish but have eternal life"),
            (new Reference("Philippians", 4, 13), "I can do all this through him who gives me strength"),
            (new Reference("Isaiah", 40, 31), "But those who hope in the Lord will renew their strength. They will soar on wings like eagles; they will run and not grow weary, they will walk and not be faint"),
            (new Reference("Psalm", 23, 4), "Even though I walk through the darkest valley, I will fear no evil, for you are with me; your rod and your staff comfort me"),
            (new Reference("Proverbs", 3, 5), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight"),
        };

        Random random = new Random();
        (Reference reference, string text) scripture = scriptures[random.Next(scriptures.Length)];

        Scripture currentScripture = new Scripture(scripture.reference, scripture.text);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(currentScripture.GetDisplayText());

            Console.Write("Press Enter to continue, type 'quit' to finish: ");
          string input = Console.ReadLine().ToLower();

        if (input == "quit")
        {
            break;
        }
        else if (input == "")
        {
            currentScripture.HideRandomWords();

            if (currentScripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(currentScripture.GetDisplayText());
                Console.WriteLine("All words are now hidden. Goodbye!");
                break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

}