using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class Event
    {
        public int Id;
        public string EventName;
        public string Location;
        public string Date;
        public int Price;
        public int EventTypeId;
        public int UserId;
        public string EventTypeFromId;
        public string EventCreatorByUserId;

        //public void ShowEventInformation()
        //{
        //    Console.WriteLine("Skriv kod för att visa event-information. (ej färdigkodat");
        //}

        public void ShowParticipants()
        {
            Console.WriteLine("\nVisar alla deltagare i eventet...\n");
            Database db = new Database();
            List<Participant> listOfParticipants = new List<Participant>();
            listOfParticipants = db.GetParticipantsByEventId(Id);
            for (int i = 0; i < listOfParticipants.Count; i++)
            {
                Console.WriteLine(listOfParticipants[i].UserNameFromUserId);
            }
        }
    }
}
