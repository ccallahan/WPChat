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
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace csharpirc
{
    public static class Networking
    {
        public static TcpClient ircconnect;
        public static NetworkStream ircstream ;
        public static StreamWriter writer;
        public static StreamReader reader;
        
        public static void Connect_Server()
        {
            //connection setup 
            ircconnect = new TcpClient(SetupClass.Server, SetupClass.port);
            ircstream = ircconnect.GetStream();
            PingClass ping = new PingClass();
            ping.Start();
            reader = new StreamReader(ircstream);
            writer = new StreamWriter(ircstream);

            writer.WriteLine("PING " + "irc.freenode.net");
            writer.Flush();
            writer.WriteLine("USER WPChat 8 * : WrongPlanet IRC Client");
            writer.Flush();
            writer.WriteLine("NICK " + SetupClass.Nick);
            writer.Flush();
            writer.WriteLine("JOIN " + "#wrongplanet");
            writer.Flush();
            
            while (true)
            {

                //if (reader.ReadLine() != null)
                {
                    System.Messaging.MessageQueue queue = new System.Messaging.MessageQueue(@".\Private$\ServerQueue");
                    queue.Send(reader.ReadLine());
                }

            }
        }
    }
}
