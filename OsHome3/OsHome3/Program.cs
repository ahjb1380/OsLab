using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<Dictionary<string, object>>();
            string userInput;

            string filePath = "users.json";

            string jsonDataFromFile = File.ReadAllText(filePath);

            var usersFromFile = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonDataFromFile);
            Console.WriteLine("Data read from the json file.\nData read with the following userNames:\n");

            foreach (var user in usersFromFile)
            {
                users.Add(user);
                Console.WriteLine(user["userName"]);
            }

            Console.WriteLine("Welcome to Saman\'s third session homework:/n");
            Console.WriteLine("1.Create a user\n2.Read from the json file\n3.Print the users userName in JSON file");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    string draftUserUsername;
                    string draftUserAge;
                    bool draftUserIsMale;

                    Console.WriteLine("Please enter the username:");
                    draftUserUsername = Console.ReadLine();

                    Console.WriteLine("Please enter the user's age:");
                    draftUserAge = Console.ReadLine();

                    Console.WriteLine("Is the user male?");
                    draftUserIsMale = bool.Parse(Console.ReadLine());

                    var newUser = new Dictionary<string, object>()
                    {
                        {"userName", draftUserUsername},
                        {"userAge", draftUserAge},
                        {"userIsMale", draftUserIsMale}
                    };
                    users.Add(newUser);

                    string json = JsonSerializer.Serialize(users);

                    File.WriteAllText(filePath, json);

                    Console.WriteLine("Users list has been saved to users.json file.");
                    return;

                case "2":
                    Console.WriteLine("Insert the JSON file path, or type 'local' to import from the default file:");
                    string tempPath = Console.ReadLine();
                    if (tempPath == "local") { jsonDataFromFile = File.ReadAllText(filePath); }
                    else
                    {
                        jsonDataFromFile = File.ReadAllText(tempPath);
                    }

                    usersFromFile = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonDataFromFile);
                    Console.WriteLine("Data read from the json file.\nData read with the following userNames:\n");

                    foreach (var user in usersFromFile)
                    {
                        users.Add(user);
                        Console.WriteLine(user["userName"]);
                    }

                    return;

                case "3":
                    Console.WriteLine("\n");
                    foreach (var user in users)
                    {
                        Console.WriteLine("==========");
                        Console.WriteLine(user["userName"]);
                    }

                    return;
            }



        }
    }
}

