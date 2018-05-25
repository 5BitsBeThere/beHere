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
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();
        }
        //Išsaugoti mygtuko atliekami veiksmai
        private void button1_Click(object sender, EventArgs e)
        {
            //Priskiriamos textbox laukų reikšmės

            //Kadangi įvedamos naujos reikšmės, reikia jas išsaugoti į failą
            //Tam sukuriamas naujas objektas, parametrams priskiriamos reikšmės
            //Ir duomenys įrašomi į json failą
            SettingsClass newSettings = new SettingsClass();
            newSettings.Datasource = datasourceTextBox.Text;
            newSettings.Port = portTextBox.Text;
            newSettings.Username = usernameTextBox.Text;
            newSettings.Password = passwordTextBox.Text;
            newSettings.Database = databaseTextBox.Text;
            //Šitas objektą konvertuoją į stringą, kuris ir įrašomas į failą
            string json = JsonConvert.SerializeObject(newSettings);
            System.IO.File.WriteAllText("../../Properties/settings.json", json);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            //Kai užkraunamas nustatymų langas, reikia kad rodytu reikšmes, kurios yra tarp failo
            using (StreamReader r = new StreamReader("../../Properties/settings.json"))
            {
                SettingsClass settingsObj = new SettingsClass();
                //Eilutės nuskaitymas
                string json = r.ReadToEnd();
                //Eilutė keičiama į objektą pagal parametrus
                SettingsClass deserializedSettings = JsonConvert.DeserializeObject<SettingsClass>(json);
                //Textboxam priskiriamos reikšmės, kurios buvo tarp failo
                datasourceTextBox.Text = deserializedSettings.Datasource;
                portTextBox.Text = deserializedSettings.Port;
                usernameTextBox.Text = deserializedSettings.Username;
                passwordTextBox.Text = deserializedSettings.Password;
                databaseTextBox.Text = deserializedSettings.Database;
            }
        }
    }
}
