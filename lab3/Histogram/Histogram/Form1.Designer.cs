namespace Histogram
{
    partial class Histogram
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Histogram));
            this.myPictureBox = new System.Windows.Forms.PictureBox();
            this.myZedGraphControl = new ZedGraph.ZedGraphControl();
            this.loadButton = new System.Windows.Forms.Button();
            this.isParallelCB = new System.Windows.Forms.CheckBox();
            this.isSmooth = new System.Windows.Forms.CheckBox();
            this.lineBarCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // myPictureBox
            // 
            this.myPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.myPictureBox.Location = new System.Drawing.Point(12, 12);
            this.myPictureBox.Name = "myPictureBox";
            this.myPictureBox.Size = new System.Drawing.Size(424, 586);
            this.myPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.myPictureBox.TabIndex = 0;
            this.myPictureBox.TabStop = false;
            this.myPictureBox.Click += new System.EventHandler(this.myPictureBox_Click);
            // 
            // myZedGraphControl
            // 
            this.myZedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myZedGraphControl.Location = new System.Drawing.Point(442, 12);
            this.myZedGraphControl.Name = "myZedGraphControl";
            this.myZedGraphControl.ScrollGrace = 0D;
            this.myZedGraphControl.ScrollMaxX = 0D;
            this.myZedGraphControl.ScrollMaxY = 0D;
            this.myZedGraphControl.ScrollMaxY2 = 0D;
            this.myZedGraphControl.ScrollMinX = 0D;
            this.myZedGraphControl.ScrollMinY = 0D;
            this.myZedGraphControl.ScrollMinY2 = 0D;
            this.myZedGraphControl.Size = new System.Drawing.Size(1004, 615);
            this.myZedGraphControl.TabIndex = 1;
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadButton.Location = new System.Drawing.Point(12, 604);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load Image";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // isParallelCB
            // 
            this.isParallelCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isParallelCB.AutoSize = true;
            this.isParallelCB.Checked = true;
            this.isParallelCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isParallelCB.Location = new System.Drawing.Point(93, 608);
            this.isParallelCB.Name = "isParallelCB";
            this.isParallelCB.Size = new System.Drawing.Size(59, 17);
            this.isParallelCB.TabIndex = 3;
            this.isParallelCB.Text = "parallel";
            this.isParallelCB.UseVisualStyleBackColor = true;
            this.isParallelCB.CheckedChanged += new System.EventHandler(this.isParallelCB_CheckedChanged);
            // 
            // isSmooth
            // 
            this.isSmooth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isSmooth.AutoSize = true;
            this.isSmooth.Checked = true;
            this.isSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isSmooth.Location = new System.Drawing.Point(158, 608);
            this.isSmooth.Name = "isSmooth";
            this.isSmooth.Size = new System.Drawing.Size(60, 17);
            this.isSmooth.TabIndex = 4;
            this.isSmooth.Text = "smooth";
            this.isSmooth.UseVisualStyleBackColor = true;
            this.isSmooth.CheckedChanged += new System.EventHandler(this.isSmooth_CheckedChanged);
            // 
            // lineBarCB
            // 
            this.lineBarCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lineBarCB.AutoSize = true;
            this.lineBarCB.Checked = true;
            this.lineBarCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lineBarCB.Location = new System.Drawing.Point(224, 608);
            this.lineBarCB.Name = "lineBarCB";
            this.lineBarCB.Size = new System.Drawing.Size(65, 17);
            this.lineBarCB.TabIndex = 5;
            this.lineBarCB.Text = "line | bar";
            this.lineBarCB.UseVisualStyleBackColor = true;
            this.lineBarCB.CheckedChanged += new System.EventHandler(this.lineBarCB_CheckedChanged);
            // 
            // Histogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 639);
            this.Controls.Add(this.lineBarCB);
            this.Controls.Add(this.isSmooth);
            this.Controls.Add(this.isParallelCB);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.myZedGraphControl);
            this.Controls.Add(this.myPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Histogram";
            this.Text = "Histogram";
            ((System.ComponentModel.ISupportInitialize)(this.myPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox myPictureBox;
        private ZedGraph.ZedGraphControl myZedGraphControl;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.CheckBox isParallelCB;
        private System.Windows.Forms.CheckBox isSmooth;
        private System.Windows.Forms.CheckBox lineBarCB;
    }
}

