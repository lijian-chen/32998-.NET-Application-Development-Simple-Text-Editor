using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2_TextEditor_LijianChen
{
    class SimpleTextEditorSystem
    {
        // Path of the database - login text file
        const string loginFilePath = @"Database\Login Credentials\login.txt";

        public string LoginFilePath
        {
            get { return loginFilePath; }
        }

        /*
         * @brief Check the login file path is exist or not
         * 
         * @return bool - True if the login file is exist
         */
        public bool CheckLoginFilePath()
        {
            if (File.Exists(loginFilePath) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * @brief Get all login credentials from the database (login.txt file)
         * 
         * @return Dictionary<string, string> - A dictionary to store all login credentials (Key: Username, Value: Password)
         */
        public Dictionary<string, string> GetLoginCredentials()
        {
            // Dictionary to store the login credentials
            Dictionary<string, string> loginCredentials = new Dictionary<string, string>();

            // Read the login text file
            string[] loginFile = File.ReadAllLines(loginFilePath);
            // Loop over the login file and split it by the delimiter ','
            foreach (string text in loginFile)
            {
                string[] splittedText = text.Split(',');
                // Add the username and password to the loginInfo dictionary
                loginCredentials.Add(splittedText[0], splittedText[1]);
            }

            return loginCredentials;
        }

        /*
         * @brief Get all username from the database (login.txt file)
         * 
         * @return ArrayList - An ArrayList to store all username
         */
        public ArrayList GetAllUsername()
        {
            ArrayList allUsername = new ArrayList();

            // Read the login text file
            string[] loginFile = File.ReadAllLines(loginFilePath);
            // Loop over the login file and split it by the delimiter ','
            foreach (string text in loginFile)
            {
                string[] splittedText = text.Split(',');
                // Add the username and password to the loginInfo dictionary
                allUsername.Add(splittedText[0]);
            }

            return allUsername;
        }

        /*
         * @brief Create a new user by adding the new user information into the login text file
         * 
         * @param username - Username (unique login ID) of the new user
         * @param password - Login password of the new user
         * @param userType - User type of the new user
         * @param firstName - First name of the new user
         * @param lastName - Last name of the new user
         * @param dateOfBirth - Date of birth of the new user
         * @return void
         */
        public void CreateNewUser(string username, string password, string userType, string firstName, string lastName, string dateOfBirth)
        {
            string newUserInformation = "\n" + username + "," + password + "," + userType + "," + firstName + "," + lastName + "," + dateOfBirth;

            File.AppendAllText(loginFilePath, newUserInformation);
        }
    }

    class Users : SimpleTextEditorSystem
    {
        // User information
        private string username;
        private string password;
        private string userType;
        private string firstName;
        private string lastName;
        private string dateOfBirth;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string UserType
        {
            get { return userType; }
            set { userType = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        /*
         * @brief Get the user information based on the username
         * 
         * @param username - A string represents user's username
         * @return List<Users> - A List contains one Users object as the username is unique
         */
        public List<Users> GetUserInformation(string username)
        {
            List<Users> userInformation = new List<Users>();

            // Read the login text file
            string[] loginFile = File.ReadAllLines(LoginFilePath);
            // Loop over the login file and split it by the delimiter ','
            foreach (string text in loginFile)
            {
                string[] splittedText = text.Split(',');

                if (splittedText[0] == username)
                {
                    userInformation.Add(new Users()
                    {
                        Username = splittedText[0],
                        Password = splittedText[1],
                        UserType = splittedText[2],
                        FirstName = splittedText[3],
                        LastName = splittedText[4],
                        DateOfBirth = splittedText[5]
                    });
                }
                else
                {
                    // Skip all other users as they are username is different than the inquired one
                    continue;
                }
            }

            return userInformation;
        }
    }
}
