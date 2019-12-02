using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class EventApp
    {
        User LoggedInUser;

        public void Start()
        {
            LoggedInUser = new User();
            ShowLoginScreen();

        }
        private void ShowLoginScreen()
        {
            Console.WriteLine("Skapa konto eller logga in");
            Console.WriteLine("  1. Logga in");
            Console.WriteLine("  2. Skapa konto");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("Loggin skräm");
                    Loggin();

                    //Logga in
                    break;
                case "2":
                    //Skapa konto
                    Console.WriteLine("Skapa konto skräm");
                    break;
                default:
                    break;

            }
                
        }
        private void Loggin()
        {
            Console.WriteLine("Skriv in ditt användarnamn!");
            Console.Write("Användarnamn: ");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Skriv in ditt Password!");
            Console.Write("Password: ");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("AnvädarMeny");
            Console.WriteLine(" 1. Skapa event");
            Console.WriteLine(" 2. Lista Evens");
            Console.WriteLine(" 3. logga ut");
            Console.Write("välj:");



            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreatEvent();
                    break;
                default:
                    break;
            }




        }
        private void CreatEvent ()
        {
            Console.Write("Fyll i Evetens Namn");
            Console.WriteLine("Namn");
            Console.WriteLine("Tid");
            Console.WriteLine("Datum");
            Console.WriteLine("");


        }
    }
}
