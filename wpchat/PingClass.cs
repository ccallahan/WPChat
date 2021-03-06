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
using System.Threading;
namespace csharpirc
{
    public  class PingClass
    {
        static string PING = "PING :";
private Thread pingSender;
// Empty constructor makes instance of Thread
public  void PingSender()
{
pingSender = new Thread (new ThreadStart (this.Run) );
}
// Starts the thread
public void Start ()
{
//pingClass.Start ();
}
// Send PING to irc server every 15 seconds
public void Run ()
{
while (true)
{
Networking.writer.WriteLine (PING + " irc.freenode.net");
Networking.writer.Flush();
Thread.Sleep (15000);
}
}
    }
}
