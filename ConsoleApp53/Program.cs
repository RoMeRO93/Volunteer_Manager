using System;
using System.Collections.Generic;

public class Volunteer
{
    public string Name { get; set; }

    public Volunteer(string name)
    {
        Name = name;
    }
}

public class Task
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public Task(string description)
    {
        Description = description;
        IsCompleted = false;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}

public class VolunteerProject
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Organizer { get; set; }
    public List<Volunteer> Volunteers { get; set; }
    public List<Task> Tasks { get; set; }
    public int MaxVolunteers { get; set; } // Adăugare proprietate MaxVolunteers

    public VolunteerProject(string name, string description, DateTime startDate, DateTime endDate, string organizer, int maxVolunteers)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Organizer = organizer;
        Volunteers = new List<Volunteer>();
        Tasks = new List<Task>();
        MaxVolunteers = maxVolunteers; // Inițializare MaxVolunteers
    }

    public bool IsOngoing()
    {
        DateTime currentDate = DateTime.Now;
        return (currentDate >= StartDate && currentDate <= EndDate);
    }

    public void AddVolunteer(string volunteerName)
    {
        Volunteer volunteer = new Volunteer(volunteerName);
        Volunteers.Add(volunteer);
        Console.WriteLine(volunteerName + " has joined the project.");
    }

    public void RemoveVolunteer(string volunteerName)
    {
        Volunteer volunteer = Volunteers.Find(v => v.Name == volunteerName);
        if (volunteer != null)
        {
            Volunteers.Remove(volunteer);
            Console.WriteLine(volunteerName + " has been removed from the project.");
        }
    }

    public void AddTask(string taskDescription)
    {
        Task task = new Task(taskDescription);
        Tasks.Add(task);
        Console.WriteLine("Task added: " + taskDescription);
    }

    public void MarkTaskAsCompleted(string taskDescription)
    {
        Task task = Tasks.Find(t => t.Description == taskDescription);
        if (task != null)
        {
            task.MarkAsCompleted();
            Console.WriteLine("Task marked as completed: " + taskDescription);
        }
    }

    public void DisplayProjectDetails()
    {
        Console.WriteLine("Project Name: " + Name);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Start Date: " + StartDate.ToShortDateString());
        Console.WriteLine("End Date: " + EndDate.ToShortDateString());
        Console.WriteLine("Organizer: " + Organizer);

        Console.WriteLine("Volunteers:");
        foreach (Volunteer volunteer in Volunteers)
        {
            Console.WriteLine("- " + volunteer.Name);
        }

        Console.WriteLine("Tasks:");
        foreach (Task task in Tasks)
        {
            Console.WriteLine("- " + task.Description + (task.IsCompleted ? " [Completed]" : ""));
        }
    }
}

public class VolunteerPlatform
{
    public List<VolunteerProject> Projects { get; set; }

    public VolunteerPlatform()
    {
        Projects = new List<VolunteerProject>();
    }

    public void AddProject(VolunteerProject project)
    {
        Projects.Add(project);
        Console.WriteLine("New project added: " + project.Name);
    }

    public void RemoveProject(VolunteerProject project)
    {
        Projects.Remove(project);
        Console.WriteLine("Project removed: " + project.Name);
    }

    public void DisplayAllProjects()
    {
        foreach (VolunteerProject project in Projects)
        {
            project.DisplayProjectDetails();
            Console.WriteLine();
        }
    }

    public List<VolunteerProject> GetProjectsByOrganizer(string organizer)
    {
        List<VolunteerProject> projectsByOrganizer = Projects.FindAll(project => project.Organizer == organizer);
        return projectsByOrganizer;
    }

    public List<VolunteerProject> GetProjectsByKeyword(string keyword)
    {
        List<VolunteerProject> projectsByKeyword = Projects.FindAll(project => project.Name.Contains(keyword) || project.Description.Contains(keyword));
        return projectsByKeyword;
    }

    public List<VolunteerProject> GetAvailableProjects()
    {
        DateTime currentDate = DateTime.Now;
        List<VolunteerProject> availableProjects = Projects.FindAll(project => project.IsOngoing() && project.Volunteers.Count < project.MaxVolunteers);
        return availableProjects;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        VolunteerPlatform platform = new VolunteerPlatform();
        VolunteerProject project1 = new VolunteerProject("Teach Kids Programming", "Teach programming to underprivileged kids", new DateTime(2023, 6, 1), new DateTime(2023, 8, 31), "John Doe", 10);
        VolunteerProject project2 = new VolunteerProject("Environmental Cleanup", "Cleanup local parks and rivers", new DateTime(2023, 7, 1), new DateTime(2023, 9, 30), "Jane Smith", 5);
        platform.AddProject(project1);
        platform.AddProject(project2);

        project1.AddVolunteer("John");
        project1.AddVolunteer("Alice");

        project1.AddTask("Prepare teaching materials");
        project1.AddTask("Coordinate volunteers");

        project2.AddVolunteer("Bob");

        platform.DisplayAllProjects();

        List<VolunteerProject> projectsByOrganizer = platform.GetProjectsByOrganizer("John Doe");
        Console.WriteLine("Projects by John Doe:");
        foreach (VolunteerProject project in projectsByOrganizer)
        {
            project.DisplayProjectDetails();
            Console.WriteLine();
        }

        List<VolunteerProject> projectsByKeyword = platform.GetProjectsByKeyword("programming");
        Console.WriteLine("Projects related to programming:");
        foreach (VolunteerProject project in projectsByKeyword)
        {
            project.DisplayProjectDetails();
            Console.WriteLine();
        }

        List<VolunteerProject> availableProjects = platform.GetAvailableProjects();
        Console.WriteLine("Available Projects:");
        foreach (VolunteerProject project in availableProjects)
        {
            project.DisplayProjectDetails();
            Console.WriteLine();
        }
    }
}
