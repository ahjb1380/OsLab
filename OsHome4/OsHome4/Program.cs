using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

Console.WriteLine("Hello kind user, Please Enter your URLs to start downloading them using thread: (Separate each URL with a comma)");
string userInput = Console.ReadLine();

string[] URLs = userInput.Split(',');

string folderPath = Path.Combine("./", "DownloadedFiles");
Directory.CreateDirectory("DownloadedFiles");

List<Thread> threads = new List<Thread>();

foreach (var URL in URLs)
{
    Thread thread = new Thread(() => DownloadFile(URL.Trim(), folderPath));
    thread.Start();
    threads.Add(thread);
}

foreach (var thread in threads)
{
    thread.Join();
}

Console.WriteLine("\n==================\nAll downloads completed.\n==================\n");

static void DownloadFile(string URL, string folderPath)
{
    try
    {
        using (WebClient client = new WebClient())
        {
            string fileName = URL.Substring(URL.LastIndexOf('/') + 1);
            string filePath = Path.Combine("./DownloadedFiles/", fileName);
            client.DownloadFile(URL, filePath);
            Console.WriteLine($"--------------------------------\nIt looks like your file has been downloaded: {fileName}\n--------------------------------");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n---------------------There's an error caused by something while downloading the file from {URL}: {ex.Message}\n----------------------");
    }
}