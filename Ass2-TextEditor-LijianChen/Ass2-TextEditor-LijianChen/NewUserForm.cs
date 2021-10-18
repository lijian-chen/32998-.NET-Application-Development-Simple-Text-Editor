using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2_TextEditor_LijianChen
{
    public partial class NewUserForm : Form
    {
        LoginForm loginForm = new LoginForm();

        public NewUserForm()
        {
            InitializeComponent();
        }

        /*
         * @brief Cancel button to cancel the new user registration
         * 
         * @return void
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            loginForm.Show(); // Show the LoginForm
            this.Hide(); // Hide the current form (NewUserForm)
        }

        /*
         * @brief Submit button to finish the new user registration
         * 
         * @return void
         */
        private void submitButton_Click(object sender, EventArgs e)
        {
            SimpleTextEditorSystem STES = new SimpleTextEditorSystem();

            // ArrayList to store all username in the login database (login.txt file)
            ArrayList allUsername = new ArrayList();

            allUsername = STES.GetAllUsername(); // Get all username and stored into the allUsername ArrayList

            // Check if the username input by the user is unique and ensure the username is not empty
            if (!allUsername.Contains(usernameTextBox.Text) && !String.IsNullOrEmpty(usernameTextBox.Text))
            {
                // Check if the entered password matches the re-entered password and ensure the password is not empty
                if (!String.IsNullOrEmpty(passwordTextBox.Text) && passwordTextBox.Text == reEnterPasswordTextBox.Text)
                {
                    // Check if other fields (first name, last name, user type) are empty or not
                    if (!String.IsNullOrEmpty(firstNameTextBox.Text) && !String.IsNullOrEmpty(lastNameTextBox.Text) && !String.IsNullOrEmpty(userTypeComboBox.Text))
                    {
                        // Create a new user in the Simple Text Editor System
                        STES.CreateNewUser(usernameTextBox.Text, passwordTextBox.Text, userTypeComboBox.Text, firstNameTextBox.Text, lastNameTextBox.Text, dateOfBirthDateTimePicker.Text);
                        
                        // Show a new user successfully registered message and ask if the user would like to do a new registration
                        DialogResult dialogResult = MessageBox.Show("New user " + usernameTextBox.Text + " successfully registered!\n\nAnother new user registration?", "New User Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            // Clear/Reset the previous registration input
                            usernameTextBox.Clear();
                            passwordTextBox.Clear();
                            reEnterPasswordTextBox.Clear();
                            firstNameTextBox.Clear();
                            lastNameTextBox.Clear();
                            dateOfBirthDateTimePicker.ResetText();
                            userTypeComboBox.ResetText();
                        }
                        else
                        {
                            loginForm.Show(); // Show the LoginForm
                            this.Hide(); // Hide the current form (NewUserForm)
                        }
                    }
                    else
                    {
                        ShowInvalidRegistrationMessage("FirstName/LastName/DateOfBirth/User-Type field cannot be empty!");
                    }
                }
                else
                {
                    ShowInvalidRegistrationMessage("Invalid password!");
                }
            }
            else
            {
                ShowInvalidRegistrationMessage("Invalid username!");
            }
        }

        /*
         * @brief Show a message box with a proper warning message based on different types of invalid registration
         * 
         * @param message - A string to contain a warning message for different types of invalid registration
         * @return void
         */
        private void ShowInvalidRegistrationMessage(string message)
        {
            // Show the message box with the retry and cancel buttons
            DialogResult dialogResult = MessageBox.Show(message, "New User Registration Verification", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.Retry) { /* Retry the new user registration */ }
            else
            {
                loginForm.Show(); // Show the LoginForm
                this.Hide(); // Hide the current form (NewUserForm)
            }
        }
    }
}
