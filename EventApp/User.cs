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
    }
}
