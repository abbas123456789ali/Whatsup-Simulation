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
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;
using System.Threading;
namespace Wsemulation
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }
        string temp = "";
        string temp2 = "";
        char[] string1 = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '5', '6', '7', '8', '9', ' ' };
        char[] string2 = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '#' };

        SpeechSynthesizer reader = new SpeechSynthesizer();
        SpeechRecognizer speechrecognizer = new SpeechRecognizer();
        Socket sck;
        EndPoint eplocal, epremote;
        byte[] buffer;
       
        string GetIPAdress()
        {
            IPHostEntry myHost;
            string ip = "127.0.0.1";
            myHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipa in myHost.AddressList)
            {
                if (ipa.AddressFamily.ToString() == "InterNetwork") ip = ipa.ToString();
            }
            return ip;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textLocalIp.Text = GetIPAdress();
            speechrecognizer.SpeechRecognized += speechrecognizer_SpeechRecognized;
            label14.Visible = false;
        }

        private void speechrecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            text.AppendText(e.Result.Text.ToString() + "");
        }
       
            
    
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //binding sockets
            eplocal = new IPEndPoint(IPAddress.Parse(textLocalIp.Text), Convert.ToInt32(textLoaclPort.Text));
            sck.Bind(eplocal);
            epremote = new IPEndPoint(IPAddress.Parse(textRemoteIp.Text), Convert.ToInt32(textRemotePort.Text));
            sck.Connect(epremote);
            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);
            MessageBox.Show("The connection is completed successfully");

        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            ASCIIEncoding aencoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aencoding.GetBytes(text.Text);
            sck.Send(sendingMessage);
            list.Items.Add("Me:" + text.Text + "√");
            text.Text = ""; 
        }

        private void MessageCallBack(IAsyncResult aresult)
        {
            try
            {
                byte[] ReceivedData = new byte[1500];
                ReceivedData = (byte[])aresult.AsyncState;
                ASCIIEncoding aencoding = new ASCIIEncoding();
                string receivedMessage = aencoding.GetString(ReceivedData);
                foreach (char a in receivedMessage)
                {
                    if (a == '#')
                    {
                        label14.Visible = true;
                        label14.Text = receivedMessage;
                        text.Visible = true;
                        list.Visible = true;
                    }
                    
                }
                list.Items.Add("Friend:" + receivedMessage);
                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }


        // running the speech recognation
        private void button1_Click(object sender, EventArgs e)
        {
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.LoadGrammar(new DictationGrammar());
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }


        private void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                text.AppendText(word.Text + " ");
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Paused)
                {
                    reader.Resume();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                reader.Dispose();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _recognizer.RecognizeAsyncStop();
        
        }

        private async void btnsave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "text Document|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    using (sw)
                    {
                        await sw.WriteLineAsync(list.Text);
                        MessageBox.Show("you have been successfully saved ", "Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void btnread_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "text dcuments|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        list.Text = await sr.ReadToEndAsync();
                    }
                }
            }
        }

        private void btnsi_Click(object sender, EventArgs e)
        {
            Form4 form2 = new Form4();
            form2.Show();
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            if (list.Text != "")
            {
                reader.Dispose();
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(list.Text);
            }
            else
            {
                MessageBox.Show("please enter some text first");
            }
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Speaking)
                {
                    reader.Pause();
                }
            }
        }
       
        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "text Document|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    using (sw)
                    {
                        await sw.WriteLineAsync(list.Text);
                        MessageBox.Show("you have been successfully saved ", "Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "text Document|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    using (sw)
                    {
                        await sw.WriteLineAsync(list.Text);
                        MessageBox.Show("you have been successfully saved ", "Message ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "text dcuments|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        list.Text = await sr.ReadToEndAsync();
                    }
                }
            }
        }

       

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Whatsup Simulation");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            text.Text = "";
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            list.Items.Clear();
            list.Items.Add("ListMessage :-");
        }

        private void sendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form2 = new Form4();
            form2.Show();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void encryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temp = "";
            string txt = text.Text;
            foreach (char s in txt)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (s == '#') temp += '#';
                    else if (s == string2[i]) { int j = (i + 3) % 32; temp += string2[j]; }
                    else if (s == string1[i]) { int j = (i + 3) % 32; temp += string1[j]; }
                }
            }
            text.Text = "";
            text.Text = temp;
        }

        private void steganographyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void decodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temp2 = "";
            string coded = label14.Text;
            foreach (char s in coded)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (s == '#') temp2 += null;
                    if (s == string2[i])
                    {
                        int j = (i - 3);
                        if (j < 0) j = j + 32;
                        temp2 += string2[j];
                    }
                    else if (s == string1[i])
                    {
                        int j = (i - 3);
                        if (j < 0) j = j + 32;
                        temp2 += string1[j];
                    }
                }
            }
            MessageBox.Show(temp2);
            label14.Visible = false;
        }

        

    }
    }

