using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp
{
    class User
    {
        public int Id;
        public string Username;
        private string password;

        public bool CheckPassword(string Password)
        {
            bool isPasswordValid;
            if(Password == password)
            {
                isPasswordValid = true;
            }
            else
            {
                isPasswordValid = false;
            }
            return isPasswordValid;
        }
    }
}
