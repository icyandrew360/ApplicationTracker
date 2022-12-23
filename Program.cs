// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
namespace TestAPI
{
    
    class Program
    {
        public static void spaceToContinue(){
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Press space to continue...");
            spaceToContinue();
            //then display a list of current applications
            manageDB.listDB();         
        }
    }

    class manageDB
    {
        static ArrayList appData = new ArrayList();
        
        public static void listDB()
        {   
            while(true){
                Console.WriteLine("\n1) View or edit list");
                Console.WriteLine("2) Add new application\n");
                //wait here until 1 or 2 is pressed
                ConsoleKey key = ConsoleKey.D0;
                while (key != ConsoleKey.D1 && key != ConsoleKey.D2){
                    key = Console.ReadKey(true).Key;
                }
                if (key == ConsoleKey.D2){
                    Console.WriteLine("adding new application: \n");
                    
                    //TODO: add null and error string checking.
                    Console.WriteLine("enter the name of the company: ");
                    String company = Console.ReadLine();
                    Console.WriteLine("enter the date of application: ");
                    String date = Console.ReadLine();
                    addNewApplication(company, date);
                    Console.WriteLine("added application: " + company + " " + date);
                }
                if (key == ConsoleKey.D1){
                    Console.WriteLine("viewing list:");
                    showApplications();
                    Console.WriteLine();
                    Console.WriteLine("press space to continue... ");
                    Program.spaceToContinue();
                }
            }
        }
        public static void addNewApplication(string company, string date)
        {
            string[] temp = new string[3];//company, date, status
            temp[0] = company;
            temp[1] = date;
            temp[2] = "waiting";
            appData.Add(temp);
        }
        public static void showApplications()
        {
            foreach (string[] applicationLine in appData){
                Console.WriteLine("{0, -15}{1, -15}{2, -15}", applicationLine[0], applicationLine[1], applicationLine[2]);
            }
        }
    }
}

