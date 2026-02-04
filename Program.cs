using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;

namespace EmployeeManager
{
    class Employee
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Department { get; set; }

        public Employee(string name, int id, string department)
        {
            Name = name;
            ID = id;
            Department = department;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            if (File.Exists("employees.txt"))
            {
            string[] lines = File.ReadAllLines("employees.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string n = parts[0];
                int i = int.Parse(parts[1]);
                string d = parts[2];
                employees.Add(new Employee(n, i, d));
            }
            }
            while(true)
            {
                Console.Clear();
                Console.WriteLine("------Options-------");
                Console.WriteLine("1. Add Employee :");
                Console.WriteLine("2. View Employee :");
                Console.WriteLine("3. Remove Employee :");
                Console.WriteLine("4. Search Employee :");
                Console.WriteLine("5. Update Department :");
                Console.WriteLine("0. Exit & Save:");
                Console.WriteLine("--------------------");
                Console.Write("Choosed Option : ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    string yesNo = "";
                    do
                        {
                        Console.WriteLine();
                        Console.Write("Enther Emloyee Name :");
                        string name = Console.ReadLine();
                        int id;
                        try
                            {
                                Console.Write("Enter Employee ID :");
                                id = Convert.ToInt32(Console.ReadLine());
                            }
                             catch
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid ID! Please Enter Numbers Only.");
                                Console.WriteLine("Press any key to try again...");
                                Console.ReadKey();
                                continue;
                            }
                        Console.Write("Enter Employee Department :");
                        string dept = Console.ReadLine();
                                if (employees.Find(e => e.ID == id) != null)
                                {
                                    Console.WriteLine("Sorry, that ID is already taken");
                                    Console.ReadKey();
                                    yesNo = "Y";
                                }
                                else
                            {
                                employees.Add(new Employee(name,id,dept));
                                SaveData(employees);
                                Console.Write("Add another Employee (Y/N) : ");
                                yesNo = Console.ReadLine();
                            }
                        
                        } while (yesNo.ToUpper() == "Y");
                        break;

                    case "2":
                    if (employees.Count == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("No Employees to Show");
                            Console.WriteLine("\nPress any key to return to menu...");
                            Console.ReadKey();
                            continue;  
                        }
                    DisplayEmployees(employees);
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                        break;

                    case "3":
                    Console.WriteLine();
                    Console.Write("Enter the ID of Employee to Remove: ");
                    int rmID = Convert.ToInt32(Console.ReadLine());
                    Employee rmEmployee = employees.Find(e => e.ID == rmID);

                    if (rmEmployee != null)
                    {
                        employees.Remove(rmEmployee);
                        SaveData(employees);
                    }
                    else 
                    {
                        Console.WriteLine("ID Not Found.");
                    }
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                        break;

                    case "4":
                    Console.WriteLine();
                    Console.WriteLine("Enter the Name for Search :");
                    string searchName = Console.ReadLine();
                    List<Employee> results = employees.FindAll(e => e.Name.Contains(searchName));
                    if (results.Count > 0)
                        {
                            DisplayEmployees(results);
                        }
                        else
                        {
                            Console.WriteLine("No Matches Found");
                        }
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                        break;

                    case "5":
                        Console.WriteLine();
                        try
                        {
                            Console.Write("Enter Employee ID to Update: ");
                            int upID = Convert.ToInt32(Console.ReadLine());
                            Employee upEmp = employees.Find(e => e.ID == upID);
                            if (upEmp != null)
                            {
                                Console.Write($"Enter New Department for {upEmp.Name}: ");
                                upEmp.Department = Console.ReadLine();
                                Console.WriteLine("Update Successful!");
                            }
                            else
                            {
                                Console.WriteLine("Employee Not Found.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Invalid ID Input.");
                        }
                        SaveData(employees);

                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                        break;

                    case "0":
                    Console.WriteLine();
                    SaveData(employees);
                    return;

                    default:
                    Console.WriteLine();
                    Console.WriteLine("Please Input 0/1/2/3/4 Only !");
                    Console.ReadKey();
                    break;
                }
            }
        }   
        static void SaveData(List<Employee> employees)
        {
                using (StreamWriter writer = new StreamWriter("employees.txt"))
                        {
                            foreach (Employee e in employees)
                            {
                                writer.WriteLine($"{e.Name},{e.ID},{e.Department}");
                            }
                        }
        } 
        static void DisplayEmployees(List<Employee> listToPrint)
        {
        Console.WriteLine();
        Console.WriteLine($"{"ID",-10} | {"Name",-20} | {"Department",-20}");
        Console.WriteLine(new string('-', 55));
        foreach (Employee I in listToPrint)
        {
            Console.WriteLine($"{I.ID,-10} | {I.Name,-20} | {I.Department,-20}");
        }
        }
    }
}



