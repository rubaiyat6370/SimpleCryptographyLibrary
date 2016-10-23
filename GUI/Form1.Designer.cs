namespace GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cryptoButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.crcLength = new System.Windows.Forms.ComboBox();
            this.cryptoMethods = new System.Windows.Forms.ComboBox();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.outputString = new System.Windows.Forms.TextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.methodBox = new System.Windows.Forms.TextBox();
            this.crcVersionBox = new System.Windows.Forms.TextBox();
            this.keyBox = new System.Windows.Forms.TextBox();
            this.outputBitStringBox = new System.Windows.Forms.TextBox();
            this.outputStringBox = new System.Windows.Forms.TextBox();
            this.keyForAes = new System.Windows.Forms.TextBox();
            this.keyOfDes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cryptoButton
            // 
            this.cryptoButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cryptoButton.Location = new System.Drawing.Point(226, 316);
            this.cryptoButton.Name = "cryptoButton";
            this.cryptoButton.Size = new System.Drawing.Size(131, 23);
            this.cryptoButton.TabIndex = 0;
            this.cryptoButton.UseVisualStyleBackColor = false;
            this.cryptoButton.Click += new System.EventHandler(this.ExecuteCryptoMethod);
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.inputTextBox.Location = new System.Drawing.Point(23, 49);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(521, 20);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.outputTextBox.Location = new System.Drawing.Point(23, 383);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(521, 20);
            this.outputTextBox.TabIndex = 2;
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // crcLength
            // 
            this.crcLength.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.crcLength.FormattingEnabled = true;
            this.crcLength.Location = new System.Drawing.Point(24, 169);
            this.crcLength.Name = "crcLength";
            this.crcLength.Size = new System.Drawing.Size(131, 21);
            this.crcLength.TabIndex = 3;
            // 
            // cryptoMethods
            // 
            this.cryptoMethods.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cryptoMethods.FormattingEnabled = true;
            this.cryptoMethods.Location = new System.Drawing.Point(226, 101);
            this.cryptoMethods.Name = "cryptoMethods";
            this.cryptoMethods.Size = new System.Drawing.Size(131, 21);
            this.cryptoMethods.TabIndex = 4;
            this.cryptoMethods.SelectedIndexChanged += new System.EventHandler(this.CryptoMethodSelectionCHanged);
            // 
            // keyTextBox
            // 
            this.keyTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.keyTextBox.Location = new System.Drawing.Point(25, 235);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(130, 20);
            this.keyTextBox.TabIndex = 5;
            this.keyTextBox.TextChanged += new System.EventHandler(this.keyTextBox_TextChanged);
            // 
            // outputString
            // 
            this.outputString.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.outputString.Location = new System.Drawing.Point(23, 448);
            this.outputString.Name = "outputString";
            this.outputString.Size = new System.Drawing.Size(521, 20);
            this.outputString.TabIndex = 6;
            this.outputString.TextChanged += new System.EventHandler(this.outputString_TextChanged);
            // 
            // inputBox
            // 
            this.inputBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.inputBox.Location = new System.Drawing.Point(23, 23);
            this.inputBox.Name = "inputBox";
            this.inputBox.ReadOnly = true;
            this.inputBox.Size = new System.Drawing.Size(131, 20);
            this.inputBox.TabIndex = 7;
            this.inputBox.Text = "Input Text";
            this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
            // 
            // methodBox
            // 
            this.methodBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.methodBox.Location = new System.Drawing.Point(226, 75);
            this.methodBox.Name = "methodBox";
            this.methodBox.ReadOnly = true;
            this.methodBox.Size = new System.Drawing.Size(131, 20);
            this.methodBox.TabIndex = 8;
            this.methodBox.Text = "Crypto Methods";
            // 
            // crcVersionBox
            // 
            this.crcVersionBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.crcVersionBox.Location = new System.Drawing.Point(24, 143);
            this.crcVersionBox.Name = "crcVersionBox";
            this.crcVersionBox.ReadOnly = true;
            this.crcVersionBox.Size = new System.Drawing.Size(131, 20);
            this.crcVersionBox.TabIndex = 9;
            this.crcVersionBox.Text = "CRC Versions";
            // 
            // keyBox
            // 
            this.keyBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.keyBox.Location = new System.Drawing.Point(24, 209);
            this.keyBox.Name = "keyBox";
            this.keyBox.ReadOnly = true;
            this.keyBox.Size = new System.Drawing.Size(131, 20);
            this.keyBox.TabIndex = 10;
            this.keyBox.Text = "Enter Key";
            // 
            // outputBitStringBox
            // 
            this.outputBitStringBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.outputBitStringBox.Location = new System.Drawing.Point(24, 357);
            this.outputBitStringBox.Name = "outputBitStringBox";
            this.outputBitStringBox.ReadOnly = true;
            this.outputBitStringBox.Size = new System.Drawing.Size(100, 20);
            this.outputBitStringBox.TabIndex = 11;
            this.outputBitStringBox.Text = "Output Codes";
            // 
            // outputStringBox
            // 
            this.outputStringBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.outputStringBox.Location = new System.Drawing.Point(23, 422);
            this.outputStringBox.Name = "outputStringBox";
            this.outputStringBox.ReadOnly = true;
            this.outputStringBox.Size = new System.Drawing.Size(106, 20);
            this.outputStringBox.TabIndex = 12;
            this.outputStringBox.Text = "Output String";
            this.outputStringBox.TextChanged += new System.EventHandler(this.outputStringBox_TextChanged);
            // 
            // keyForAes
            // 
            this.keyForAes.Location = new System.Drawing.Point(24, 261);
            this.keyForAes.Name = "keyForAes";
            this.keyForAes.ReadOnly = true;
            this.keyForAes.Size = new System.Drawing.Size(131, 20);
            this.keyForAes.TabIndex = 13;
            this.keyForAes.Text = "Give a key of 2 character";
            // 
            // keyOfDes
            // 
            this.keyOfDes.Location = new System.Drawing.Point(23, 287);
            this.keyOfDes.Name = "keyOfDes";
            this.keyOfDes.Size = new System.Drawing.Size(132, 20);
            this.keyOfDes.TabIndex = 14;
            this.keyOfDes.Text = "Give a key of 8 character";
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(586, 509);
            this.Controls.Add(this.keyOfDes);
            this.Controls.Add(this.keyForAes);
            this.Controls.Add(this.outputStringBox);
            this.Controls.Add(this.outputBitStringBox);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.crcVersionBox);
            this.Controls.Add(this.methodBox);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.outputString);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.cryptoMethods);
            this.Controls.Add(this.crcLength);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.cryptoButton);
            this.Name = "Form1";
            this.Text = "Simple Cryptography Library";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cryptoButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.ComboBox crcLength;
        private System.Windows.Forms.ComboBox cryptoMethods;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.TextBox outputString;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.TextBox methodBox;
        private System.Windows.Forms.TextBox crcVersionBox;
        private System.Windows.Forms.TextBox keyBox;
        private System.Windows.Forms.TextBox outputBitStringBox;
        private System.Windows.Forms.TextBox outputStringBox;
        private System.Windows.Forms.TextBox keyForAes;
        private System.Windows.Forms.TextBox keyOfDes;
    }
}

