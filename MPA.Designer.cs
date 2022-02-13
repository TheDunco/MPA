
namespace MPA
{
    partial class MPAForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AnswerBox = new System.Windows.Forms.RichTextBox();
            this.InputTx = new System.Windows.Forms.TextBox();
            this.ThemeCB = new System.Windows.Forms.ComboBox();
            this.themeLab = new System.Windows.Forms.Label();
            this.Instructions1Lab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AnswerBox
            // 
            this.AnswerBox.BackColor = System.Drawing.Color.White;
            this.AnswerBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AnswerBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AnswerBox.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AnswerBox.Location = new System.Drawing.Point(1, 169);
            this.AnswerBox.Name = "AnswerBox";
            this.AnswerBox.Size = new System.Drawing.Size(1471, 278);
            this.AnswerBox.TabIndex = 0;
            this.AnswerBox.TabStop = false;
            this.AnswerBox.Text = "";
            // 
            // InputTx
            // 
            this.InputTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputTx.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InputTx.Location = new System.Drawing.Point(1, 112);
            this.InputTx.Name = "InputTx";
            this.InputTx.PlaceholderText = "Command";
            this.InputTx.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.InputTx.Size = new System.Drawing.Size(1471, 30);
            this.InputTx.TabIndex = 1;
            this.InputTx.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputTx_KeyUp);
            // 
            // ThemeCB
            // 
            this.ThemeCB.FormattingEnabled = true;
            this.ThemeCB.Items.AddRange(new object[] {
            "WoB",
            "BoW",
            "Pulse"});
            this.ThemeCB.Location = new System.Drawing.Point(12, 26);
            this.ThemeCB.Name = "ThemeCB";
            this.ThemeCB.Size = new System.Drawing.Size(121, 23);
            this.ThemeCB.TabIndex = 2;
            this.ThemeCB.SelectedIndexChanged += new System.EventHandler(this.ThemeCB_SelectedIndexChanged);
            // 
            // themeLab
            // 
            this.themeLab.AutoSize = true;
            this.themeLab.Location = new System.Drawing.Point(12, 5);
            this.themeLab.Name = "themeLab";
            this.themeLab.Size = new System.Drawing.Size(43, 15);
            this.themeLab.TabIndex = 3;
            this.themeLab.Text = "Theme";
            // 
            // Instructions1Lab
            // 
            this.Instructions1Lab.AutoSize = true;
            this.Instructions1Lab.Location = new System.Drawing.Point(12, 62);
            this.Instructions1Lab.Name = "Instructions1Lab";
            this.Instructions1Lab.Size = new System.Drawing.Size(280, 30);
            this.Instructions1Lab.TabIndex = 4;
            this.Instructions1Lab.Text = "Parameters to commands passed in separated by -p\r\nEx. messagebox -p Hello there";
            // 
            // MPAForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 450);
            this.Controls.Add(this.Instructions1Lab);
            this.Controls.Add(this.themeLab);
            this.Controls.Add(this.ThemeCB);
            this.Controls.Add(this.InputTx);
            this.Controls.Add(this.AnswerBox);
            this.Name = "MPAForm";
            this.Text = "Modular Personal Assistant";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox AnswerBox;
        private System.Windows.Forms.TextBox InputTx;
        private System.Windows.Forms.ComboBox ThemeCB;
        private System.Windows.Forms.Label themeLab;
        private System.Windows.Forms.Label Instructions1Lab;
    }
}

