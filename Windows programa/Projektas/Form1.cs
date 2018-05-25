using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json;
using Projektas.Properties;

namespace Projektas
{
    public partial class Form1 : Form
    {

        //Reikiamu nustatymu objektas
        public static SettingsClass deserializedSettings;

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Atsidarius pagrindiniam langui, paimamos settings lango reikšmės
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Nuskaitis duomenus is json failo, kurie reikalingi jungiantis prie DB
            using (StreamReader r = new StreamReader("../../Properties/settings.json"))
            {
                //Sukuriama nauja parametru klase
                SettingsClass settingsObj = new SettingsClass();
                //Nuskaitomas failas kaip stringas
                string json = r.ReadToEnd();
                //Sitas metodas pavercia nuskaityta eilute i reikiama objekta
                deserializedSettings = JsonConvert.DeserializeObject<SettingsClass>(json);

                databaseConnection = new MySqlConnection(parameters());
            }
        }
        //Parametrai, reikejo kurt kaip atskira metoda del pasiekiamumo, bbz
        static string parameters()
        {
            return "datasource=" + deserializedSettings.Datasource + ";port=" + deserializedSettings.Port +
                   ";username=" + deserializedSettings.Username + ";password=" + deserializedSettings.Password +
                   ";database=" + deserializedSettings.Database + ";SslMode=none;Convert Zero Datetime=True";
        }

        MySqlConnection databaseConnection;
        DataTable table = new DataTable();


        /// <summary>
        /// Išveda visus duomenų bazės įrašus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void visiButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Tas MySqlDataAdapter tai klasė, naudojama bendravimui su duomenų baze
                //Taigi joje rašosi ta MySql užklausa (gražina viską, o tas Darbuotojas ir Data - stulpelių headeriai vos ne, kad butu paprasčiau)
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' from lentele ORDER BY data DESC", databaseConnection);

                //Išvalo lentelę
                table.Clear();

                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                count.Text = adapter.Fill(table).ToString();

                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                dataView.DataSource = table;
                dataView.Columns[0].Width = 155;
                dataView.Columns[1].Width = 155;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Išveda einamos dienos įrašus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siandienButton_Click(object sender, EventArgs e)
        {
            try
            {
                //MySql užklausa, kuri gražina esamos dienos įrašus
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' FROM lentele WHERE DATE(Data) = CURRENT_DATE ORDER BY data DESC", databaseConnection);
                //Išvaloma lentelė
                table.Clear();

                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                count.Text = adapter.Fill(table).ToString();

                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                dataView.DataSource = table;
                dataView.Columns[0].Width = 155;
                dataView.Columns[1].Width = 155;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Išveda einamos savaitės įrašus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savaitesButton_Click(object sender, EventArgs e)
        {
            try
            {
                //MySql užklausa, kuri gražina esamos dienos įrašus
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' FROM lentele WHERE YEARWEEK(Data)=YEARWEEK(NOW()) ORDER BY data DESC", databaseConnection);

                //Išvaloma lentelė
                table.Clear();

                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                count.Text = adapter.Fill(table).ToString();

                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                dataView.DataSource = table;
                dataView.Columns[0].Width = 155;
                dataView.Columns[1].Width = 155;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Išveda einamo mėnesio įrašus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menesioButton_Click(object sender, EventArgs e)
        {
            try
            {
                //MySql užklausa, kuri gražina esamos dienos įrašus
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' FROM lentele WHERE YEAR(Data) = YEAR(CURRENT_DATE()) AND"
                        + " MONTH(Data) = MONTH(CURRENT_DATE()) ORDER BY data DESC", databaseConnection);

                //Išvaloma lentelė
                table.Clear();

                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                count.Text = adapter.Fill(table).ToString();

                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                dataView.DataSource = table;
                dataView.Columns[0].Width = 155;
                dataView.Columns[1].Width = 155;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Paieška pagal datą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rodytiData_Click(object sender, EventArgs e)
        {
            //Išsaugoma reikiama data
            string neededDate = dataTextBox.Text;
            //Vėl padaromas tas pats tekstas
            dataTextBox.Text = "YY/MM/DD";
            dataTextBox.ForeColor = Color.Silver;

            //Vėl tas pats kaip ir aukščiau: užklausa bet tas pats atvaizdavimas
            try
            {
                //MySql užklausa, tas LIKE tai ieško įrašu, kurie turi tokį stringą
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' FROM lentele WHERE data LIKE '%" + neededDate + "%' ORDER BY data DESC", databaseConnection);

                //Išvaloma lentelė
                table.Clear();

                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                count.Text = adapter.Fill(table).ToString();

                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                dataView.DataSource = table;
                dataView.Columns[0].Width = 155;
                dataView.Columns[1].Width = 155;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// Paiešką pagal vardą pavardę
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rodytiVarpav_Click(object sender, EventArgs e)
        {
            //Išsaugomas norimas surasti vardas,pavardė
            string neededVarpav = varpavTextBox.Text;
            //Vėl padaromas tas pats tekstas
            varpavTextBox.Text = "Vardas, pavardė";
            varpavTextBox.ForeColor = Color.Silver;

            //Vėl tas pats kaip ir aukščiau: užklausa bet tas pats atvaizdavimas
            try
            {
                //MySql užklausa, tas LIKE tai ieško įrašu, kurie turi tokį stringą
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' FROM lentele WHERE varpav LIKE '%" + neededVarpav + "%' ORDER BY data DESC", databaseConnection);

                if (adapter.Equals("")) { MessageBox.Show("KLAIDA"); }
                //Išvaloma lentelė
                table.Clear();

                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                count.Text = adapter.Fill(table).ToString();
                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                dataView.DataSource = table;
                dataView.Columns[0].Width = 155;
                dataView.Columns[1].Width = 155;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Paspaudus ant metų įvedimo lauko
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Metai_enter(object sender, EventArgs e)
        {
            //Paspaudus pradingsta tekstas ir pasikeičia spalva 
            dataTextBox.Text = "";
            dataTextBox.ForeColor = Color.Black;
        }
        /// <summary>
        /// Išėjus iš metų įvedimo teksto lauko
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metai_leave(object sender, EventArgs e)
        {
            //Vėl atsiranda tas pats užrašas (jeigu teksto laukas paliktas tuščias)
            //Nes jeigu ne tuščias, tai nenunulina, kad paspaudus knopkę įvesti nepradingtų reikšmė
            if (dataTextBox.Text == "")
            {
                dataTextBox.Text = "YY-MM-DD";
                dataTextBox.ForeColor = Color.Silver;
            }
        }

        /// <summary>
        /// Paspaudus ant vardo,pavardės teksto įvedimo lauko
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void varpav_enter(object sender, EventArgs e)
        {
            //Paspaudus pradingsta tekstas ir pasikeičia spalva 
            varpavTextBox.Text = "";
            varpavTextBox.ForeColor = Color.Black;
        }
        /// <summary>
        /// Išėjus iš metų įvedimo teksto lauko
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void varpav_leave(object sender, EventArgs e)
        {
            //Vėl atsiranda tas pats užrašas (jeigu teksto laukas paliktas tuščias)
            //Nes jeigu ne tuščias, tai nenunulina, kad paspaudus knopkę įvesti nepradingtų reikšmė
            if (varpavTextBox.Text == "")
            {
                varpavTextBox.Text = "Vardas, pavardė";
                varpavTextBox.ForeColor = Color.Silver;
            }
        }


        /// <summary>
        /// Paspaudus knopke Šalinti, surandama kuris RadioBoxas užžymėtas ir pagal tai atliekami veiksmai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salintiButton_Click(object sender, EventArgs e)
        {
            //Pirmiausia dariau kad atliktų veiksmus, kai paspaudžia pagal darbuotoją, nes čia biški sudėtingiau
            if (istrintiKitus.Checked)
            {
                //varpav tai čia norimo ištrinti darbuotojo vardas, pavardė
                string vardasPavarde;
                istrintiKitus.Checked = false;
                //Sukuriamas naujas darbuotojo įvedimo langas
                enterVarpav enterForm = new enterVarpav();
                //Tikrinama ar forma buvo įvykdyta (t.y. įvestas vardas ir pan)
                if (enterForm.ShowDialog() == DialogResult.OK)
                {
                    //Priskiriama įvedimo formos textbox reikšmė
                    vardasPavarde = enterForm.darbuotojoVarpav;

                    //Dabar jau galima atlikti šalinimą
                    try
                    {
                        //Čia nesugalvojau kaip padaryt, kad ištrintų ir iškart updeitintų tą tablę
                        //Todėl padariau pirmiausia kad įvykdytų užklausą su DataReader
                        //Tai tas query yra užklausa kad ištrintų įrašus, kurių varpav atitinka įvestą
                        MySqlCommand query = new MySqlCommand("DELETE FROM `lentele` WHERE varpav = '" + vardasPavarde + "'", databaseConnection);
                        //Naudojant DataReader reikia atidaryti ir uždaryti connectioną
                        databaseConnection.Open();
                        //Čia executina tą užklausą
                        MySqlDataReader execute = query.ExecuteReader();
                        databaseConnection.Close();

                        //Čia tas adapter tiesiog paima visus įrašus su MySql užklausa (aukščiau ištrynė, todėl čia jau nebebus tų įrašų, kurių nereikia)
                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' from lentele ORDER BY data DESC", databaseConnection);

                        //Išvaloma lentelė
                        table.Clear();
                        //Čia tikrinimui, ar buvo pašalintas koks darbuotojas
                        int previousCount = Convert.ToInt32(count.Text);

                        //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                        count.Text = adapter.Fill(table).ToString();
                        int newCount = Convert.ToInt32(count.Text);
                        //Jeigu pirmais ir dabar countai buvo vienodi, reiškias nei vienas nebuvo pašalintas. Smart
                        if (previousCount == newCount)
                            MessageBox.Show("Toks darbuotojas nerastas");
                        else MessageBox.Show("Darbuotojo įrašai pašalinti.");


                        //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                        dataView.DataSource = table;
                        dataView.Columns[0].Width = 155;
                        dataView.Columns[1].Width = 155;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
                //Išmeta pranešimą
                DialogResult dr = MessageBox.Show("Ar tikrai norite pašalinti įrašus?",
                                      "Pranešimas", MessageBoxButtons.YesNo);
                //Čia if case sakinys, jeigu Yes paspaudi, tai atlieka veiksmus, jei No, tai tiesiog uždaro pranešimą
                switch (dr)
                {
                    case DialogResult.Yes:
                        //Tikrina RadioBoxus atskirai
                        //Jeigu buvo paspaustas senesnių įrašų nei mėnesis radioBoxas
                        if (istrintiMetai.Checked)
                        {
                            //Iškart padarau kad nusiimtų tas check'as
                            istrintiMetai.Checked = false;
                            try
                            {
                                //Čia nesugalvojau kaip padaryt, kad ištrintų ir iškart updeitintų tą tablę
                                //Todėl padariau pirmiausia kad įvykdytų užklausą su DataReader
                                //Tai tas query yra užklausa kad ištrintų įrašus, senesnius nei 365 dienos
                                MySqlCommand query = new MySqlCommand("DELETE FROM `lentele` WHERE `data` < CURDATE() - INTERVAL 365 DAY", databaseConnection);
                                //Naudojant DataReader reikia atidaryti ir uždaryti connectioną
                                databaseConnection.Open();
                                //Čia executina tą užklausą
                                MySqlDataReader execute = query.ExecuteReader();
                                databaseConnection.Close();
                                //Čia tas adapter tiesiog paima visus įrašus su MySql užklausa (aukščiau ištrynė, todėl čia jau nebebus tų įrašų, kurių nereikia)
                                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' from lentele ORDER BY data DESC", databaseConnection);

                                //Išvaloma lentelė
                                table.Clear();

                                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                                count.Text = adapter.Fill(table).ToString();

                                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                                dataView.DataSource = table;
                                dataView.Columns[0].Width = 155;
                                dataView.Columns[1].Width = 155;

                                MessageBox.Show("Įrašai pašalinti.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (istrintiMenesis.Checked)
                        {
                            istrintiMenesis.Checked = false;

                            try
                            {
                                //Čia toliau tiem kitiem RadioBoxam viskas tas pats, todėl nebekomentuosiu per daug :D 
                                MySqlCommand query = new MySqlCommand("DELETE FROM `lentele` WHERE `data` < CURDATE() - INTERVAL 31 DAY", databaseConnection);
                                databaseConnection.Open();
                                MySqlDataReader execute = query.ExecuteReader();
                                databaseConnection.Close();
                                //Gražina vėl visą lentelę
                                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' from lentele ORDER BY data DESC", databaseConnection);

                                //Išvaloma lentelė
                                table.Clear();

                                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                                count.Text = adapter.Fill(table).ToString();

                                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                                dataView.DataSource = table;
                                dataView.Columns[0].Width = 155;
                                dataView.Columns[1].Width = 155;

                                MessageBox.Show("Įrašai pašalinti.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (istrintiSavaite.Checked)
                        {
                            istrintiSavaite.Checked = false;

                            try
                            {
                                //Vėl viskas tas pats, tik intervalas keičias
                                MySqlCommand query = new MySqlCommand("DELETE FROM `lentele` WHERE `data` < CURDATE() - INTERVAL 7 DAY", databaseConnection);
                                databaseConnection.Open();
                                MySqlDataReader execute = query.ExecuteReader();
                                databaseConnection.Close();
                                //MySql užklausa, gražina visą lentelę
                                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT varpav as 'Darbuotojas', data as 'Data' from lentele ORDER BY data DESC", databaseConnection);

                                //Išvaloma lentelė
                                table.Clear();

                                //Duombazės duomenys įdedami į tą datatable ir grąžinamas eilučių skaičius. Tai jis ir yra total
                                count.Text = adapter.Fill(table).ToString();

                                //DataGridView komponentui liepiama rodyti lentelę, kuri jau užpildyta duomenimis
                                dataView.DataSource = table;
                                dataView.Columns[0].Width = 155;
                                dataView.Columns[1].Width = 155;

                                MessageBox.Show("Įrašai pašalinti.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;

                    case DialogResult.No:
                        break;
                }
            }
        }
        /// <summary>
        /// Paspaudus tarp meniu "Pagrindinis", atidaro tą titulinį langą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagrindinisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            index newform = new index();
            newform.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// Atidaro nustatymų langą, ir paskui perskaito duomenis iš json failo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nustatymaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm newform = new settingsForm();
            newform.ShowDialog();

            //Nuskaitis duomenus is json failo, kurie reikalingi jungiantis prie DB
            using (StreamReader r = new StreamReader("../../Properties/settings.json"))
            {
                //Sukuriama nauja parametru klase
                SettingsClass settingsObj = new SettingsClass();
                //Nuskaitomas failas kaip stringas
                string json = r.ReadToEnd();
                //Sitas metodas pavercia nuskaityta eilute i reikiama objekta
                deserializedSettings = JsonConvert.DeserializeObject<SettingsClass>(json);

                databaseConnection = new MySqlConnection(parameters());
            }
        }
        /// <summary>
        /// Bus padarytas vartotojo vadovo langas, kurį šita knopkė ir atidaris
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vartotojoVadovasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




    }
}
