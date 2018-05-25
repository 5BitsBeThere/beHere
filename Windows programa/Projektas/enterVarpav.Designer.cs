namespace Projektas
{
    partial class enterVarpav
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.norimasIstrintiDarbuotojas = new System.Windows.Forms.TextBox();
            this.istrintiDarbuotoja = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(54, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Įveskite darbuotojo vardą ir pavardę";
            // 
            // norimasIstrintiDarbuotojas
            // 
            this.norimasIstrintiDarbuotojas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.norimasIstrintiDarbuotojas.Location = new System.Drawing.Point(58, 118);
            this.norimasIstrintiDarbuotojas.Name = "norimasIstrintiDarbuotojas";
            this.norimasIstrintiDarbuotojas.Size = new System.Drawing.Size(305, 27);
            this.norimasIstrintiDarbuotojas.TabIndex = 1;
            // 
            // istrintiDarbuotoja
            // 
            this.istrintiDarbuotoja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.istrintiDarbuotoja.Location = new System.Drawing.Point(150, 151);
            this.istrintiDarbuotoja.Name = "istrintiDarbuotoja";
            this.istrintiDarbuotoja.Size = new System.Drawing.Size(116, 50);
            this.istrintiDarbuotoja.TabIndex = 2;
            this.istrintiDarbuotoja.Text = "Ištrinti";
            this.istrintiDarbuotoja.UseVisualStyleBackColor = true;
            this.istrintiDarbuotoja.Click += new System.EventHandler(this.istrintiDarbuotoja_Click);
            // 
            // enterVarpav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 255);
            this.Controls.Add(this.istrintiDarbuotoja);
            this.Controls.Add(this.norimasIstrintiDarbuotojas);
            this.Controls.Add(this.label1);
            this.Name = "enterVarpav";
            this.Text = "Įvedimas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox norimasIstrintiDarbuotojas;
        private System.Windows.Forms.Button istrintiDarbuotoja;
    }
}