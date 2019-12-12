using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class User
    {
        public int Id;
        public string UserName;
        public string Email;
        private string Password;


        public bool CheckPassword(string password)
        {
            bool isPasswordValid;
            if(password == Password)
            {
                isPasswordValid = true;
            }
            else
            {
                isPasswordValid = false;
            }
            return isPasswordValid;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public string GetPassword()
        {
            return Password;
        }

        public void AttendEvent(int EventId, int UserId)
        {
            Database db = new Database();

            if (db.IsAlreadyParticipating(UserId, EventId))
            {
                Console.WriteLine("\nDu deltar redan i detta event!");
            }
            else
            {
                Participant newParticipant = new Participant();
                newParticipant.SetParticipant(EventId, UserId);
                Database db3 = new Database();
                db.SaveNewParticipant(newParticipant);
                Console.WriteLine("\n\nDu är nu sparad som deltagare i detta event!");
            }
        }
    }
}
