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
            ShowStartScreen();
        }
        private void ShowStartScreen()
        {
            while (true)
            {
                Console.WriteLine("\n* START-MENY *\n");
                Console.WriteLine("  1. Logga in");
                Console.WriteLine("  2. Skapa användarkonto");
                Console.WriteLine("  3. Avsluta programmet");

                var inputKey = Console.ReadKey();

                switch (inputKey.KeyChar)
                {
                    case '1':
                        ShowLoginScreen();
                        break;

                    case '2':
                        CreateUserAccount();
                        break;

                    case '3':
                        Console.WriteLine("\nAvslutar programmet...");
                        return;

                    default:
                        break;
                }
            }
        }

        private void ShowLoginScreen()
        {
            while (true)
            {
                Console.WriteLine("\n* LOGGA IN *\n");
                Console.Write("Skriv in ditt användarnamn: ");
                string inputUserName = Console.ReadLine();

                Console.Write("Skriv in ditt Lösenord: ");
                string inputPassword = Console.ReadLine();

                User userInDatabase = new User();
                Database db = new Database();

                userInDatabase = db.GetUserByUserName(inputUserName);

                if (userInDatabase != null)
                {
                    if (String.Equals(inputPassword, userInDatabase.GetPassword()))
                    {
                        LoggedInUser = userInDatabase;
                        ShowUserMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nFelaktigt användarnamn eller lösenord!");
                        Console.WriteLine("\nTryck på valfri tangent för att återvända...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nFelaktigt användarnamn eller lösenord!");
                    Console.WriteLine("\nTryck på valfri tangent för att återvända...");
                    Console.ReadKey();
                }
            }
        }

        public void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n* ANVÄNDARMENY - Inloggad som " + LoggedInUser.UserName + " *");

                Console.WriteLine(" 1. Skapa ett event");
                Console.WriteLine(" 2. Lista events");
                Console.WriteLine(" 3. Logga ut");

                var inputKey = Console.ReadKey();

                switch (inputKey.KeyChar)
                {
                    case '1':
                        CreateEvent();
                        break;

                    case '2':
                        ListEvents();
                        break;

                    case '3':
                        return;

                    default:
                        break;
                }
            }
        }

        public void CreateUserAccount()
        {
            // Console.Clear();
            Console.WriteLine("\n* SKAPA ANVÄNDARKONTO *");

            string inputUserName;
            string inputEmail;
            string inputPassword;

            while (true)
            {
                Console.Write("\nFyll i nytt användarnamn: ");
                inputUserName = Console.ReadLine();

                Database db1 = new Database();
                User userInDatabase = new User();
                userInDatabase = db1.GetUserByUserName(inputUserName);
                
                if (userInDatabase != null)
                {
                    Console.WriteLine("\nTyvärr, en användare med detta användarnamn finns redan, välj ett annat.");
                }
                else break;
            }

            while (true)
            {
                Console.Write("Fyll i din E-mail: ");
                inputEmail = Console.ReadLine();

                Database dbs = new Database();
                if (dbs.ExistUserWithEmail(inputEmail))
                {
                    Console.WriteLine("\nTyvärr, en användare med denna E-mailadress finns redan, välj en annan.");
                }
                else break;
            }

            while (true)
            {
                Console.Write("Fyll i lösenord: ");
                inputPassword = Console.ReadLine();
                if (inputPassword.Length < 6)
                {
                    Console.WriteLine("\nLösenordet är för kort! Välj ett lösenord med minst 6 tecken.");
                }
                else break;
            }

            User newRegisteredUser = new User();
            newRegisteredUser.UserName = inputUserName;
            newRegisteredUser.Email = inputEmail;
            newRegisteredUser.SetPassword(inputPassword);

            Database db = new Database();
            db.SaveNewUser(newRegisteredUser);
            Console.WriteLine("\nNy användare skapad!");
            Console.WriteLine("\nTryck valfri tangent för att gå tillbaks till startmenyn...");
            Console.ReadKey();
        }


        private void CreateEvent()
        {
            // Console.Clear();
            Event newEvent = new Event();

            Console.WriteLine("\n* SKAPA ETT EVENT - Inloggad som " + LoggedInUser.UserName + " *");

            Console.Write("\nEventets namn: ");
            newEvent.EventName = Console.ReadLine();
            Console.Write("Plats för eventet: ");
            newEvent.Location = Console.ReadLine();
            Console.Write("Datum (OBS! Formatet YYYY-MM-DD): ");
            newEvent.Date = Console.ReadLine();
            Console.Write("Pris för deltagande: ");
            newEvent.Price = int.Parse(Console.ReadLine());
            Console.WriteLine("Välj eventtyp: ");
            Console.WriteLine("Tryck valfri tangent för att lista eventtyper ur databas.");
            Console.ReadKey();
            ListEventTypes();
            Console.WriteLine("Välj vilken eventtyp (med siffror): ");
            newEvent.EventTypeId = int.Parse(Console.ReadLine());
            newEvent.UserId = LoggedInUser.Id;

            Console.WriteLine("\nTryck valfri tangent för att spara event i databas...");
            Console.ReadKey();

            Database db = new Database();
            db.SaveNewEvent(newEvent);
            Console.WriteLine("Eventet sparat i databasen!");
            Console.WriteLine("Tryck på valfri tangent för att återgå till användarmenyn...");
            Console.ReadKey();
        }

        private void ListEventTypes()
        {
            Database db = new Database();
            List<EventType> eventtypes = new List<EventType>();
            eventtypes = db.GetEventTypes();
            for (int i = 0; i < eventtypes.Count; i++)
            {
                Console.WriteLine(eventtypes[i].Id + ". " + eventtypes[i].TypeName);
            }
        }
        private void ListEvents()
        {
            Database db = new Database();
            List<Event> eventlist = new List<Event>();
            eventlist = db.GetAllEvents();

            Console.WriteLine("\nTryck valfri tangent för att lista alla events ur databasen...");
            Console.ReadKey();

            for (int i = 0; i < eventlist.Count; i++)
            {
                Console.WriteLine(eventlist[i].Id + ". " + eventlist[i].EventName + " : " + eventlist[i].Location + " : " + eventlist[i].Date + " : " + eventlist[i].Price + " : " + eventlist[i].EventTypeId);
            }

            Console.WriteLine("\nFör att välja ett event tryck på dess Id-nummer.");
            Console.ReadKey();
        }
    }
}
