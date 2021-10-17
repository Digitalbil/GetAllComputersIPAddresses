using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.DirectoryServices;

namespace <InsertNameSpaceHere>
{
    //To call this class use var localComputers = NetworkComputer.GetLocalNetwork();
    public class NetworkComputer
    {
        private string domain;
        private string name;
        private IPAddress[] addresses = null;

        public string Domain { get { return domain; } }
        public string Name { get { return name; } }
        public IPAddress[] Addresses { get { return addresses; } }

        private NetworkComputer(string domain, string name)
        {
            domain = domain;
            name = name;
            try { addresses = Dns.GetHostAddresses(name); } catch { }
        }

        public static NetworkComputer[] GetLocalNetwork()
        {
            var list = new List<NetworkComputer>();
            using (var root = new DirectoryEntry("WinNT:"))
            {
                foreach (var _ in root.Children.OfType<DirectoryEntry>())
                {
                    switch (_.SchemaClassName)
                    {
                        case "Computer":
                            list.Add(new NetworkComputer("", _.Name));
                            break;
                        case "Domain":
                            list.AddRange(_.Children.OfType<DirectoryEntry>().Where(__ => (__.SchemaClassName == "Computer")).Select(__ => new NetworkComputer(_.Name, __.Name)));
                            break;
                    }
                }
            }
            return list.OrderBy(_ => _.Domain).ThenBy(_ => _.Name).ToArray();
        }
    }
}
