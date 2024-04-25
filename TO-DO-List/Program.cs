using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace TO_DO_List
{
    internal class Program
    {
        public class Task
        {
            public string Description { get; set; }
            public bool IsComplete { get; set; }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                var tasks = ReadFromJson();
                Console.WriteLine("Welcome to the TODO list, here are your current items:\n");
                DisplayTasks(tasks, false);
                int CRUD = DisplayCRUDOptions();
                switch (CRUD)
                {
                    case 1: // Create new record
                        RecordCreate(tasks);
                        break;

                    case 2: // Set record to complete
                        RecordComplete(tasks);
                        break;
                    case 3: // Update existing record
                        RecordUpdate(tasks);
                        break;
                    case 4: // Delete record
                        RecordDelete(tasks);
                        break;
                }
                Console.Clear();
            }

        }

        static void RecordCreate(List<Task> tasks)
        {
            Console.Clear();
            var allTasks = ReadFromJson();
            Console.WriteLine("What is the name of this task?");
            string taskName = Console.ReadLine();
            var newTask = new Task { Description = taskName, IsComplete = false };

            allTasks.Add(newTask);
            WriteToJSON(allTasks);

        }

        static void RecordComplete(List<Task> tasks)
        {
            int usersChoice;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Which task have you completed?");
                //DisplayTasks(tasks);
                DisplayIncompleteTasks(tasks);

                if (int.TryParse(Console.ReadLine(), out usersChoice) && usersChoice >= 1 && usersChoice <= tasks.Count + 1)
                {
                    usersChoice--;
                    break;
                }
                else
                {
                    Console.WriteLine("Please select a task from the list");
                }
            }
            Console.Clear();
            tasks[usersChoice].IsComplete = true;
            WriteToJSON(tasks);
            Console.WriteLine("Task has been updated, congratulations! Press any key to return home...");
            Console.ReadLine();
        }

        static void RecordUpdate(List<Task> tasks)
        {
            int usersChoice;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Which task would you like to update?");
                DisplayTasks(tasks);

                if (int.TryParse(Console.ReadLine(), out usersChoice) && usersChoice >= 1 && usersChoice <= tasks.Count + 1)
                {
                    usersChoice--;
                    break;
                }
                else
                {

                    Console.WriteLine("Please select a task from the list");
                }
            }
            Console.Clear();
            Console.WriteLine($"The task is currently called: {tasks[usersChoice].Description}");
            Console.WriteLine("Please enter a new name for the record: ");
            tasks[usersChoice].Description = Console.ReadLine();
            WriteToJSON(tasks);
            Console.WriteLine("Task has been updated. Press any key to return home...");
            Console.ReadLine();
        }
        
        static void RecordDelete(List<Task> tasks)
        {
            int usersChoice;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Which task would you like to delete?");
                DisplayTasks(tasks);

                if (int.TryParse(Console.ReadLine(), out usersChoice) && usersChoice >= 1 && usersChoice <= tasks.Count + 1)
                {
                    usersChoice--;
                    break;
                }
                else
                {

                    Console.WriteLine("Please select a task from the list");
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Are you sure you wish to delete this item? Type YES to continue, type NO to cancel");
                string response = Console.ReadLine();
                if (response == "YES")
                {
                    tasks.Remove(tasks[usersChoice]);
                    WriteToJSON(tasks);
                    Console.WriteLine("Task has been deleted. Press any key to return home...");
                    break;
                }
                else if (response == "NO")
                {
                    Console.WriteLine("Task has NOT been deleted. Press any key to return home...");
                    break;
                }  
            }
            Console.ReadLine();
        }

        static int DisplayCRUDOptions()
        {
            while (true)
            {
                Console.WriteLine("What would you like to do: ");
                Console.WriteLine("1: Create an item");
                Console.WriteLine("2: Complete an item");
                Console.WriteLine("3: Update an item");
                Console.WriteLine("4: Delete an item");

                int usersChoice;

                if (int.TryParse(Console.ReadLine(), out usersChoice) && usersChoice >= 1 && usersChoice <= 4)
                {
                    return usersChoice;
                }
                else
                {
                    Console.WriteLine("Please select a number from the list");
                }
            }
        }

        static void WriteToJSON(List<Task> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            File.WriteAllText(@"C:\Users\Joshua\Desktop\Coding\C#\TO-DO-List\todolist.json", jsonString);
        }

        static void AddToJSON(List<Task> newTasks)
        {
            var allTasks = ReadFromJson();
            allTasks.AddRange(newTasks);

            string jsonString = JsonSerializer.Serialize(allTasks);
            File.WriteAllText(@"C:\Users\Joshua\Desktop\Coding\C#\TO-DO-List\todolist.json", jsonString);
        }

        static List<Task> ReadFromJson()
        {
            string jsonString = File.ReadAllText(@"C:\Users\Joshua\Desktop\Coding\C#\TO-DO-List\todolist.json");
            var tasks = JsonSerializer.Deserialize<List<Task>>(jsonString);
            return tasks;

        }

        static void DisplayTasks(List<Task> tasks, bool displayNumbers = true)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                string position = (displayNumbers) ? $"{i + 1}: " : ""; // can be supplied false to not order list, makes user selection clearer
                string status = (tasks[i].IsComplete) ? "Finished" : "Incomplete";
                Console.WriteLine($"{position}{tasks[i].Description} - {status}");
            }
        }
        
        static void DisplayIncompleteTasks(List<Task> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (!tasks[i].IsComplete)
                {
                Console.WriteLine($"{i + 1}: {tasks[i].Description}");
                }
            }
        }
    }
}
