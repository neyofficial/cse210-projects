using System;
using System.Collections.Generic;

// Base class
public abstract class Activity
{
    // Shared attributes
    private DateTime date;
    private int duration; // duration in minutes

    // Constructor
    public Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    // Encapsulated properties
    public DateTime Date
    {
        get { return date; }
    }

    public int Duration
    {
        get { return duration; }
    }

    // Virtual methods to be overridden
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Get summary method
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} - Duration: {Duration} min, " +
               $"Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min/mile";
    }
}

// Derived class for Running
public class Running : Activity
{
    private double distance; // in miles

    public Running(DateTime date, int duration, double distance) : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Duration) * 60; // mph
    }

    public override double GetPace()
    {
        return Duration / GetDistance(); // min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Running";
    }
}

// Derived class for Cycling
public class Cycling : Activity
{
    private double speed; // in mph

    public Cycling(DateTime date, int duration, double speed) : base(date, duration)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed / 60) * Duration; // Distance = Speed * Time
    }

    public override double GetSpeed()
    {
        return speed; // mph
    }

    public override double GetPace()
    {
        return 60 / speed; // min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Cycling";
    }
}

// Derived class for Swimming
public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int duration, int laps) : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return (laps * 50.0 / 1000) * 0.621371; // Convert to miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Duration) * 60; // mph
    }

    public override double GetPace()
    {
        return Duration / GetDistance(); // min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Swimming";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list to hold activities
        List<Activity> activities = new List<Activity>();

        // Add different activities to the list
        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0)); // 3 miles running
        activities.Add(new Cycling(new DateTime(2022, 11, 3), 30, 15.0)); // 15 mph cycling
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 20)); // 20 laps swimming

        // Display the summary for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
