using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using RSAUT;

namespace Networking_Project_for_Encryption_Visualization
{
    internal static class Program
    {
        public static MyApplicationContext context;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            context = new MyApplicationContext();            
            context.run();
            Application.Run(context);
        }
        public static void Sender(List<int>encrypted, int reciever, int sender)
        {
            string s = null;
            for (int i = 0; i < encrypted.Count; i++)
            {
                s += encrypted[i].ToString();
            }
            context.form3.SendMessage(sender, reciever, s);
        }
    }
    class MyApplicationContext : ApplicationContext
    {
        public Form1 form1 = new Form1();
        public Form2 form2 = new Form2();
        public Form3 form3 = new Form3();
        public void run()
        {
            form1.FormClosed += Form1_FormClosed;
            form2.FormClosed += Form2_FormClosed;
            form3.FormClosed += Form3_FormClosed;
            form1.Show();
            form2.Show();
            form3.Show();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitThread();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitThread();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ExitThread();
        }
    }
}
