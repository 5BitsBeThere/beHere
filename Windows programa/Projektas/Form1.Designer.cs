namespace Projektas
{

    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.siandienButton = new System.Windows.Forms.Button();
            this.savaitesButton = new System.Windows.Forms.Button();
            this.menesioButton = new System.Windows.Forms.Button();
            this.rodytiData = new System.Windows.Forms.Button();
            this.dataTextBox = new System.Windows.Forms.TextBox();
            this.varpavTextBox = new System.Windows.Forms.TextBox();
            this.rodytiVarpav = new System.Windows.Forms.Button();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.visoLabel = new System.Windows.Forms.Label();
            this.count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.salintiButton = new System.Windows.Forms.Button();
            this.istrintiSavaite = new System.Windows.Forms.RadioButton();
            this.istrintiMenesis = new System.Windows.Forms.RadioButton();
            this.istrintiKitus = new System.Windows.Forms.RadioButton();
            this.istrintiMetai = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.visiButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pagrindinisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nustatymaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vartotojoVadovasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // siandienButton
            // 
            this.siandienButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.siandienButton.Location = new System.Drawing.Point(511, 95);
            this.siandienButton.Name = "siandienButton";
            this.siandienButton.Size = new System.Drawing.Size(224, 45);
            this.siandienButton.TabIndex = 4;
            this.siandienButton.Text = "Šios dienos sąrašas";
            this.siandienButton.UseVisualStyleBackColor = true;
            this.siandienButton.Click += new System.EventHandler(this.siandienButton_Click);
            // 
            // savaitesButton
            // 
            this.savaitesButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.savaitesButton.Location = new System.Drawing.Point(511, 146);
            this.savaitesButton.Name = "savaitesButton";
            this.savaitesButton.Size = new System.Drawing.Size(224, 45);
            this.savaitesButton.TabIndex = 5;
            this.savaitesButton.Text = "Savaitės sąrašas";
            this.savaitesButton.UseVisualStyleBackColor = true;
            this.savaitesButton.Click += new System.EventHandler(this.savaitesButton_Click);
            // 
            // menesioButton
            // 
            this.menesioButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.menesioButton.Location = new System.Drawing.Point(511, 196);
            this.menesioButton.Name = "menesioButton";
            this.menesioButton.Size = new System.Drawing.Size(224, 45);
            this.menesioButton.TabIndex = 6;
            this.menesioButton.Text = "Mėnesio sąrašas";
            this.menesioButton.UseVisualStyleBackColor = true;
            this.menesioButton.Click += new System.EventHandler(this.menesioButton_Click);
            // 
            // rodytiData
            // 
            this.rodytiData.Location = new System.Drawing.Point(652, 263);
            this.rodytiData.Name = "rodytiData";
            this.rodytiData.Size = new System.Drawing.Size(83, 28);
            this.rodytiData.TabIndex = 7;
            this.rodytiData.Text = "Rodyti";
            this.rodytiData.UseVisualStyleBackColor = true;
            this.rodytiData.Click += new System.EventHandler(this.rodytiData_Click);
            // 
            // dataTextBox
            // 
            this.dataTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.dataTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataTextBox.Location = new System.Drawing.Point(511, 264);
            this.dataTextBox.Name = "dataTextBox";
            this.dataTextBox.Size = new System.Drawing.Size(135, 24);
            this.dataTextBox.TabIndex = 8;
            this.dataTextBox.Text = "YY-MM-DD";
            this.dataTextBox.Enter += new System.EventHandler(this.Metai_enter);
            this.dataTextBox.Leave += new System.EventHandler(this.metai_leave);
            // 
            // varpavTextBox
            // 
            this.varpavTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.varpavTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.varpavTextBox.Location = new System.Drawing.Point(511, 297);
            this.varpavTextBox.Name = "varpavTextBox";
            this.varpavTextBox.Size = new System.Drawing.Size(135, 24);
            this.varpavTextBox.TabIndex = 9;
            this.varpavTextBox.Text = "Vardas, pavardė";
            this.varpavTextBox.Enter += new System.EventHandler(this.varpav_enter);
            this.varpavTextBox.Leave += new System.EventHandler(this.varpav_leave);
            // 
            // rodytiVarpav
            // 
            this.rodytiVarpav.Location = new System.Drawing.Point(652, 297);
            this.rodytiVarpav.Name = "rodytiVarpav";
            this.rodytiVarpav.Size = new System.Drawing.Size(83, 28);
            this.rodytiVarpav.TabIndex = 10;
            this.rodytiVarpav.Text = "Rodyti";
            this.rodytiVarpav.UseVisualStyleBackColor = true;
            this.rodytiVarpav.Click += new System.EventHandler(this.rodytiVarpav_Click);
            // 
            // dataView
            // 
            this.dataView.AllowUserToAddRows = false;
            this.dataView.AllowUserToDeleteRows = false;
            this.dataView.AllowUserToResizeColumns = false;
            this.dataView.AllowUserToResizeRows = false;
            this.dataView.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataView.ColumnHeadersHeight = 35;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataView.Location = new System.Drawing.Point(12, 44);
            this.dataView.Name = "dataView";
            this.dataView.RowTemplate.Height = 24;
            this.dataView.Size = new System.Drawing.Size(493, 442);
            this.dataView.TabIndex = 11;
            // 
            // visoLabel
            // 
            this.visoLabel.AutoSize = true;
            this.visoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.visoLabel.Location = new System.Drawing.Point(421, 493);
            this.visoLabel.Name = "visoLabel";
            this.visoLabel.Size = new System.Drawing.Size(46, 18);
            this.visoLabel.TabIndex = 12;
            this.visoLabel.Text = "Viso:";
            // 
            // count
            // 
            this.count.AutoSize = true;
            this.count.Location = new System.Drawing.Point(484, 492);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(0, 17);
            this.count.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(507, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Pašalinti įrašus";
            // 
            // salintiButton
            // 
            this.salintiButton.Location = new System.Drawing.Point(601, 484);
            this.salintiButton.Name = "salintiButton";
            this.salintiButton.Size = new System.Drawing.Size(82, 25);
            this.salintiButton.TabIndex = 19;
            this.salintiButton.Text = "Šalinti";
            this.salintiButton.UseVisualStyleBackColor = true;
            this.salintiButton.Click += new System.EventHandler(this.salintiButton_Click);
            // 
            // istrintiSavaite
            // 
            this.istrintiSavaite.AutoSize = true;
            this.istrintiSavaite.Location = new System.Drawing.Point(6, 54);
            this.istrintiSavaite.Name = "istrintiSavaite";
            this.istrintiSavaite.Size = new System.Drawing.Size(167, 21);
            this.istrintiSavaite.TabIndex = 18;
            this.istrintiSavaite.TabStop = true;
            this.istrintiSavaite.Text = "Senesnius nei savaitė";
            this.istrintiSavaite.UseVisualStyleBackColor = true;
            // 
            // istrintiMenesis
            // 
            this.istrintiMenesis.AutoSize = true;
            this.istrintiMenesis.Location = new System.Drawing.Point(6, 27);
            this.istrintiMenesis.Name = "istrintiMenesis";
            this.istrintiMenesis.Size = new System.Drawing.Size(174, 21);
            this.istrintiMenesis.TabIndex = 17;
            this.istrintiMenesis.TabStop = true;
            this.istrintiMenesis.Text = "Senesnius nei mėnesis";
            this.istrintiMenesis.UseVisualStyleBackColor = true;
            // 
            // istrintiKitus
            // 
            this.istrintiKitus.AutoSize = true;
            this.istrintiKitus.Location = new System.Drawing.Point(6, 81);
            this.istrintiKitus.Name = "istrintiKitus";
            this.istrintiKitus.Size = new System.Drawing.Size(137, 21);
            this.istrintiKitus.TabIndex = 20;
            this.istrintiKitus.TabStop = true;
            this.istrintiKitus.Text = "Pagal darbuotoją";
            this.istrintiKitus.UseVisualStyleBackColor = true;
            // 
            // istrintiMetai
            // 
            this.istrintiMetai.AutoSize = true;
            this.istrintiMetai.Location = new System.Drawing.Point(6, 0);
            this.istrintiMetai.Name = "istrintiMetai";
            this.istrintiMetai.Size = new System.Drawing.Size(156, 21);
            this.istrintiMetai.TabIndex = 15;
            this.istrintiMetai.TabStop = true;
            this.istrintiMetai.Text = "Senesnius nei metai";
            this.istrintiMetai.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.istrintiMetai);
            this.groupBox1.Controls.Add(this.istrintiKitus);
            this.groupBox1.Controls.Add(this.istrintiMenesis);
            this.groupBox1.Controls.Add(this.istrintiSavaite);
            this.groupBox1.Location = new System.Drawing.Point(511, 381);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 102);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // visiButton
            // 
            this.visiButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.visiButton.Location = new System.Drawing.Point(511, 44);
            this.visiButton.Name = "visiButton";
            this.visiButton.Size = new System.Drawing.Size(224, 45);
            this.visiButton.TabIndex = 22;
            this.visiButton.Text = "Visi įrašai";
            this.visiButton.UseVisualStyleBackColor = true;
            this.visiButton.Click += new System.EventHandler(this.visiButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pagrindinisToolStripMenuItem,
            this.nustatymaiToolStripMenuItem,
            this.vartotojoVadovasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(814, 28);
            this.menuStrip1.TabIndex = 23;
            // 
            // pagrindinisToolStripMenuItem
            // 
            this.pagrindinisToolStripMenuItem.Name = "pagrindinisToolStripMenuItem";
            this.pagrindinisToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
            this.pagrindinisToolStripMenuItem.Text = "Pagrindinis";
            this.pagrindinisToolStripMenuItem.Click += new System.EventHandler(this.pagrindinisToolStripMenuItem_Click);
            // 
            // nustatymaiToolStripMenuItem
            // 
            this.nustatymaiToolStripMenuItem.Name = "nustatymaiToolStripMenuItem";
            this.nustatymaiToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.nustatymaiToolStripMenuItem.Text = "Nustatymai";
            this.nustatymaiToolStripMenuItem.Click += new System.EventHandler(this.nustatymaiToolStripMenuItem_Click);
            // 
            // vartotojoVadovasToolStripMenuItem
            // 
            this.vartotojoVadovasToolStripMenuItem.Name = "vartotojoVadovasToolStripMenuItem";
            this.vartotojoVadovasToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.vartotojoVadovasToolStripMenuItem.Text = "Vartotojo vadovas";
            this.vartotojoVadovasToolStripMenuItem.Click += new System.EventHandler(this.vartotojoVadovasToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 541);
            this.Controls.Add(this.visiButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.salintiButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.count);
            this.Controls.Add(this.visoLabel);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.rodytiVarpav);
            this.Controls.Add(this.varpavTextBox);
            this.Controls.Add(this.dataTextBox);
            this.Controls.Add(this.rodytiData);
            this.Controls.Add(this.menesioButton);
            this.Controls.Add(this.savaitesButton);
            this.Controls.Add(this.siandienButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "BeHere";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button siandienButton;
        private System.Windows.Forms.Button savaitesButton;
        private System.Windows.Forms.Button menesioButton;
        private System.Windows.Forms.Button rodytiData;
        private System.Windows.Forms.TextBox dataTextBox;
        private System.Windows.Forms.TextBox varpavTextBox;
        private System.Windows.Forms.Button rodytiVarpav;
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.Label visoLabel;
        private System.Windows.Forms.Label count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button salintiButton;
        private System.Windows.Forms.RadioButton istrintiSavaite;
        private System.Windows.Forms.RadioButton istrintiMenesis;
        private System.Windows.Forms.RadioButton istrintiKitus;
        private System.Windows.Forms.RadioButton istrintiMetai;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button visiButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pagrindinisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nustatymaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vartotojoVadovasToolStripMenuItem;
    }
}

