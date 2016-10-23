using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Lib;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         
            crcLength.Items.Add(1);
            crcLength.Items.Add(4);
            crcLength.Items.Add(8);
            crcLength.Items.Add(12);
            crcLength.Items.Add(16);
            crcLength.Items.Add(24);
            crcLength.Items.Add(30);
            crcLength.Items.Add(32);

            crcLength.Visible = false;
            keyTextBox.Visible = false;
            outputString.Visible = false;

            keyBox.Visible = false;
            crcVersionBox.Visible = false;
            outputString.Visible = false;

            keyOfDes.Visible = false;
            keyForAes.Visible = false;

            cryptoMethods.Items.Add("DesEncrypt");
            cryptoMethods.Items.Add("DesDecrypt");
            cryptoMethods.Items.Add("AesEncrypt");
            cryptoMethods.Items.Add("AesDecrypt");
            cryptoMethods.Items.Add("sha-1");
            cryptoMethods.Items.Add("MD-5");
            cryptoMethods.Items.Add("CRC");

            cryptoMethods.SelectedIndex = 0;

        }

        private void ExecuteCryptoMethod(object sender, EventArgs e)
        {
            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("CRC"))
            {
                outputTextBox.Text = new CRC().executeCrc(inputTextBox.Text,
                Int32.Parse(
                crcLength.Items[crcLength.SelectedIndex].ToString()));
            }

            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("sha-1"))
            {
                outputTextBox.Text = new SHA1().Hash(inputTextBox.Text);
            }

            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("MD-5"))
            {
                outputTextBox.Text = new MD5().Hash(inputTextBox.Text);
            }

            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("DesEncrypt"))
            {
                outputTextBox.Text = new DES().Encrypt(inputTextBox.Text, keyTextBox.Text);
                if (keyTextBox.Text.Length == 8) outputString.Text = new DES().BitStringToString(outputTextBox.Text);
                else outputString.Text = "Enter a key of length 8";
            }

            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("DesDecrypt"))
            {
                outputTextBox.Text = new DES().Decrypt(inputTextBox.Text, keyTextBox.Text);
                if (keyTextBox.Text.Length == 8) outputString.Text = new DES().BitStringToString(outputTextBox.Text);
                else outputString.Text = "Enter a key of length 8";
            }

            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("AesEncrypt"))
            {
                outputTextBox.Text = new AES().Encryption(inputTextBox.Text, keyTextBox.Text);
                if(keyTextBox.Text.Length == 2) outputString.Text = new DES().BitStringToString(outputTextBox.Text);
                else outputString.Text = "Enter a key of length 2";
            }
            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("AesDecrypt"))
            {
                outputTextBox.Text = new AES().Decryption(inputTextBox.Text, keyTextBox.Text);
                if (keyTextBox.Text.Length == 2) outputString.Text = new DES().BitStringToString(outputTextBox.Text);
                else outputString.Text = "Enter a key of length 2";
            }

        }

        private void CryptoMethodSelectionCHanged(object sender, EventArgs e)
        {
            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("CRC"))
            {
                crcLength.Visible = true;
                crcVersionBox.Visible = true;
            }
            else
            {
                crcLength.Visible = false;
                crcVersionBox.Visible = false;
            }

            var encryptionOptions = new List<string> { "AesEncrypt", "DesEncrypt", };
            var decryptionOptions = new List<string> { "AesDecrypt", "DesDecrypt", };
            var hashOptions = new List<string> { "MD-5", "CRC","sha-1" };
            if (encryptionOptions.Contains(cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString()))
            {
                cryptoButton.Text = "Encrypt";
                
            }

            else if (decryptionOptions.Contains(cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString()))
            {
                cryptoButton.Text = "Decrypt";
            }

            else if (hashOptions.Contains(cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString()))
            {
                cryptoButton.Text = "Hash";
            }
           
            
            if ((decryptionOptions.Contains(cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString())) ||
                 (encryptionOptions.Contains(cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString())))
            {
                keyTextBox.Visible = true;
                outputString.Visible = true;
                outputStringBox.Visible = true;
                keyBox.Visible = true;
            }
            else
            {
                keyTextBox.Visible = false;
                keyBox.Visible = false;
                outputString.Visible = false;
                outputStringBox.Visible = false;
            }
            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("AesDecrypt") || cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("AesEncrypt"))
            {
                keyForAes.Visible = true;
            }
            else
            {
                keyForAes.Visible = false;
            }
            if (cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("DesDecrypt") || cryptoMethods.Items[cryptoMethods.SelectedIndex].ToString().Equals("DesEncrypt"))
            {
                keyOfDes.Visible = true;
            }
            else
            {
                keyOfDes.Visible = false;
            }
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void crcLength_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void keyTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void outputString_TextChanged(object sender, EventArgs e)
        {

        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void outputStringBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
