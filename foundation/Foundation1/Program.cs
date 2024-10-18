using System;
using System.Collections.Generic;

// The Comment class to represent each comment on a video
class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// The Video class to represent a YouTube video
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    // Method to add a comment to the video
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Method to get the number of comments on the video
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Method to display all details of the video
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        // Display each comment for the video
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"{comment.CommenterName}: {comment.Text}");
        }
        Console.WriteLine(); // Add space between videos
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a few video objects
        Video video1 = new Video("Introduction to C#", "John Doe", 300);
        Video video2 = new Video("Learn Python in 10 Minutes", "Jane Smith", 600);
        Video video3 = new Video("JavaScript for Beginners", "Mike Johnson", 450);

        // Add comments to the first video
        video1.AddComment(new Comment("Alice", "Great introduction!"));
        video1.AddComment(new Comment("Bob", "Very informative."));
        video1.AddComment(new Comment("Charlie", "Thanks for sharing!"));

        // Add comments to the second video
        video2.AddComment(new Comment("Eve", "Loved the tutorial!"));
        video2.AddComment(new Comment("Frank", "Easy to follow."));
        video2.AddComment(new Comment("Grace", "Python is awesome!"));

        // Add comments to the third video
        video3.AddComment(new Comment("Heidi", "Good explanations."));
        video3.AddComment(new Comment("Ivan", "Very clear instructions."));
        video3.AddComment(new Comment("Judy", "Helped me a lot."));

        // Store all videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Iterate through the list and display video details and comments
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
