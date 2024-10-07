// Exceeding the basic requirement 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main()
    {
        ScriptureLibrary library = new ScriptureLibrary();
        library.LoadScripturesFromFile("scriptures.txt"); // Load scriptures from a file
        
        // Get a random scripture from the library
        Scripture scripture = library.GetRandomScripture();
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.Display());
            Console.WriteLine("Press Enter to hide some words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("All words are now hidden!");
                break;
            }
            
            scripture.HideRandomWords(3); // Hide 3 random words
        }
    }
}

public class Scripture
{
    public Reference Reference { get; private set; }
    public List<Word> Words { get; private set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public string Display()
    {
        return $"{Reference} {string.Join(" ", Words)}";
    }

    public void HideRandomWords(int count)
    {
        // Select words that are not already hidden
        var hiddenWords = Words.Where(w => !w.IsHidden)
                                .OrderBy(w => Guid.NewGuid())
                                .Take(count)
                                .ToList();
        
        foreach (var word in hiddenWords)
        {
            word.Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}

public class Reference
{
    public string SingleVerse { get; private set; }
    public string VerseRange { get; private set; }

    public Reference(string verse)
    {
        if (verse.Contains("-"))
        {
            VerseRange = verse;
        }
        else
        {
            SingleVerse = verse;
        }
    }

    public override string ToString()
    {
        return SingleVerse ?? VerseRange;
    }
}

public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? "_" : Text;
    }
}

public class ScriptureLibrary
{
    private List<Scripture> scriptures;

    public ScriptureLibrary()
    {
        scriptures = new List<Scripture>();
    }

    public void LoadScripturesFromFile(string filePath)
    {
        // Load scriptures from a file
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var reference = new Reference(parts[0].Trim());
                    var text = parts[1].Trim();
                    scriptures.Add(new Scripture(reference, text));
                }
            }
        }
        else
        {
            Console.WriteLine("Scripture file not found.");
        }
    }

    public Scripture GetRandomScripture()
    {
        Random random = new Random();
        return scriptures[random.Next(scriptures.Count)];
    }
}
