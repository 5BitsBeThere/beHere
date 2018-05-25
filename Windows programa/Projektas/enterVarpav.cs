using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projektas
{
    public partial class enterVarpav : Form
    {
        //Padaromas public kintamasis, kad kita forma pasiektų
        public string darbuotojoVarpav { get; set;  }
        public enterVarpav()
        {
            InitializeComponent();
        }

        private void istrintiDarbuotoja_Click(object sender, EventArgs e)
        {
            //Priskiriama jam TextBox reikšmė
            darbuotojoVarpav = norimasIstrintiDarbuotojas.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
