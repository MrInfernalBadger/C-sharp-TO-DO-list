using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TO_DO_List.Program;

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
            var newTasks = new List<Task>
            {
                new Task{ Description = "Third Task", IsComplete = true },
                new Task{ Description = "Fourth Task", IsComplete = false }
            };
            AddToJSON(newTasks);
            DisplayJson(ReadFromJson());
        }

        static void WriteToJSON()
        {
            var tasks = new List<Task>
            {
                new Task{ Description = "First Task", IsComplete = true },
                new Task{ Description = "Second Task", IsComplete = false }
            };
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

        static void DisplayJson(List<Task> tasks)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Description} - Complete: {task.IsComplete}");
            }
            Console.ReadLine();
        }
    }
}
