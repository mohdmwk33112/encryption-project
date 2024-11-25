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
    public partial class Form3 : Form
    {
        private RSAUtility rsa;
        public Form3()
        {
            InitializeComponent();
            rsa = new RSAUtility();
            this.Load += Form3_Load;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Location = new Point(346, 243);
        }
        public void SendMessage(int Sender,int Reciever,string Msg)
        {
            Msg.Trim();
            if (Msg != " ")
            {
                List<int> cipher = new List<int>();
                int pkey = rsa.PrivateKey;
                int n = rsa.N;
                string decrypted = null;
                if (Sender == 1)
                {
                    for (int i = 0; i < Msg.Count(); i++)
                    {
                        decrypted += rsa.Decrypt(int.Parse(Msg[i].ToString()), pkey, n);
                    }
                    Program.context.form1.listBox1.Items.Add(Msg);
                    Program.context.form2.listBox1.Items.Add(Environment.NewLine);
                    Program.context.form2.listBox2.Items.Add(Msg);
                    Program.context.form1.listBox2.Items.Add(Environment.NewLine);
                    Program.context.form1.richTextBox1.Text = "";
                }
                else
                {
                    for (int i = 0; i < Msg.Count(); i++)
                    {
                        decrypted += rsa.Decrypt(int.Parse(Msg[i].ToString()), pkey, n);
                    }
                    Program.context.form2.listBox1.Items.Add(Msg);   
                    Program.context.form1.listBox1.Items.Add(Environment.NewLine);
                    Program.context.form1.listBox2.Items.Add(Msg);
                    Program.context.form2.listBox2.Items.Add(Environment.NewLine);
                    Program.context.form2.richTextBox1.Text = "";
                }
            }
        }
    }
}
