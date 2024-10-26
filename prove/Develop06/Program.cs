// In this project, I have added a leveling system and badge earning functionality to enhance user engagement and motivation. 
// Each level unlocks new goals and achievements. Daily challenges offer additional opportunities for points, making the program more dynamic and fun.

using System;
using System.Collections.Generic;
using System.Linq;

// Base class for goals
public abstract class Goal
{
    public string Name { get; private set; } // Make Name public
    protected string Description { get; set; }
    protected int Points { get; set; }
    protected bool IsComplete { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();
    public virtual string GetDetailsString()
    {
        return $"{Name}: {Description} - {(IsComplete ? "[X]" : "[ ]")}";
    }
}

// Simple goal class
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        IsComplete = true;
        // Add points to user's score
        Program.TotalPoints += Points;
        Program.CheckForLevelUp();
    }
}

// Eternal goal class
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        // Add points to user's score every time this goal is recorded
        Program.TotalPoints += Points;
        Program.CheckForLevelUp();
    }
}

// Checklist goal class
public class ChecklistGoal : Goal
{
    private int TimesCompleted { get; set; }
    private int Target { get; set; }
    private int BonusPoints { get; set; }

    public ChecklistGoal(string name, string description, int points, int target, int bonusPoints) 
        : base(name, description, points)
    {
        Target = target;
        BonusPoints = bonusPoints;
        TimesCompleted = 0;
    }

    public override void RecordEvent()
    {
        TimesCompleted++;
        Program.TotalPoints += Points;
        if (TimesCompleted >= Target)
        {
            IsComplete = true;
            Program.TotalPoints += BonusPoints; // Add bonus points
        }
        Program.CheckForLevelUp();
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} - Completed {TimesCompleted}/{Target} times";
    }
}

// Goal manager class
public class GoalManager
{
    private List<Goal> goals = new List<Goal>();

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoalEvent(string goalName)
    {
        var goal = goals.FirstOrDefault(g => g.Name.Equals(goalName, StringComparison.OrdinalIgnoreCase));
        goal?.RecordEvent();
    }

    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
        Console.WriteLine($"Total Points: {Program.TotalPoints}");
    }
}

// Program class for user interaction
class Program
{
    public static int TotalPoints { get; set; } = 0;
    public static int Level { get; set; } = 1;
    public static List<string> BadgesEarned { get; set; } = new List<string>();

    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Eternal Quest Menu:");
            Console.WriteLine("1. Add Simple Goal");
            Console.WriteLine("2. Add Eternal Goal");
            Console.WriteLine("3. Add Checklist Goal");
            Console.WriteLine("4. Record Goal Event");
            Console.WriteLine("5. Display Goals");
            Console.WriteLine("6. Display Level and Badges");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string simpleName = Console.ReadLine();
                    Console.Write("Enter goal description: ");
                    string simpleDescription = Console.ReadLine();
                    Console.Write("Enter points for goal: ");
                    int simplePoints = int.Parse(Console.ReadLine());
                    goalManager.AddGoal(new SimpleGoal(simpleName, simpleDescription, simplePoints));
                    break;

                case "2":
                    Console.Write("Enter goal name: ");
                    string eternalName = Console.ReadLine();
                    Console.Write("Enter goal description: ");
                    string eternalDescription = Console.ReadLine();
                    Console.Write("Enter points for each recording: ");
                    int eternalPoints = int.Parse(Console.ReadLine());
                    goalManager.AddGoal(new EternalGoal(eternalName, eternalDescription, eternalPoints));
                    break;

                case "3":
                    Console.Write("Enter goal name: ");
                    string checklistName = Console.ReadLine();
                    Console.Write("Enter goal description: ");
                    string checklistDescription = Console.ReadLine();
                    Console.Write("Enter points for each completion: ");
                    int checklistPoints = int.Parse(Console.ReadLine());
                    Console.Write("Enter target number of completions: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points for completion: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    goalManager.AddGoal(new ChecklistGoal(checklistName, checklistDescription, checklistPoints, target, bonusPoints));
                    break;

                case "4":
                    Console.Write("Enter goal name to record: ");
                    string goalNameToRecord = Console.ReadLine();
                    goalManager.RecordGoalEvent(goalNameToRecord);
                    break;

                case "5":
                    goalManager.DisplayGoals();
                    break;

                case "6":
                    DisplayLevelAndBadges();
                    break;

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    public static void CheckForLevelUp()
    {
        if (TotalPoints >= Level * 1000) // Example level-up threshold
        {
            Level++;
            Console.WriteLine($"Congratulations! You've leveled up to Level {Level}!");
            EarnBadge();
        }
    }

    public static void EarnBadge()
    {
        // Example badge logic
        if (Level == 2)
            BadgesEarned.Add("Goal Getter!");
        else if (Level == 3)
            BadgesEarned.Add("Overachiever!");
        else if (Level == 4)
            BadgesEarned.Add("Master of Goals!");

        if (BadgesEarned.Count > 0)
        {
            Console.WriteLine($"You've earned a new badge: {BadgesEarned.Last()}!");
        }
    }

    public static void DisplayLevelAndBadges()
    {
        Console.WriteLine($"Current Level: {Level}");
        Console.WriteLine("Badges Earned:");
        if (BadgesEarned.Count == 0)
        {
            Console.WriteLine("No badges earned yet.");
        }
        else
        {
            foreach (var badge in BadgesEarned)
            {
                Console.WriteLine($"- {badge}");
            }
        }
    }
}
