/*
 * WPChat Client
Copyright (C) 2010 Chance Callahan

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Messaging;

namespace csharpirc
{
    public partial class FormMain : Form
    {
        //globals
       
        public static Thread connection = new Thread(new ThreadStart(Networking.Connect_Server));
       
        public FormMain()
        {
            InitializeComponent();
        }
        //opens Option Form to set nickname, irc server, port and channel
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupForm setup = new SetupForm();
            setup.ShowDialog();

        }
        //connection button 
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (!MessageQueue.Exists(@".\Private$\ServerQueue"))
            {
                MessageQueue.Delete(@".\Private$\ServerQueue");
                MessageQueue.Create(@".\Private$\ServerQueue");
            }
            connection.IsBackground = true;
            connection.Start();
            buttonConnect.Visible = false;
            buttonSend.Visible = true;
            buttonSend.Enabled = true;
            Thread ServerOutput = new Thread(new ThreadStart(outputDisplay));
            ServerOutput.IsBackground = true;
            ServerOutput.Start();
           
        }

        private void outputDisplay()
        {
            while (connection.IsAlive == true)
            {
                //accesses MessageQueue 
                System.Messaging.MessageQueue queue = new System.Messaging.MessageQueue(@".\Private$\ServerQueue");
                //looks at top message in the Queue
                System.Messaging.Message ServermsgCheck = queue.Peek();
                //Next Two Line Format both the Queue and the Message
                queue.Formatter = new XmlMessageFormatter(new String[] { "System.String, mscorlib", });
                ServermsgCheck.Formatter = new XmlMessageFormatter(new String[] { "System.String, mscorlib", });
                //sets the current Channel Topic 
                if (ServermsgCheck.Body.ToString().Contains("332") || ServermsgCheck.Body.ToString().Contains("TOPIC"))
                {
                    this.Invoke((MethodInvoker)delegate
                        {
                            System.Messaging.Message Servermsg = queue.Receive();
                            string[] incomming = new string[1024];
                            char[] cutter = { ' ' };
                            incomming = Servermsg.Body.ToString().Split(cutter, 4);
                            string channel_topic = incomming[3];
                            textBoxTopic.Text = channel_topic;
                        });
                    }
                    /*System.Messaging.Message Servermsg = queue.Receive();
                    string[] incomming = new string[1024];
                    char[] cutter = { ' ' };
                    incomming = Servermsg.Body.ToString().Split(cutter, 4);
                    string channel_topic = incomming[3];
                    textBoxTopic.Text = channel_topic;
                }*/
                    //Gets the Current Users in the Active Channel
                    else if (ServermsgCheck.Body.ToString().Contains("353"))
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            textBoxNames.Clear();
                        System.Messaging.Message Servermsg = queue.Receive();
                        string[] incomming = new string[2048];
                        char[] cutter = { ' ' };
                        incomming = Servermsg.Body.ToString().Split(cutter, 6);
                        string[] all_channel_names = new string[2048];
                        all_channel_names = incomming[5].Split(cutter);
                        int count = 0;
                        while (count != all_channel_names.Length)
                        {

                            textBoxNames.Text += all_channel_names[count] + Environment.NewLine;
                            count++;
                        }
                        });
                        
                        
                    }
                    //Gets the Channel Messages and places it in the Channel TextBox
                    else if (ServermsgCheck.Body.ToString().Contains("PRIVMSG"))
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (ServermsgCheck.Body.ToString() != null)
                            {
                                Thread.Sleep(100);
                                System.Messaging.Message Servermsg = queue.Receive();
                                string[] incomming = new string[2048];
                                char[] cutter = { ' ' };
                                char[] cutter2 = { '!' };
                                incomming = Servermsg.Body.ToString().Split(cutter, 4);
                                string[] raw_channel_message = new string[2048];
                                raw_channel_message = incomming[0].Split(cutter2);
                                string channel_message = raw_channel_message[0] + " " + incomming[3];
                                textBoxChannel.AppendText(channel_message + Environment.NewLine);
                            }
                        });
                        
                    }
                    else if (ServermsgCheck.Body.ToString().Contains("305"))
                    {
                        System.Messaging.Message Servermsg = queue.Receive();
                        textBoxOutput.AppendText("You are no longer marked as away from keyboard." + Environment.NewLine);
                    }
                    else if (ServermsgCheck.Body.ToString().Contains("306"))
                    {
                        System.Messaging.Message Servermsg = queue.Receive();
                        textBoxOutput.AppendText("You are now marked as away from keyboard.");
                    }

                    //if none of the above apply then place the message in the Server Window
                    else
                    {
                        if (InvokeRequired) // Line #1
                        {

                            this.Invoke((MethodInvoker)delegate
                            {
                                if (ServermsgCheck.Body.ToString() != null)
                                {
                                    System.Messaging.Message Servermsg = queue.Receive();
                                    textBoxOutput.AppendText(Servermsg.Body.ToString() + Environment.NewLine);
                                }
                            });
                        }

                        /* if (ServermsgCheck.Body.ToString() != null)
                         {
                         System.Messaging.Message Servermsg = queue.Receive();
                         textBoxOutput.AppendText(Servermsg.Body.ToString() + Environment.NewLine);
                        */
                    }
                }
            }
        
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //if the input textbox starts with / then do not echo it to the channel textbox 
            Commands.msg_sending(textBoxCommandLine.Text);
            if(textBoxCommandLine.Text.StartsWith("/"))
            {
            }
            else
            {
            textBoxChannel.AppendText(SetupClass.Nick + " " + textBoxCommandLine.Text + Environment.NewLine);
            }
            //clear input textbox after command is sent
            textBoxCommandLine.Clear();
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutbox1 = new AboutBox1();
            aboutbox1.ShowDialog();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void textBoxOutput_TextChanged(object sender, EventArgs e)
        {

        }

       
    } 
 }
