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

        public void ShowEventInformation()
        {
            Console.WriteLine("Skriv kod för att visa event-information. (ej färdigkodat");
        }
    }
}
