// See https://aka.ms/new-console-template for more information
using HTTPConnection;
using TDD_Example;



IClient client = new Client();
StatusCheck check = new StatusCheck(client);


string url = "htt://www.google.com";

if(check.WebsiteIsRunning(url))
    Console.WriteLine("WEBSITE IS RUNNING");
else
    Console.WriteLine("WEBSITE DOWN");