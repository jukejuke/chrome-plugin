using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string name)
        {
            InitializeComponent();
            this.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendMsg(txtContent.Text);
        }

        private void sendMsg(string content)
        {
            // {"text":"48BA4E4DA17F"} {"text":"48-BA-4E-4D-A1-7F"}
            string msg = "{\"tabId\":"+this.Text+",\"text\":\"" + content + "\"}";
           // Console.Write(msg.Length + msg);
            //msg = "{\"text\":\"48-BA-4E-4D-A1-7F,1C-4D-70-60-CB-32,00-50-56-C0-00-01,00-50-56-C0-00-08,1C-4D-70-60-CB-2E,1E-4D-70-60-CB-2E,1C-4D-70-60-CB-2F\"}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(msg);
 
            var stdout = Console.OpenStandardOutput();
            stdout.WriteByte((byte)((bytes.Length >> 0) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 8) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 16) & 0xFF));
            stdout.WriteByte((byte)((bytes.Length >> 24) & 0xFF));
            stdout.Write(bytes, 0, bytes.Length);
            stdout.Flush();
            //Console.ReadKey();
        }

        private void btnReceiveMsg_Click(object sender, EventArgs e)
        {
            txtReceiveMsg.Text = OpenStandardStreamIn();
        }

        private static string OpenStandardStreamIn()
        {
            //// We need to read first 4 bytes for length information  
            Stream stdin = Console.OpenStandardInput();
            int length = 0;
            byte[] bytes = new byte[4];
            stdin.Read(bytes, 0, 4);
            length = System.BitConverter.ToInt32(bytes, 0);

            string input = "";
            for (int i = 0; i < length; i++)
            {
                input += (char)stdin.ReadByte();
            }

            return input;
        }  
    }
}
