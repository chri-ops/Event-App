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
                Console.WriteLine("\n\n* LOGGA IN *\n");
                Console.Write("Skriv in ditt användarnamn: ");
                string inputUserName = Console.ReadLine();

                Console.Write("Skriv in ditt Lösenord: ");
                string inputPassword = Console.ReadLine();

                Console.WriteLine("\n");

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
                Console.WriteLine("\n\n* ANVÄNDARMENY - Inloggad som " + LoggedInUser.UserName + " *\n");

                Console.WriteLine(" 1. Skapa ett event");
                Console.WriteLine(" 2. Lista events");
                Console.WriteLine(" 3. Logga ut");

                Console.WriteLine("\n");

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
            Console.WriteLine("Välj eventtyp: \n");
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
            Console.WriteLine("\n");

            for (int i = 0; i < eventlist.Count; i++)
            {
                // Console.WriteLine(eventlist[i].Id + ". " + eventlist[i].EventName + " : " + eventlist[i].Location + " : " + eventlist[i].Date + " : " + eventlist[i].Price + " : " + eventlist[i].EventTypeId);
                Console.WriteLine(eventlist[i].Id + ". " + eventlist[i].EventName);
            }

            Console.WriteLine("\nFör att välja ett event tryck på dess Id-nummer...");
            Console.WriteLine("Tryck 'X' för att gå tillbaks!\n");
            string inputIndex = Console.ReadLine();
            if (inputIndex == "X" || inputIndex == "x")
            {
                return;
            }
            int inputId = int.Parse(inputIndex);
            Event SelectedEvent = db.ListEventByEventId(inputId);
            // Console.WriteLine("Värde på inputId: " + inputId);
            Console.WriteLine("\n" + SelectedEvent.EventName + " / Plats: " + SelectedEvent.Location + " / Datum: " + SelectedEvent.Date + " / Pris: " + SelectedEvent.Price + " / Eventtyp: " + SelectedEvent.EventTypeFromId + " / Skapad av: " + SelectedEvent.EventCreatorByUserId);
            // Console.WriteLine("Eventtyp: ");
            //Console.Write("Visa valmeny för detta event? (J/N)");
            //var inputKey4 = Console.ReadKey();
            //if (inputKey4.KeyChar == 'J' || inputKey4.KeyChar == 'j')
            //{
            ShowSpecificEventMenu(SelectedEvent);
            //}
        }

        public void ShowSpecificEventMenu(Event SelectedEvent)
        {
            while (true)
            {
                Console.WriteLine("\n * EVENTMENY FÖR DETTA EVENT (Id: " + SelectedEvent.Id + ") *\n");
                Console.WriteLine(" 1. Deltag");
                Console.WriteLine(" 2. Visa deltagare");
                Console.WriteLine(" 3. Visa eventwall");
                // Console.WriteLine(" 4. Redigera event");
                Console.WriteLine(" 4. Ta bort event");
                Console.WriteLine(" 5. Gå tillbaks...");
                Console.WriteLine("\n");
                var inputKey = Console.ReadKey();

                switch (inputKey.KeyChar)
                {
                    case '1':
                        {
                            LoggedInUser.AttendEvent(SelectedEvent.Id, LoggedInUser.Id);
                            Console.WriteLine("\nTryck valfri tangent för att gå tillbaks till menyn...");
                            Console.ReadKey();
                            break;
                        }

                    case '2':
                        {
                            SelectedEvent.ShowParticipants();
                            Console.WriteLine("\nTryck valfri tangent för att gå tillbaks till menyn...");
                            Console.ReadKey();
                            break;
                        }
                    case '3':
                        {
                            ShowEventWall(SelectedEvent.Id);
                            Console.WriteLine("\nTryck valfri tangent för att gå tillbaks till menyn...");
                            Console.ReadKey();
                            break;
                        }
                    //case '4':
                    //    {
                    //        Console.WriteLine("Du kan fortfarande inte redigera events i denna version av programmet.");
                    //        Console.WriteLine("Tryck valfri tangent för att gå tillbaks...");
                    //        Console.ReadKey();
                    //}

                    case '4':
                        {
                            if (SelectedEvent.UserId == LoggedInUser.Id)
                            {
                                Console.WriteLine("\nÄr du säker på att du vill ta bort valt event? (J/N)");
                                var inputKey4 = Console.ReadKey();
                                if (inputKey4.KeyChar == 'J' || inputKey4.KeyChar == 'j')
                                {
                                    Database db = new Database();
                                    db.DeleteEventByEventId(SelectedEvent.Id);
                                    Console.WriteLine("\nEventet borttaget!");
                                }
                                return;
                            }
                            else
                            {
                                Console.WriteLine("\nDu är inte skapare av eventet och kan därför inte ta bort det!");
                                Console.WriteLine("Tryck valfri tangent...");
                                Console.ReadKey();
                                break;
                            }
                        }

                    case '5':
                        {
                            return;
                        }
                }
            }

            // ShowEventWall(inputId);
        }

        public void ShowEventWall(int inputId)
        {
            Database db = new Database();
            // Console.WriteLine("\nTryck 'E' för att visa eventwall (och kunna skriva in meddelande), eller valfri tangent för att gå tillbaks...");
            // var inputKey2 = Console.ReadKey();
            // if (inputKey2.KeyChar == 'E' || inputKey2.KeyChar == 'e')
            // {
                List<Message> MessageList = new List<Message>();
                MessageList = db.GetMessageListByEventId(inputId);

                Console.WriteLine("\n");
                for (int i = 0; i < MessageList.Count; i++)
                {
                    Console.WriteLine("Meddelande: " + MessageList[i].Text + " - Av: " + MessageList[i].UserNameByUserId);
                    Console.WriteLine("\n");
                }
                if (MessageList.Count == 0)
                {
                    Console.WriteLine("Inga meddelanden skrivna ännu på denna eventwall!");
                }

                Console.Write("\nVill du göra ett inlägg på eventwallen? (J/N) ");
                var inputKey3 = Console.ReadKey();
                Console.WriteLine("\n");
                if (inputKey3.KeyChar == 'J' || inputKey3.KeyChar == 'j')
                {
                    Console.Write("\nSkriv in meddelande, tryck sedan ENTER: ");
                    string inputMessage = Console.ReadLine();
                    db.AddMessageByEventIdAndUserId(inputMessage, inputId, LoggedInUser.Id);
                    Console.WriteLine("\nMeddelande sparat i eventets wall!");
                }
            // }
            //else
            //{
            //    Console.WriteLine("\n");
            //    return;
            //}
        }
    }
}
