using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RSAUT;

namespace Networking_Project_for_Encryption_Visualization
{
    public partial class Form1 : Form
    {
        Timer tt = new Timer();
        private RSAUtility rsa;
        public Form1()
        {
            InitializeComponent();
            rsa = new RSAUtility();
            rsa.GenerateKeys();
            //DisplayKeys();
            this.Load += Form1_Load;
            tt.Tick += Tt_Tick;
            tt.Interval = 100;
            tt.Start();
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            this.Text = this.Location.ToString();
        }
        private void DisplayKeys()
        {
            Label lblPublicKey = new Label();
            lblPublicKey.Location = new Point(700, this.Location.Y);
            lblPublicKey.AutoSize = true;
            lblPublicKey.Text = $"Public Key: {rsa.PublicKey}";
            this.Controls.Add(lblPublicKey);

            Label lblPrivateKey = new Label();
            lblPrivateKey.Location = new Point(700, lblPublicKey.Location.Y + 20);
            lblPrivateKey.AutoSize = true;
            lblPrivateKey.Text = $"Private Key: {rsa.PrivateKey}";
            this.Controls.Add(lblPrivateKey);

            Label lblN = new Label();
            lblN.Location = new Point(700, lblPrivateKey.Location.Y + 20);
            lblN.AutoSize = true;
            lblN.Text = $"N: {rsa.N}";
            this.Controls.Add(lblN);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(800, 60);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Msg = this.richTextBox1.Text;
            List<int> encrypted = new List<int>();
            foreach (char c in Msg)
            {
                encrypted.Add(rsa.Encrypt((int)c, rsa.PublicKey, rsa.N));
            }
            Program.Sender(encrypted, 2, 1);
        }
    }
}
