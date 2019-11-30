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

            if (inputPassword == )
            {

            }

        }
        private void CreateAccount()
        {

        }
    }
}
