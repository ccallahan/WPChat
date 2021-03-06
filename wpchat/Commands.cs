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
using System.Text;

namespace csharpirc
{
    public static class Commands
    {
        //IRC commands some only partially correct 
        public static void msg_sending(string sending )
           
        {
            
            char[] splitter = { ' ' };
            string[] commandLine = new string[1024];
            string nickname = "";
            string message = "";
            commandLine = sending.Split(splitter, 4);
            string commands = sending.ToLower();
            if (commands.Contains("/msg"))
            {
                {
                    nickname = commandLine[1];
                    message = commandLine[2];
                }

                Networking.writer.WriteLine("PRIVMSG " + nickname + " : " + message);
                Networking.writer.Flush();
            }
            else if (commands.Contains("/voice"))
            {
                nickname = commandLine[1];
                message = "";

                Networking.writer.WriteLine("MODE " + SetupClass.Channel + " +v " + nickname);
                Networking.writer.Flush();
            }
            else if (commands.Contains("/quit"))
            {
                Networking.writer.WriteLine("QUIT ");
                Networking.writer.Flush();
                Networking.writer.Close();
                Networking.reader.Close();
                Networking.ircstream.Close();
                Networking.ircconnect.Close();
               
            }
            else if (commands.Contains("/names"))
            {
                Networking.writer.WriteLine("NAMES " + SetupClass.Channel);
                Networking.writer.Flush();
            }
            else if (commands.Contains("/topic"))
            {
                Networking.writer.WriteLine("TOPIC " + SetupClass.Channel);
                Networking.writer.Flush();
            }
            else if (commands.Contains("/nick"))
            {
                nickname = commandLine[1];
                SetupClass.Nick = nickname;
                if (FormMain.connection.IsAlive == true)
                {
                    Networking.writer.WriteLine("NICK " + SetupClass.Nick);
                    Networking.writer.Flush();
                }
            }
            else if (commands.Contains("/server"))
            {
                nickname = commandLine[1];
                SetupClass.Server = commandLine[1];
                SetupClass.port = int.Parse(commandLine[2]);
                SetupClass.Channel = commandLine[3];
                FormMain.connection.IsBackground = true;
                FormMain.connection.Start();
            }
            else if (commands.Contains("/join"))
            {
                Networking.writer.WriteLine("PART " + SetupClass.Channel);
                Networking.writer.Flush();
                nickname = commandLine[1];
                SetupClass.Channel = commandLine[1];
                Networking.writer.WriteLine("JOIN " + SetupClass.Channel);
                Networking.writer.Flush();
                

            }
            else if (commands.Contains("/back"))
            {
               

                Networking.writer.WriteLine("AWAY " );
                Networking.writer.Flush();

            }
            else if (commands.Contains("/away"))
            {
                if (commandLine[1] == null)
                {
                    
                }

                Networking.writer.WriteLine("AWAY : " + commandLine[1]);
            }
            else if (commands.Contains("/mode"))
            {
                Networking.writer.WriteLine("MODE " + commandLine[1] + " " + commandLine[2]);
                Networking.writer.Flush();
            }
            else if (commands.Contains("/invite"))
            {
                Networking.writer.WriteLine("INVITE " + commandLine[1] + " " + commandLine[2]);
                Networking.writer.Flush();
            }
            else if (commands.Contains("/kick"))
            {
                Networking.writer.WriteLine("KICK " + commandLine[1] + " " + commandLine[2] + " :" + commandLine[3]);
                Networking.writer.Flush();
            }
            else
            {
                Networking.writer.WriteLine("PRIVMSG " + SetupClass.Channel + " : " + sending);
                Networking.writer.Flush();
            }
        }
    }
}
