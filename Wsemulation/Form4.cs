using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Wsemulation
{
    public partial class Form4 : Form
    {
        private Form1 frm1;
        public Form4()
        {
            InitializeComponent();
        }
        MemoryStream ms;
        TcpClient tc;
        NetworkStream ns;
        BinaryWriter br;
      /*  string GetIpAdress()
        {
            IPHostEntry host;
          string localip = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "Internetworker")
                {
                    localip = ip.ToString();
                }
            }
            return localip;
        }*/

        private void Form4_Load(object sender, EventArgs e)
        {
          ///  txtserver.Text = frm1.textRemoteIp.Text;
           /// txtserver.Text = GetIpAdress();
           /// pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnbrow_Click(object sender, EventArgs e)
        {
          ///  txtserver.Text = frm1.textRemoteIp.Text;
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(path);
            txtbrow.Text = path;
        }

        private void btnsend_Click(object sender, EventArgs e)
        {

            try
            {
                ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] buffer = ms.GetBuffer();
                ms.Close();
                tc = new TcpClient(txtserver.Text, 53300);
                ns = tc.GetStream();
                br = new BinaryWriter(ns);
                br.Write(buffer);
                br.Close();
                ns.Close();
                tc.Close();
                MessageBox.Show("...sending operation is completed successfuly...");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
