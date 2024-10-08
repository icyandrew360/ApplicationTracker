﻿// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;

using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace TestAPI
{
    
    class Program
    {
        public static SQLiteConnection DBcontroller;
        public static void spaceToContinue(){
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Press space to continue...");
            spaceToContinue();
            DBcontroller = SQLite.Controller.CreateConnection();
            SQLiteCommand command;
            command = DBcontroller.CreateCommand();
            string validateTable = "CREATE TABLE IF NOT EXISTS applications (Company VARCHAR(20), Date VARCHAR(20), Status VARCHAR(20))";
            command.CommandText = validateTable;
            command.ExecuteNonQuery();
            //then display a list of current applications
            manageDB.mainPage();         
        }
    }

    class manageDB
    {
        static ArrayList appData = new ArrayList();
        
        public static void mainPage()
        {   
            while(true){
                Console.WriteLine("\n1) View or edit list");
                Console.WriteLine("2) Add new application");
                Console.WriteLine("3) Edit an application status\n");
                Console.WriteLine("9) DELETE ALL APPLICATIONS\n");
                //wait here until valid key press
                ConsoleKey key = ConsoleKey.D0;
                while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key != ConsoleKey.D3 && key != ConsoleKey.D9){
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
                //TODO: if company is invalid, return an error message
                if (key == ConsoleKey.D3){
                    Console.WriteLine("Editing application: \n");
                    Console.WriteLine("enter the name of the company: ");
                    String company = Console.ReadLine();
                    Console.WriteLine("enter the new status of the application: ");
                    String status = Console.ReadLine();
                    editApplication(company, status);
                }
                if (key == ConsoleKey.D9){
                    Console.WriteLine("Deleted all applications. \n");
                    deleteAllApplications();
                }
            }
        }
        public static void addNewApplication(string company, string date)
        {
            // string[] temp = new string[3];//company, date, status
            // temp[0] = company;
            // temp[1] = date;
            // temp[2] = "waiting";
            SQLiteCommand command;
            command = Program.DBcontroller.CreateCommand();
            string addApplication = "INSERT INTO applications (Company, Date, Status) VALUES (@param1, @param2, 'waiting')";
            command.CommandText = addApplication;
            command.Parameters.Add(new SQLiteParameter("@param1", company));
            command.Parameters.Add(new SQLiteParameter("@param2", date));
            command.ExecuteNonQuery();
      
            // appData.Add(temp);
        }

        public static void editApplication(string company, string status)
        {
            company = company.ToLower();
            foreach (string[] applicationLine in appData){
                if (String.Compare(applicationLine[0].ToLower(),company) == 0){//found the company
                    applicationLine[2] = status;
                    Console.WriteLine("Changed status for " + company);
                    return;
                }
            }
        }
        public static void showApplications()
        {
            // foreach (string[] applicationLine in appData){
            //     Console.WriteLine("{0, -15}{1, -15}{2, -15}", applicationLine[0], applicationLine[1], applicationLine[2]);
            // }
            SQLiteCommand command;
            command = Program.DBcontroller.CreateCommand();
            command.CommandText = "SELECT * FROM applications";
            SQLiteDataAdapter adapter;
            DataTable dt = new DataTable();
            adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine("{0, -15}{1, -15}{2, -15}", (string)row[0], (string)row[1], (string)row[2]);
            }
        }
        public static void deleteAllApplications()
        {
            SQLiteCommand command;
            command = Program.DBcontroller.CreateCommand();
            command.CommandText = "DELETE FROM applications";
            command.ExecuteNonQuery();
        }
    }
}

