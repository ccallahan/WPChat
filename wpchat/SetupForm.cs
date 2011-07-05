/*
 * WPChat Client for #WrongPlanet
Copyright (C) 2010  Chance Callahan

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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace csharpirc
{
    public partial class SetupForm : Form
    {
        
        public SetupForm()
        {
            InitializeComponent();
        }

        private void buttonSetupOk_Click(object sender, EventArgs e)
        {
            try
            {
                SetupClass.Nick = textBoxNick.Text;
                SetupClass.port = 6667;
                SetupClass.Server = "irc.freenode.net";
                SetupClass.Channel = "#wrongplanet";
                this.Hide();
               
            }
            catch
            {
               
                if (textBoxNick.Text == "")
                {
                    Error_Box();
                }
                           }
        }

        public void Error_Box()
        {
            MessageBox.Show("Nickname Can't be blank.");
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {

        }

    }
}