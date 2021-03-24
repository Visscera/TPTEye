namespace ThePowderToyFilterColorTool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.picColors = new System.Windows.Forms.PictureBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtCurrentColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtCurrentColorHex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picColorRender = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picColorRender)).BeginInit();
            this.SuspendLayout();
            // 
            // picColors
            // 
            this.picColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picColors.Location = new System.Drawing.Point(9, 11);
            this.picColors.Name = "picColors";
            this.picColors.Size = new System.Drawing.Size(644, 28);
            this.picColors.TabIndex = 0;
            this.picColors.TabStop = false;
            this.picColors.Paint += new System.Windows.Forms.PaintEventHandler(this.picColors_Paint);
            this.picColors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picColors_MouseDown);
            this.picColors.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picColors_MouseMove);
            this.picColors.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picColors_MouseUp);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(9, 84);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(644, 361);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.TabStop = false;
            this.txtOutput.Text = resources.GetString("txtOutput.Text");
            // 
            // txtCurrentColor
            // 
            this.txtCurrentColor.Location = new System.Drawing.Point(112, 45);
            this.txtCurrentColor.Name = "txtCurrentColor";
            this.txtCurrentColor.Size = new System.Drawing.Size(92, 20);
            this.txtCurrentColor.TabIndex = 1;
            this.txtCurrentColor.Text = "0";
            this.txtCurrentColor.TextChanged += new System.EventHandler(this.txtCurrentColor_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current color:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 68);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(77, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "FILT wiki page";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtCurrentColorHex
            // 
            this.txtCurrentColorHex.Location = new System.Drawing.Point(228, 45);
            this.txtCurrentColorHex.Name = "txtCurrentColorHex";
            this.txtCurrentColorHex.Size = new System.Drawing.Size(61, 20);
            this.txtCurrentColorHex.TabIndex = 5;
            this.txtCurrentColorHex.TextChanged += new System.EventHandler(this.txtCurrentColorHex_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "#";
            // 
            // picColorRender
            // 
            this.picColorRender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picColorRender.Location = new System.Drawing.Point(9, 45);
            this.picColorRender.Name = "picColorRender";
            this.picColorRender.Size = new System.Drawing.Size(20, 20);
            this.picColorRender.TabIndex = 8;
            this.picColorRender.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 457);
            this.Controls.Add(this.picColorRender);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCurrentColorHex);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurrentColor);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.picColors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "TPTEye - Optics helper tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picColorRender)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picColors;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtCurrentColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtCurrentColorHex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picColorRender;
    }
}

