using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class Participant
    {
        public int Id;
        public int EventId;
        public int UserId;
        public string UserNameFromUserId;
        public string EventNameFromEventId;

        public void SetParticipant(int inputEventId, int inputUserId)
        {
            EventId = inputEventId;
            UserId = inputUserId;
        }
    }
}
