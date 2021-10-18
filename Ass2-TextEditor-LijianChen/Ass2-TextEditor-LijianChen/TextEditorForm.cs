using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass2_TextEditor_LijianChen
{
    public partial class TextEditorForm : Form
    {
        // Custom font style enumeration 
        enum CustomFontStyle : int
        {
            Bold = 1,
            Italic = 2,
            Underline = 4,
            BoldItalic = 3,
            BoldUnderline = 5,
            ItalicUnderline = 6,
            BoldItalicUnderline = 7
        }

        // Path of the opened file
        private string openedFilePath;

        public string OpenedFilePath
        {
            get { return openedFilePath; }
            set { openedFilePath = value; }
        }

        public TextEditorForm(string loggedInUser, string userType)
        {
            InitializeComponent();

            // Write the logged-in user name, including username, first name and last name
            loggedInUserLabel.Text = "User Name: " + loggedInUser;

            // Check user type
            if (userType == "Edit") { /* No restriction for the users who are "Edit" user type */ }
            else if (userType == "View")
            {
                // Disable the text box
                richTextBox.ReadOnly = true;
                // Disable all editing menu items
                newToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;
                // Disable all editing button
                newButton.Enabled = false;
                saveButton.Enabled = false;
                saveAsButton.Enabled = false;
                cutButton.Enabled = false;
                pasteButton.Enabled = false;
                boldButton.Enabled = false;
                italicsButton.Enabled = false;
                underlineButton.Enabled = false;
            }
        }

        /*
         * @brief Menu strip item Logout to sign out from the text editor and back to the login screen
         * 
         * @return void
         */
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show(); // Show the LoginForm
            this.Hide(); // Hide the current form (TextEditorForm)
        }

        /*
         * @brief Menu strip item About to display/open the application (text editor) information
         * 
         * @return void
         */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditorAboutBox textEditorAboutBox = new TextEditorAboutBox();
            textEditorAboutBox.Show(); // Show the TextEditorAboutBox form
        }

        /*
         * @brief Menu strip item New to get a clean text editor environment
         * 
         * @return void
         */
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear(); // Clear all text from the rich text box
        }

        /*
         * @brief Menu strip item Open to open an existing txt/rtf file
         * 
         * @return void
         */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the title property
            openFileDialog.Title = "Open File";
            // Set the filter property (default set to .txt file)
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

            // Show the dialog box and get the user response
            DialogResult dialogResult = openFileDialog.ShowDialog();

            // Check the dialog result
            if (dialogResult == DialogResult.OK)
            {
                // Read all text from the file and display in the text editor
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName);

                OpenedFilePath = openFileDialog.FileName; // Set the path of the opended file
            }
        }

        /*
         * @brief Menu strip item Save to save the changes made in the opened file
         * 
         * @return void
         */
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if there is any opened file
            if (!String.IsNullOrEmpty(OpenedFilePath))
            {
                // Save the text from the text editor to the opened file
                File.WriteAllText(OpenedFilePath, richTextBox.Text);

                // Show a file saved successfully message
                MessageBox.Show("File saved successfully!\n\nFile: " + OpenedFilePath, "File Saved Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Show a message to warn the user that there is no opened file
                MessageBox.Show("Sorry, you cannot save a non-existing file! Please use the Save-As instead.", "File Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*
         * @brief Menu strip item SaveAs to save a new or existing file in the same or different location of the computer
         * 
         * @return void
         */
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog instance
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set the title property
            saveFileDialog.Title = "Save File As";
            // Set the filter property (default set to .rtf file)
            saveFileDialog.Filter = "RTF Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            // Show the dialog box and get the user response
            DialogResult dialogResult = saveFileDialog.ShowDialog();

            // Check the dialog result
            if (dialogResult == DialogResult.OK)
            {
                // Create/Save the file
                File.WriteAllText(saveFileDialog.FileName, richTextBox.Text);
            }
        }

        /*
         * @brief Menu strip item Cut to cut/move the selected text to the clipboard
         * 
         * @return void
         */
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cut the selected text from the text editor to the clipboard
            richTextBox.Cut();
        }

        /*
         * @brief Menu strip item Copy to copy the selected text to the clipboard
         * 
         * @return void
         */
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Copy the selected text from the text editor to the clipboard
            richTextBox.Copy();
        }

        /*
         * @brief Menu strip item Paste to get the cut/copied text from the clipboard
         * 
         * @return void
         */
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Paste the text from clipboard to the text editor
            richTextBox.Paste();
        }

        /*
         * @brief Bold button to change the selected text to bold style or undo the bold style
         * @Note - CustomFontStyle Enum (e.g., Bold = 1, Italic = 2, Underline = 4)
         * 
         * @return void
         */
        private void boldButton_Click(object sender, EventArgs e)
        {
            // Check if the selected text is bold style or not
            if (!(richTextBox.SelectionFont.Bold))
            {
                // Change the selected text to bold style
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Bold);
            }
            else
            {
                // Check if the selected text is only the bold (CustomFontStyle Enum: Bold = 1) style or has more than one styles
                if (richTextBox.SelectionFont.Style.GetHashCode() != (int) CustomFontStyle.Bold)
                {
                    // Check if the selected text is the bold and italic combined style (CustomFontStyle Enum: Bold + Italic = 3)
                    if (richTextBox.SelectionFont.Style.GetHashCode() == (int) CustomFontStyle.BoldItalic)
                    {
                        // Change the selected text to the italic style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Italic);
                    }
                    // Check if the selected text is the bold and underline combined style (CustomFontStyle Enum: Bold + Underline = 5)
                    else if (richTextBox.SelectionFont.Style.GetHashCode() == (int) CustomFontStyle.BoldUnderline)
                    {
                        // Change the selected text to the underline style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Underline);
                    }
                    else
                    {
                        // Change the selected text to the italic and underline combined style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Italic | FontStyle.Underline);
                    }
                }
                else
                {
                    // Change the selected text to non-bold style
                    richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular);
                }
            }
        }

        /*
         * @brief Italic button to change the selected text to italic style or undo the italic style
         * @Note - CustomFontStyle Enum (e.g., Bold = 1, Italic = 2, Underline = 4)
         * 
         * @return void
         */
        private void italicsButton_Click(object sender, EventArgs e)
        {
            // Check if the selected text is italic style or not
            if (!(richTextBox.SelectionFont.Italic))
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Italic);
            }
            else
            {
                // Check if the selected text is only the italic (CustomFontStyle Enum: Italic = 2) style or has more than one styles
                if (richTextBox.SelectionFont.Style.GetHashCode() != (int) CustomFontStyle.Italic)
                {
                    // Check if the selected text is the italic and bold combined style (CustomFontStyle Enum: Italic + Bold = 3)
                    if (richTextBox.SelectionFont.Style.GetHashCode() == (int) CustomFontStyle.BoldItalic)
                    {
                        // Change the selected text to the bold style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Bold);
                    }
                    // Check if the selected text is the italic and underline combined style (CustomFontStyle Enum: Italic + Underline = 6)
                    else if (richTextBox.SelectionFont.Style.GetHashCode() == (int) CustomFontStyle.ItalicUnderline)
                    {
                        // Change the selected text to the underline style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Underline);
                    }
                    else
                    {
                        // Change the selected text to the bold and underline combined style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
                    }
                }
                else
                {
                    // Change the selected text to non-italic style
                    richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular);
                }
            }
        }

        /*
         * @brief Underline button to change the selected text to underline style or undo the underline style
         * @Note - CustomFontStyle Enum (e.g., Bold = 1, Italic = 2, Underline = 4)
         * 
         * @return void
         */
        private void underlineButton_Click(object sender, EventArgs e)
        {
            // Check if the selected text is underline style or not
            if (!(richTextBox.SelectionFont.Underline))
            {
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style | FontStyle.Underline);
            }
            else
            {
                // Check if the selected text is only the underline (CustomFontStyle Enum: Underline = 4) style or has more than one styles
                if (richTextBox.SelectionFont.Style.GetHashCode() != (int) CustomFontStyle.Underline)
                {
                    // Check if the selected text is the underline and bold combined style (CustomFontStyle Enum: Underline + Bold = 5)
                    if (richTextBox.SelectionFont.Style.GetHashCode() == (int) CustomFontStyle.BoldUnderline)
                    {
                        // Change the selected text to the bold style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Bold);
                    }
                    // Check if the selected text is the underline and italic combined style (CustomFontStyle Enum: Underline + Italic = 6)
                    else if (richTextBox.SelectionFont.Style.GetHashCode() == (int) CustomFontStyle.ItalicUnderline)
                    {
                        // Change the selected text to the italic style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Italic);
                    }
                    else
                    {
                        // Change the selected text to the bold and italic combined style
                        richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular | FontStyle.Bold | FontStyle.Italic);
                    }
                }
                else
                {
                    // Change the selected text to non-underline style
                    richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, richTextBox.SelectionFont.Style & FontStyle.Regular);
                }
            }
        }

        /*
         * @brief Font size combo box to change the font size of the selected text (Selectable font sizes are 8 to 20, or manually type in)
         * 
         * @return void
         */
        private void fontSizeComboBox_Click(object sender, EventArgs e)
        {
            // Check the selected font size
            if (Convert.ToInt32(fontSizeComboBox.Text) < 8)
            {
                // Keep the current font size if the selected font size is less than 8
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont.FontFamily, Convert.ToInt32(richTextBox.SelectionFont.Size), richTextBox.SelectionFont.Style);
            }
            else
            {
                // Update the selected text font size
                richTextBox.SelectionFont = new Font(richTextBox.SelectionFont.FontFamily, Convert.ToInt32(fontSizeComboBox.Text), richTextBox.SelectionFont.Style);
            }
        }

        /*
         * @brief About button to display/open the application (text editor) information
         * 
         * @return void
         */
        private void aboutButton_Click(object sender, EventArgs e)
        {
            TextEditorAboutBox textEditorAboutBox = new TextEditorAboutBox();
            textEditorAboutBox.Show(); // Show the TextEditorAboutBox form
        }

        /*
         * @brief New button to get a clean text editor environment
         * 
         * @return void
         */
        private void newButton_Click(object sender, EventArgs e)
        {
            richTextBox.Clear(); // Clear all text from the rich text box
        }

        /*
         * @brief Open button to open an existing txt/rtf file
         * 
         * @return void
         */
        private void openButton_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the title property
            openFileDialog.Title = "Open File";
            // Set the filter property (default set to .txt file)
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

            // Show the dialog box and get the user response
            DialogResult dialogResult = openFileDialog.ShowDialog();

            // Check the dialog result
            if (dialogResult == DialogResult.OK)
            {
                // Read all text from the file and display in the text editor
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName);

                OpenedFilePath = openFileDialog.FileName; // Set the path of the opended file
            }
        }

        /*
         * @brief Save button to save the changes made in the opened file
         * 
         * @return void
         */
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Check if there is any opened file
            if (!String.IsNullOrEmpty(OpenedFilePath))
            {
                // Save the text from the text editor to the opened file
                File.WriteAllText(OpenedFilePath, richTextBox.Text);

                // Show a file saved successfully message
                MessageBox.Show("File saved successfully!\n\nFile: " + OpenedFilePath, "File Saved Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Show a message to warn the user that there is no opened file
                MessageBox.Show("Sorry, you cannot save a non-existing file! Please use the Save-As instead.", "File Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*
         * @brief SaveAs button to save a new or existing file in the same or different location of the computer
         * 
         * @return void
         */
        private void saveAsButton_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog instance
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set the title property
            saveFileDialog.Title = "Save File As";
            // Set the filter property (default set to .rtf file)
            saveFileDialog.Filter = "RTF Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            // Show the dialog box and get the user response
            DialogResult dialogResult = saveFileDialog.ShowDialog();

            // Check the dialog result
            if (dialogResult == DialogResult.OK)
            {
                // Create/Save the file
                File.WriteAllText(saveFileDialog.FileName, richTextBox.Text);
            }
        }

        /*
         * @brief Cut button to cut/move the selected text to the clipboard
         * 
         * @return void
         */
        private void cutButton_Click(object sender, EventArgs e)
        {
            // Cut the selected text from the text editor to the clipboard
            richTextBox.Cut();
        }

        /*
         * @brief Copy button to copy the selected text to the clipboard
         * 
         * @return void
         */
        private void copyButton_Click(object sender, EventArgs e)
        {
            // Copy the selected text from the text editor to the clipboard
            richTextBox.Copy();
        }

        /*
         * @brief Paste button to get the cut/copied text from the clipboard
         * 
         * @return void
         */
        private void pasteButton_Click(object sender, EventArgs e)
        {
            // Paste the text from clipboard to the text editor
            richTextBox.Paste();
        }
    }
}
