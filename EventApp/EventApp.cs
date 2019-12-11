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
                Console.WriteLine("\n\n* START-MENY *\n");
                Console.WriteLine("  1. Logga in");
                Console.WriteLine("  2. Skapa användarkonto");
                Console.WriteLine("  3. Avsluta programmet");

                var inputKey = Console.ReadKey();

                switch (inputKey.KeyChar)
                {
                    case '1':
                        Console.WriteLine("\n");
                        ShowLoginScreen();
                        break;

                    case '2':
                        Console.WriteLine("\n");
                        CreateUserAccount();
                        break;

                    case '3':
                        Console.WriteLine("\n");
                        Console.WriteLine("\nAvslutar programmet...");
                        return;

                    default:
                        Console.WriteLine("\n");
                        break;
                }
            }
        }

        private void ShowLoginScreen()
        {
            while (true)
            {
                Console.WriteLine("\n\n* LOGGA IN *\n");
                Console.Write("Skriv in ditt användarnamn: ");
                string inputUserName = Console.ReadLine();

                Console.Write("Skriv in ditt Lösenord: ");
                string inputPassword = Console.ReadLine();

                Console.WriteLine("\n\n");

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
                Console.WriteLine("\n* ANVÄNDARMENY - Inloggad som " + LoggedInUser.UserName + " *\n");

                Console.WriteLine(" 1. Skapa ett event");
                Console.WriteLine(" 2. Lista events");
                Console.WriteLine(" 3. Logga ut");

                var inputKey = Console.ReadKey();

                Console.WriteLine("\n\n");

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
            Console.WriteLine("\n");

            for (int i = 0; i < eventlist.Count; i++)
            {
                // Console.WriteLine(eventlist[i].Id + ". " + eventlist[i].EventName + " : " + eventlist[i].Location + " : " + eventlist[i].Date + " : " + eventlist[i].Price + " : " + eventlist[i].EventTypeId);
                Console.WriteLine(eventlist[i].Id + ". " + eventlist[i].EventName);
            }

            Console.WriteLine("\nFör att välja ett event tryck på dess Id-nummer.");
            Console.Write("\nFör att gå tillbaka, tryck 'X': ");
            string inputIndex = Console.ReadLine();
            Console.WriteLine("\n\n");
            if (inputIndex == "X" || inputIndex == "x")
            {
                return;
            }
            int inputId = int.Parse(inputIndex);
            Event SelectedEvent = db.ListEventByEventId(inputId);
            Console.WriteLine("\n\n" + SelectedEvent.EventName + " / Plats: " + SelectedEvent.Location + " / Datum: " + SelectedEvent.Date + " / Pris: " + SelectedEvent.Price + " / Eventtyp: " + SelectedEvent.EventTypeFromId + " / Skapad av: " + SelectedEvent.EventCreatorByUserId);
            // Console.WriteLine("Eventtyp: ");
            Console.WriteLine("\nTryck 'E' för att visa eventwall (och kunna skriva in meddelande), eller valfri tangent för att gå tillbaks...");
            var inputKey2 = Console.ReadKey();
            if (inputKey2.KeyChar == 'E' || inputKey2.KeyChar == 'e')
            {
                List<Message> MessageList = new List<Message>();
                MessageList = db.GetMessageListByEventId(inputId);

                Console.WriteLine("\n\n");
                for (int i = 0; i < MessageList.Count; i++)
                {
                    Console.WriteLine(MessageList[i].Id + ". --- " + MessageList[i].Text + " --- / EventId: " + MessageList[i].EventId + " / UserId: " + MessageList[i].UserId);
                    Console.WriteLine("\n\n");
                }
                if (MessageList.Count == 0)
                {
                    Console.WriteLine("Inga meddelanden skrivna ännu på denna eventwall!");
                }

                Console.Write("\nFör att lägga till ett meddelande i denna eventwall, tryck 'L' eller valfri tangent för att gå tillbaks: ");
                var inputKey3 = Console.ReadKey();
                Console.WriteLine("\n\n");
                if (inputKey3.KeyChar == 'L' || inputKey3.KeyChar == 'l')
                {
                    Console.Write("\nSkriv in meddelande, tryck sedan ENTER: ");
                    string inputMessage = Console.ReadLine();
                    db.AddMessageByEventIdAndUserId(inputMessage, inputId, LoggedInUser.Id);
                    Console.WriteLine("\nMeddelande sparat i eventets wall!");
                    Console.WriteLine("\n\nTryck valfri tangent för att gå tillbaks...");
                    Console.ReadKey();
                    Console.WriteLine("\n\n");
                }
            }
            else
            {
                Console.WriteLine("\n\n");
                return;
            }
        }
    }
}
