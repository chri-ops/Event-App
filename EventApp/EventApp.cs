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

            switch (input)
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

            Console.WriteLine("Skriv in ditt Lössenord!");
            Console.Write("Lössenord: ");
            string inputPassword = Console.ReadLine();

        }
        private void CreateAccount()
        {
            Console.WriteLine("Registrera ett nytt konto");


            Console.Write("Fyll i användernamn: ");
            string inputUserName = Console.ReadLine();

            while (true)
            {
                Console.Write("Fyll i din E-mail: ");
                string inputMail = Console.ReadLine();

                //if (inputMail == ) // har ska vi Kolla om mailet är redan registrarat 
                //{

                //}
                //else
                //{

                //}
                break;
            }




            while (true)
            {

                Console.Write("Fyll i Lössenord: ");
                string inputPassword = Console.ReadLine();

                if (inputPassword.Length < 6)
                {
                    Console.WriteLine("Lösenordet är för kort!");
                }
                else
                {
                    Console.WriteLine("du har klart!");
                    // Spara lösenord i användare
                    break;
                }
            }
        }
    }
}
