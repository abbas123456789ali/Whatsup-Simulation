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
using System.Threading;

namespace Wsemulation
{
    public partial class Form3 : Form
    {
        //Form1 frm1 = new Form1();
        public Form3()
        {
            InitializeComponent();
        }
        TcpListener tl;
        Socket skt;
        NetworkStream ns;
        
        Thread th;
        void ReciveImage()
        {

            try
            {
                
                tl = new TcpListener(53300);
                tl.Start();
                skt = tl.AcceptSocket();
                ns = new NetworkStream(skt);
                pictureBox1.Image = Image.FromStream(ns);
                tl.Stop();
                if (skt.Connected == true)
                {
                    while (true)
                    {
                        ReciveImage();
                    }
                }
             

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            tl.Stop();
            th.Abort();
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Image(*.jpg)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                string path = saveFileDialog1.FileName;
                pictureBox1.Image.Save(path);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            th = new Thread(new ThreadStart(ReciveImage));
            th.Start();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
