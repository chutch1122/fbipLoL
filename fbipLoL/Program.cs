/* fbipLoL
 * Version 1.0
 * by Cameron Hutchison
 * 
 * The purpose of this program is to run the League of Legends client with ForceBindIP
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace fbipLoL
{
    class Program
    {
        static void Main(string[] args)
        {
            bool useForceBindIP = true;
            string forceBindIPLocation = "";
            string interfaceGUID = "";
            string lolClientDirectory = Directory.GetCurrentDirectory();
            string renamedLoLClientName = "";
            string concatenatedArguments = "";

            TextReader tr = new StreamReader("fbipLoL-settings.txt");
            if (tr.ReadLine().Trim() == "false")
                useForceBindIP = false;
            forceBindIPLocation = tr.ReadLine().Trim();
            interfaceGUID = tr.ReadLine().Trim();
            renamedLoLClientName = tr.ReadLine().Trim();
            tr.Close();

            for (int i = 0; i < args.Length; i++)
            {
                concatenatedArguments += "\"" + args[i] + "\" ";
            }

            if (useForceBindIP)
            { 
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = forceBindIPLocation;
                startInfo.Arguments = interfaceGUID + " \"" + lolClientDirectory + "\\" + renamedLoLClientName + "\"" + " dummyarg " + concatenatedArguments;

                TextWriter tw = new StreamWriter("fbipLoL-debug.txt");
                tw.WriteLine(concatenatedArguments);
                tw.WriteLine("\n");
                tw.WriteLine(startInfo.Arguments);
                tw.Close();

                Process.Start(startInfo);
            }
            else
            {
                TextWriter tw = new StreamWriter("fbipLoL-debug.txt");
                tw.WriteLine(concatenatedArguments);
                tw.Close();

                Process.Start(lolClientDirectory + "\\" + renamedLoLClientName, concatenatedArguments);
            }            
        }
    }
}
