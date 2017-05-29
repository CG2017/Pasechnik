namespace lab6
{
    partial class Lab6
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab6));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.invertCB = new System.Windows.Forms.CheckBox();
            this.contrastCorrectionCB = new System.Windows.Forms.CheckBox();
            this.adaptiveSmoothingCB = new System.Windows.Forms.CheckBox();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.gammaCorrectionCB = new System.Windows.Forms.CheckBox();
            this.binarizationDownTresholdCB = new System.Windows.Forms.CheckBox();
            this.binarizationJarvisCB = new System.Windows.Forms.CheckBox();
            this.thresholdTextBox = new System.Windows.Forms.TextBox();
            this.thresholdTB = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.binarizationBayerCB = new System.Windows.Forms.CheckBox();
            this.binarizationSierraCB = new System.Windows.Forms.CheckBox();
            this.otsuCB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contrastTBar = new System.Windows.Forms.TrackBar();
            this.contrastTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.smoothTBar = new System.Windows.Forms.TrackBar();
            this.smoothTBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gammaTBar = new System.Windows.Forms.TrackBar();
            this.gammaTBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linearMinTBar = new System.Windows.Forms.TrackBar();
            this.linearMinTBox = new System.Windows.Forms.TextBox();
            this.linearCB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.linearMaxTBar = new System.Windows.Forms.TrackBar();
            this.linearMaxTBox = new System.Windows.Forms.TextBox();
            this.binarizationUpThresholdCB = new System.Windows.Forms.CheckBox();
            this.iterativeCB = new System.Windows.Forms.CheckBox();
            this.bradlyCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smoothTBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMinTBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMaxTBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(12, 248);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(422, 378);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox2.Location = new System.Drawing.Point(442, 248);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(422, 378);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // invertCB
            // 
            this.invertCB.AutoSize = true;
            this.invertCB.Location = new System.Drawing.Point(12, 98);
            this.invertCB.Name = "invertCB";
            this.invertCB.Size = new System.Drawing.Size(52, 17);
            this.invertCB.TabIndex = 2;
            this.invertCB.Text = "invert";
            this.invertCB.UseVisualStyleBackColor = true;
            this.invertCB.CheckedChanged += new System.EventHandler(this.invertCB_CheckedChanged);
            // 
            // contrastCorrectionCB
            // 
            this.contrastCorrectionCB.AutoSize = true;
            this.contrastCorrectionCB.Location = new System.Drawing.Point(12, 8);
            this.contrastCorrectionCB.Name = "contrastCorrectionCB";
            this.contrastCorrectionCB.Size = new System.Drawing.Size(114, 17);
            this.contrastCorrectionCB.TabIndex = 3;
            this.contrastCorrectionCB.Text = "contrast correction";
            this.contrastCorrectionCB.UseVisualStyleBackColor = true;
            this.contrastCorrectionCB.CheckedChanged += new System.EventHandler(this.contrastCorrectionCB_CheckedChanged);
            // 
            // adaptiveSmoothingCB
            // 
            this.adaptiveSmoothingCB.AutoSize = true;
            this.adaptiveSmoothingCB.Location = new System.Drawing.Point(12, 43);
            this.adaptiveSmoothingCB.Name = "adaptiveSmoothingCB";
            this.adaptiveSmoothingCB.Size = new System.Drawing.Size(118, 17);
            this.adaptiveSmoothingCB.TabIndex = 4;
            this.adaptiveSmoothingCB.Text = "adaptive smoothing";
            this.adaptiveSmoothingCB.UseVisualStyleBackColor = true;
            this.adaptiveSmoothingCB.CheckedChanged += new System.EventHandler(this.adaptiveSmoothingCB_CheckedChanged);
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(12, 219);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(75, 23);
            this.LoadImageButton.TabIndex = 5;
            this.LoadImageButton.Text = "Load Image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // gammaCorrectionCB
            // 
            this.gammaCorrectionCB.AutoSize = true;
            this.gammaCorrectionCB.Location = new System.Drawing.Point(12, 75);
            this.gammaCorrectionCB.Name = "gammaCorrectionCB";
            this.gammaCorrectionCB.Size = new System.Drawing.Size(110, 17);
            this.gammaCorrectionCB.TabIndex = 6;
            this.gammaCorrectionCB.Text = "gamma correction";
            this.gammaCorrectionCB.UseVisualStyleBackColor = true;
            this.gammaCorrectionCB.CheckedChanged += new System.EventHandler(this.gammaCorrectionCB_CheckedChanged);
            // 
            // binarizationDownTresholdCB
            // 
            this.binarizationDownTresholdCB.AutoSize = true;
            this.binarizationDownTresholdCB.Location = new System.Drawing.Point(12, 179);
            this.binarizationDownTresholdCB.Name = "binarizationDownTresholdCB";
            this.binarizationDownTresholdCB.Size = new System.Drawing.Size(152, 17);
            this.binarizationDownTresholdCB.TabIndex = 8;
            this.binarizationDownTresholdCB.Text = "binarization down Treshold";
            this.binarizationDownTresholdCB.UseVisualStyleBackColor = true;
            this.binarizationDownTresholdCB.CheckedChanged += new System.EventHandler(this.binarizationTresholdCB_CheckedChanged);
            // 
            // binarizationJarvisCB
            // 
            this.binarizationJarvisCB.AutoSize = true;
            this.binarizationJarvisCB.Location = new System.Drawing.Point(644, 223);
            this.binarizationJarvisCB.Name = "binarizationJarvisCB";
            this.binarizationJarvisCB.Size = new System.Drawing.Size(106, 17);
            this.binarizationJarvisCB.TabIndex = 9;
            this.binarizationJarvisCB.Text = "binarization jarvis";
            this.binarizationJarvisCB.UseVisualStyleBackColor = true;
            this.binarizationJarvisCB.CheckedChanged += new System.EventHandler(this.binarizationJarvisCB_CheckedChanged);
            // 
            // thresholdTextBox
            // 
            this.thresholdTextBox.Enabled = false;
            this.thresholdTextBox.Location = new System.Drawing.Point(251, 194);
            this.thresholdTextBox.Name = "thresholdTextBox";
            this.thresholdTextBox.Size = new System.Drawing.Size(100, 20);
            this.thresholdTextBox.TabIndex = 10;
            this.thresholdTextBox.Text = "0";
            // 
            // thresholdTB
            // 
            this.thresholdTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdTB.AutoSize = false;
            this.thresholdTB.Location = new System.Drawing.Point(357, 189);
            this.thresholdTB.Maximum = 255;
            this.thresholdTB.Name = "thresholdTB";
            this.thresholdTB.Size = new System.Drawing.Size(507, 25);
            this.thresholdTB.TabIndex = 12;
            this.thresholdTB.Value = 75;
            this.thresholdTB.Scroll += new System.EventHandler(this.thresholdTB_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "threshold";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // binarizationBayerCB
            // 
            this.binarizationBayerCB.AutoSize = true;
            this.binarizationBayerCB.Location = new System.Drawing.Point(756, 223);
            this.binarizationBayerCB.Name = "binarizationBayerCB";
            this.binarizationBayerCB.Size = new System.Drawing.Size(108, 17);
            this.binarizationBayerCB.TabIndex = 18;
            this.binarizationBayerCB.Text = "binarization bayer";
            this.binarizationBayerCB.UseVisualStyleBackColor = true;
            this.binarizationBayerCB.CheckedChanged += new System.EventHandler(this.binarizationBayerCB_CheckedChanged);
            // 
            // binarizationSierraCB
            // 
            this.binarizationSierraCB.AutoSize = true;
            this.binarizationSierraCB.Location = new System.Drawing.Point(531, 223);
            this.binarizationSierraCB.Name = "binarizationSierraCB";
            this.binarizationSierraCB.Size = new System.Drawing.Size(107, 17);
            this.binarizationSierraCB.TabIndex = 19;
            this.binarizationSierraCB.Text = "binarization sierra";
            this.binarizationSierraCB.UseVisualStyleBackColor = true;
            this.binarizationSierraCB.CheckedChanged += new System.EventHandler(this.binarizationSierraCB_CheckedChanged);
            // 
            // otsuCB
            // 
            this.otsuCB.AutoSize = true;
            this.otsuCB.Location = new System.Drawing.Point(434, 223);
            this.otsuCB.Name = "otsuCB";
            this.otsuCB.Size = new System.Drawing.Size(90, 17);
            this.otsuCB.TabIndex = 20;
            this.otsuCB.Text = "adaptive otsu";
            this.otsuCB.UseVisualStyleBackColor = true;
            this.otsuCB.CheckedChanged += new System.EventHandler(this.otsuCB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "contrast factor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contrastTBar
            // 
            this.contrastTBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contrastTBar.AutoSize = false;
            this.contrastTBar.Location = new System.Drawing.Point(357, 4);
            this.contrastTBar.Maximum = 127;
            this.contrastTBar.Minimum = -127;
            this.contrastTBar.Name = "contrastTBar";
            this.contrastTBar.Size = new System.Drawing.Size(507, 25);
            this.contrastTBar.TabIndex = 22;
            this.contrastTBar.Scroll += new System.EventHandler(this.contrastTBar_Scroll);
            // 
            // contrastTBox
            // 
            this.contrastTBox.Enabled = false;
            this.contrastTBox.Location = new System.Drawing.Point(251, 6);
            this.contrastTBox.Name = "contrastTBox";
            this.contrastTBox.Size = new System.Drawing.Size(100, 20);
            this.contrastTBox.TabIndex = 21;
            this.contrastTBox.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "smooth factor";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // smoothTBar
            // 
            this.smoothTBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.smoothTBar.AutoSize = false;
            this.smoothTBar.Location = new System.Drawing.Point(357, 35);
            this.smoothTBar.Maximum = 255;
            this.smoothTBar.Name = "smoothTBar";
            this.smoothTBar.Size = new System.Drawing.Size(507, 25);
            this.smoothTBar.TabIndex = 25;
            this.smoothTBar.Value = 20;
            this.smoothTBar.Scroll += new System.EventHandler(this.smoothTBar_Scroll);
            // 
            // smoothTBox
            // 
            this.smoothTBox.Enabled = false;
            this.smoothTBox.Location = new System.Drawing.Point(251, 41);
            this.smoothTBox.Name = "smoothTBox";
            this.smoothTBox.Size = new System.Drawing.Size(100, 20);
            this.smoothTBox.TabIndex = 24;
            this.smoothTBox.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "gamma factor";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gammaTBar
            // 
            this.gammaTBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gammaTBar.AutoSize = false;
            this.gammaTBar.Location = new System.Drawing.Point(357, 68);
            this.gammaTBar.Maximum = 500;
            this.gammaTBar.Minimum = 1;
            this.gammaTBar.Name = "gammaTBar";
            this.gammaTBar.Size = new System.Drawing.Size(507, 25);
            this.gammaTBar.TabIndex = 28;
            this.gammaTBar.TickFrequency = 10;
            this.gammaTBar.Value = 220;
            this.gammaTBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // gammaTBox
            // 
            this.gammaTBox.Enabled = false;
            this.gammaTBox.Location = new System.Drawing.Point(251, 73);
            this.gammaTBox.Name = "gammaTBox";
            this.gammaTBox.Size = new System.Drawing.Size(100, 20);
            this.gammaTBox.TabIndex = 27;
            this.gammaTBox.Text = "0.1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "min";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linearMinTBar
            // 
            this.linearMinTBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linearMinTBar.AutoSize = false;
            this.linearMinTBar.Location = new System.Drawing.Point(357, 113);
            this.linearMinTBar.Maximum = 255;
            this.linearMinTBar.Name = "linearMinTBar";
            this.linearMinTBar.Size = new System.Drawing.Size(507, 25);
            this.linearMinTBar.TabIndex = 32;
            this.linearMinTBar.Value = 20;
            this.linearMinTBar.Scroll += new System.EventHandler(this.linearMinTBar_Scroll);
            // 
            // linearMinTBox
            // 
            this.linearMinTBox.Enabled = false;
            this.linearMinTBox.Location = new System.Drawing.Point(251, 118);
            this.linearMinTBox.Name = "linearMinTBox";
            this.linearMinTBox.Size = new System.Drawing.Size(100, 20);
            this.linearMinTBox.TabIndex = 31;
            this.linearMinTBox.Text = "20";
            // 
            // linearCB
            // 
            this.linearCB.AutoSize = true;
            this.linearCB.Location = new System.Drawing.Point(12, 121);
            this.linearCB.Name = "linearCB";
            this.linearCB.Size = new System.Drawing.Size(92, 17);
            this.linearCB.TabIndex = 30;
            this.linearCB.Text = "linear contrast";
            this.linearCB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(164, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "max";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linearMaxTBar
            // 
            this.linearMaxTBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linearMaxTBar.AutoSize = false;
            this.linearMaxTBar.Location = new System.Drawing.Point(357, 144);
            this.linearMaxTBar.Maximum = 255;
            this.linearMaxTBar.Name = "linearMaxTBar";
            this.linearMaxTBar.Size = new System.Drawing.Size(507, 25);
            this.linearMaxTBar.TabIndex = 35;
            this.linearMaxTBar.Value = 50;
            this.linearMaxTBar.Scroll += new System.EventHandler(this.linearMaxTBar_Scroll);
            // 
            // linearMaxTBox
            // 
            this.linearMaxTBox.Enabled = false;
            this.linearMaxTBox.Location = new System.Drawing.Point(251, 149);
            this.linearMaxTBox.Name = "linearMaxTBox";
            this.linearMaxTBox.Size = new System.Drawing.Size(100, 20);
            this.linearMaxTBox.TabIndex = 34;
            this.linearMaxTBox.Text = "50";
            // 
            // binarizationUpThresholdCB
            // 
            this.binarizationUpThresholdCB.AutoSize = true;
            this.binarizationUpThresholdCB.Location = new System.Drawing.Point(12, 197);
            this.binarizationUpThresholdCB.Name = "binarizationUpThresholdCB";
            this.binarizationUpThresholdCB.Size = new System.Drawing.Size(138, 17);
            this.binarizationUpThresholdCB.TabIndex = 37;
            this.binarizationUpThresholdCB.Text = "binarization up Treshold";
            this.binarizationUpThresholdCB.UseVisualStyleBackColor = true;
            this.binarizationUpThresholdCB.CheckedChanged += new System.EventHandler(this.binarizationDownCB_CheckedChanged);
            // 
            // iterativeCB
            // 
            this.iterativeCB.AutoSize = true;
            this.iterativeCB.Location = new System.Drawing.Point(12, 152);
            this.iterativeCB.Name = "iterativeCB";
            this.iterativeCB.Size = new System.Drawing.Size(124, 17);
            this.iterativeCB.TabIndex = 38;
            this.iterativeCB.Text = "adaptive iterative bin";
            this.iterativeCB.UseVisualStyleBackColor = true;
            this.iterativeCB.CheckedChanged += new System.EventHandler(this.iterativeCB_CheckedChanged);
            // 
            // bradlyCB
            // 
            this.bradlyCB.AutoSize = true;
            this.bradlyCB.Location = new System.Drawing.Point(330, 223);
            this.bradlyCB.Name = "bradlyCB";
            this.bradlyCB.Size = new System.Drawing.Size(98, 17);
            this.bradlyCB.TabIndex = 39;
            this.bradlyCB.Text = "adaptive bradly";
            this.bradlyCB.UseVisualStyleBackColor = true;
            this.bradlyCB.CheckedChanged += new System.EventHandler(this.bradlyCB_CheckedChanged);
            // 
            // Lab6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 638);
            this.Controls.Add(this.bradlyCB);
            this.Controls.Add(this.iterativeCB);
            this.Controls.Add(this.binarizationUpThresholdCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.linearMaxTBar);
            this.Controls.Add(this.linearMaxTBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linearMinTBar);
            this.Controls.Add(this.linearMinTBox);
            this.Controls.Add(this.linearCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gammaTBar);
            this.Controls.Add(this.gammaTBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.smoothTBar);
            this.Controls.Add(this.smoothTBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contrastTBar);
            this.Controls.Add(this.contrastTBox);
            this.Controls.Add(this.otsuCB);
            this.Controls.Add(this.binarizationSierraCB);
            this.Controls.Add(this.binarizationBayerCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.thresholdTB);
            this.Controls.Add(this.thresholdTextBox);
            this.Controls.Add(this.binarizationJarvisCB);
            this.Controls.Add(this.binarizationDownTresholdCB);
            this.Controls.Add(this.gammaCorrectionCB);
            this.Controls.Add(this.LoadImageButton);
            this.Controls.Add(this.adaptiveSmoothingCB);
            this.Controls.Add(this.contrastCorrectionCB);
            this.Controls.Add(this.invertCB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(892, 677);
            this.MinimumSize = new System.Drawing.Size(892, 677);
            this.Name = "Lab6";
            this.Text = "Lab 6";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smoothTBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMinTBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearMaxTBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox invertCB;
        private System.Windows.Forms.CheckBox contrastCorrectionCB;
        private System.Windows.Forms.CheckBox adaptiveSmoothingCB;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.CheckBox gammaCorrectionCB;
        private System.Windows.Forms.CheckBox binarizationDownTresholdCB;
        private System.Windows.Forms.CheckBox binarizationJarvisCB;
        private System.Windows.Forms.TextBox thresholdTextBox;
        private System.Windows.Forms.TrackBar thresholdTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox binarizationBayerCB;
        private System.Windows.Forms.CheckBox binarizationSierraCB;
        private System.Windows.Forms.CheckBox otsuCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar contrastTBar;
        private System.Windows.Forms.TextBox contrastTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar smoothTBar;
        private System.Windows.Forms.TextBox smoothTBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar gammaTBar;
        private System.Windows.Forms.TextBox gammaTBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar linearMinTBar;
        private System.Windows.Forms.TextBox linearMinTBox;
        private System.Windows.Forms.CheckBox linearCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar linearMaxTBar;
        private System.Windows.Forms.TextBox linearMaxTBox;
        private System.Windows.Forms.CheckBox binarizationUpThresholdCB;
        private System.Windows.Forms.CheckBox iterativeCB;
        private System.Windows.Forms.CheckBox bradlyCB;
    }
}
