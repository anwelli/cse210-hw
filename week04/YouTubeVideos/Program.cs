using System;
using System.Collections.Generic;

public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }

    public void DisplayCommentInfo()
    {
        Console.WriteLine($"  - {Name}: {Text}");
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; }

    public Video()
    {
        Comments = new List<Comment>();
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            comment.DisplayCommentInfo();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        var video1 = new Video
        {
            Title = "Video 1",
            Author = "Author 1",
            Length = 300
        };

        var video2 = new Video
        {
            Title = "Video 2",
            Author = "Author 2",
            Length = 240
        };

        var video3 = new Video
        {
            Title = "Video 3",
            Author = "Author 3",
            Length = 180
        };

        // Add comments to videos
        video1.Comments.Add(new Comment("John Doe", "Great video!"));
        video1.Comments.Add(new Comment("Jane Doe", "I loved it!"));
        video1.Comments.Add(new Comment("Bob Smith", "Nice job!"));

        video2.Comments.Add(new Comment("Alice Johnson", "Excellent video!"));
        video2.Comments.Add(new Comment("Mike Brown", "Very informative!"));
        video2.Comments.Add(new Comment("Emily Davis", "Well done!"));

        video3.Comments.Add(new Comment("Sarah Taylor", "Good job!"));
        video3.Comments.Add(new Comment("David Lee", "Nice video!"));
        video3.Comments.Add(new Comment("Lisa Nguyen", "Great effort!"));

        // Store videos in a list
        var videos = new List<Video> { video1, video2, video3 };

        // Display video information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine();
        }
    }
}

