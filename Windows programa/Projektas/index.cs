using Newtonsoft.Json;
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

namespace Projektas
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }
        //Pradėti
        private void startButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 mainWindow = new Form1();
            mainWindow.ShowDialog();
            this.Close();
        }
        //Atsidarius programai, nuskaitomi settingsai iš json failo
        private void index_Load(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            nustatymai.Enabled = false;
            this.AcceptButton = prisijungtiButton;
        }
        /// <summary>
        /// Prisijungimo knopkė
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prisijungtiButton_Click(object sender, EventArgs e)
        {
            string enteredUsername = userText.Text;
            string enteredPass = passwordText.Text;
            //Tikrina ar įvesti teisingi duomenys
            if (enteredUsername == "admin" && enteredPass == "password")
            {
                
                errorLabel.Text = "Sėkmingai prisijungėte. Dabar galite pradėti naudotis programa.";
                errorLabel.ForeColor = Color.ForestGreen;
                nustatymai.Enabled = true;
                startButton.Enabled = true;
            }
            else
            {
                errorLabel.Text = "Neteisingi prisijungimo duomenys. Bandykite dar kartą.";
                errorLabel.ForeColor = Color.Red;
                passwordText.Text = "";
            }
        }

        private void pagalba_Click(object sender, EventArgs e)
        {

        }

        private void nustatymai_Click(object sender, EventArgs e)
        {
            settingsForm form = new settingsForm();
            form.ShowDialog();
        }

        private void username_enter(object sender, EventArgs e)
        {
            userText.Text = "";
            userText.ForeColor = Color.Black;
        }
        private void username_leave(object sender, EventArgs e)
        {
            //Vėl atsiranda tas pats užrašas (jeigu teksto laukas paliktas tuščias)
            //Nes jeigu ne tuščias, tai nenunulina, kad paspaudus knopkę įvesti nepradingtų reikšmė
            if (userText.Text == "")
            {
                userText.Text = "Username";
                userText.ForeColor = Color.Silver;
            }
        }
        private void password_enter(object sender, EventArgs e)
        {
            passwordText.Text = "";
            passwordText.PasswordChar = '*';
            passwordText.ForeColor = Color.Black;
        }
    }
}
