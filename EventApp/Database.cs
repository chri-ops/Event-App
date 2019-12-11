using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EventApp
{
    class Database
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=EventDatabase;Integrated Security=True";

        public List<User> GetAllUsers()
        {
            string sqlQuery = "SELECT * FROM [User]"; // Query

            List<User> users = new List<User>(); // Ny lista av användare

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling databas
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query

                myConnection.Open(); // Öppna koppling

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    while (dataReader.Read()) // Läs svar (alla rader)
                    {
                        User user = new User(); // skapa nytt User-objekt

                        user.Id = int.Parse(dataReader["Id"].ToString()); // Sätt Id från databas
                        user.UserName = dataReader["Username"].ToString(); // Sätt Username från databas
                        user.SetPassword(dataReader["Password"].ToString()); // Sätt Password från databas
                        users.Add(user); // Lägg till användare till listan
                    }

                    myConnection.Close(); // Stäng uppkopplingen till db
                }
            }
            return users; // Returnera lista på alla användare i databasen
        }

        /// <summary>
        /// Returnerar första användaren i databasen med matchande namn.
        /// </summary>
        /// <param name="username">Användarnamn att matcha med i databasen</param>
        /// <returns>A User</returns>
        public User GetUserByUserName(string username)
        {
            string sqlQuery = "SELECT * FROM [User] WHERE [Username] LIKE @username"; // Query

            User user = null; // Skapa en ny användare med tomt värde

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query med databas

                sqlCommand.Parameters.AddWithValue("@username", username); // Lägg till sökt användarnamn i query

                myConnection.Open(); // Öppna uppkoppling till databas

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    if (dataReader.Read()) // Läs svar från databas (första rad)
                    {
                        user = new User(); // create new User object

                        user.Id = int.Parse(dataReader["Id"].ToString()); // Sätt User Id från databas
                        user.UserName = dataReader["Username"].ToString(); // Sätt Username från databas
                        user.SetPassword(dataReader["Password"].ToString()); // Sätt password från databas
                    }

                    myConnection.Close(); // Stäng uppkoppling till dadabas
                }
            }

            return user; // Returnerar användaren (om hittad - annars null)
        }

        public void SaveNewUser(User newUser)
        {
            string sqlQuery = "INSERT INTO [User] ([UserName], [E-mail], [Password]) VALUES (@usernamevalue, @emailvalue, @passwordvalue)"; // Query

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query med databas

                sqlCommand.Parameters.AddWithValue("@usernamevalue", newUser.UserName); // Lägg till Username i query
                sqlCommand.Parameters.AddWithValue("@emailvalue", newUser.Email); // Lägg till Email i query
                sqlCommand.Parameters.AddWithValue("@passwordvalue", newUser.GetPassword()); // Lägg till Password i query

                myConnection.Open(); // Öppna uppkoppling till databas

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    myConnection.Close(); // Stäng uppkoppling till dadabas
                }
            }
        }

        public void SaveNewEvent(Event NewEvent)
        {
            string sqlQuery = "INSERT INTO [Event] ([EventName], [Location], [Date], [Price], [EventTypeId], [UserId]) VALUES (@value1, @value2, @value3, @value4, @value5, @value6)"; // Query

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query med databas

                sqlCommand.Parameters.AddWithValue("@value1", NewEvent.EventName); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value2", NewEvent.Location); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value3", NewEvent.Date); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value4", NewEvent.Price); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value5", NewEvent.EventTypeId); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value6", NewEvent.UserId); // Lägg till value i query


                myConnection.Open(); // Öppna uppkoppling till databas

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    myConnection.Close(); // Stäng uppkoppling till dadabas
                }
            }
        }







        public bool ExistUserWithEmail(string email)
        {
            bool exist = false;
            string sqlQuery = "SELECT * FROM [User] WHERE [E-mail] LIKE @email"; // Query

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query med databas

                sqlCommand.Parameters.AddWithValue("@email", email); // Lägg till sökt E-mail i query

                myConnection.Open(); // Öppna uppkoppling till databas

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    if (dataReader.Read()) // Läs svar från databas (första rad)
                    {
                        exist = true;
                    }

                    myConnection.Close(); // Stäng uppkoppling till dadabas
                }
            }

            return exist; // Returnerar sannt om det finns användare med detta E-mail
        }

        public List<EventType> GetEventTypes()
        {
            List<EventType> eventtypes = new List<EventType>();

            string sqlQuery = "SELECT * FROM [EventType]"; // Query

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query med databas

                myConnection.Open(); // Öppna uppkoppling till databas

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    while (dataReader.Read()) // Läs svar (alla rader)
                    {
                        EventType eventtype = new EventType(); // skapa nytt User-objekt

                        eventtype.TypeName = dataReader["TypeName"].ToString(); // Sätt TypeName från databas
                        eventtype.Id = int.Parse(dataReader["Id"].ToString());
                        eventtypes.Add(eventtype); // Lägg till eventtyp till listan
                    }
                }
            }

            return eventtypes;
        }

        public List<Event> GetAllEvents()
        {
            string sqlQuery = "SELECT * FROM [Event]"; // Query

            List<Event> eventlist = new List<Event>(); // Ny lista av användare

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling databas
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query

                myConnection.Open(); // Öppna koppling

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    while (dataReader.Read()) // Läs svar (alla rader)
                    {
                        Event Event = new Event(); // skapa nytt Event-objekt

                        Event.Id = int.Parse(dataReader["Id"].ToString()); // Sätt Id från databas
                        Event.EventName = dataReader["EventName"].ToString(); // Sätt EventName från databas
                        Event.Location = dataReader["Location"].ToString(); // Sätt Location från databas
                        Event.Date = dataReader["Date"].ToString(); // Sätt Date från databas
                        Event.Price = int.Parse(dataReader["Price"].ToString()); // Sätt Price från databas
                        Event.EventTypeId = int.Parse(dataReader["EventTypeId"].ToString()); // Sätt EventTypeId från databas
                        Event.UserId = int.Parse(dataReader["UserId"].ToString()); // Sätt UserId från databas

                        eventlist.Add(Event); // Lägg till användare till listan
                    }

                    myConnection.Close(); // Stäng uppkopplingen till db
                }
            }
            return eventlist; // Returnera lista på alla event i databasen
        }

        public Event ListEventByEventId(int inputId)
        {
            string sqlQuery = "SELECT [Event].EventName, [Event].[Location], [Event].[Date], [Event].[Price], [User].[Username], [EventType].[TypeName] FROM [Event] LEFT JOIN [User] ON [User].[Id] = [Event].[UserId] LEFT JOIN [EventType] ON [Event].[EventTypeId] = [EventType].[Id] WHERE [Event].[Id] = @value1"; // Query

            // --SELECT * FROM[User]
            //-- LEFT JOIN[Role] ON[User].[RoleId] = [Role].[Id]
            //--SELECT* FROM[Role];

            Event Event = new Event(); // Objekt för att hämta event

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling databas
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query

                sqlCommand.Parameters.AddWithValue("@value1", inputId); // Lägg till value i query

                myConnection.Open(); // Öppna koppling

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    if (dataReader.Read()) // Läs svar (alla rader)
                    {
                        // Event.Id = int.Parse(dataReader["Id"].ToString()); // Sätt Id från databas
                        Event.EventName = dataReader["EventName"].ToString(); // Sätt EventName från databas
                        Event.Location = dataReader["Location"].ToString(); // Sätt Location från databas
                        Event.Date = Convert.ToDateTime(dataReader["Date"]).ToString("yyyy/MM/dd"); // Sätt Date från databas
                        Event.Price = int.Parse(dataReader["Price"].ToString()); // Sätt Price från databas
                        // Convert.ToDateTime(MyReader["DateField"]).ToString("dd/MM/yyyy");                   
                        Event.EventTypeFromId = dataReader["TypeName"].ToString(); // Sätt EventTypeId från databas
                        Event.EventCreatorByUserId = dataReader["UserName"].ToString(); // Sätt UserId från databas
                    }

                    myConnection.Close(); // Stäng uppkopplingen till db
                }
            }
            return Event; // Returnerar valt event
        }

        public List<Message> GetMessageListByEventId(int inputId)
        {
            string sqlQuery = "SELECT * FROM [Message] WHERE EventId = @value1"; // Query

            List<Message> MessageList = new List<Message>(); // Objekt för att hämta event

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling databas
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query

                sqlCommand.Parameters.AddWithValue("@value1", inputId); // Lägg till value i query

                myConnection.Open(); // Öppna koppling

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    while (dataReader.Read()) // Läs svar (alla rader)
                    {
                        Message ChosenMessage = new Message();
                        ChosenMessage.Id = int.Parse(dataReader["Id"].ToString()); // Sätt Id från databas
                        ChosenMessage.Text = dataReader["Message"].ToString(); // Sätt Text från databas
                        ChosenMessage.UserId = int.Parse(dataReader["UserId"].ToString()); // Sätt UserId från databas
                        ChosenMessage.EventId = int.Parse(dataReader["EventId"].ToString()); // Sätt EventId från databas
                        MessageList.Add(ChosenMessage);
                    }

                    myConnection.Close(); // Stäng uppkopplingen till db
                }
            }
            return MessageList; // Returnerar valt event
        }

        public void AddMessageByEventIdAndUserId(string inputMessage, int EventId, int UserId)
        {
            string sqlQuery = "INSERT INTO [Message] ([Message], [EventId], [UserId]) VALUES (@value1, @value2, @value3)"; // Query

            using (SqlConnection myConnection = new SqlConnection(connectionString)) // Förbered uppkoppling databas
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, myConnection); // Förbered query

                sqlCommand.Parameters.AddWithValue("@value1", inputMessage); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value2", EventId); // Lägg till value i query
                sqlCommand.Parameters.AddWithValue("@value3", UserId); // Lägg till value i query

                myConnection.Open(); // Öppna koppling

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Kör query
                {
                    if (dataReader.Read()) // Läs svar (alla rader)
                    {

                    }

                    myConnection.Close(); // Stäng uppkopplingen till db
                }
            }
        }
    }
}

