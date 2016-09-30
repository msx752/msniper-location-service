using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace RMSniperFeeder
{
    public class CloneConnection
    {
        private HubConnection connection;
        private IHubProxy msniperHub;
        private int numb = 0;
        public CloneConnection(int Clonenumber)
        {
            numb = Clonenumber;

        }
        public void Run()
        {
            connection = new HubConnection("http://localhost:55774/signalr", useDefaultUrl: false);
            msniperHub = connection.CreateHubProxy("msniperHub");
            connection.Received += Connection_Received;
            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;
            connection.Closed += Connection_Closed;
            connection.Start().Wait();
            Console.WriteLine($"[{numb}]connected");
            msniperHub.Invoke($"Identity");
        }

        private void Connection_Closed()
        {
            Console.WriteLine($"[{numb}]connection closed");
            Thread.Sleep(5000);
            msniperHub.Invoke("Identity");
        }

        private void Connection_Reconnected()
        {
            Console.WriteLine($"[{numb}]reconnected");
        }

        private void Connection_Reconnecting()
        {
            Console.WriteLine($"[{numb}]reconnecting");
            //Process.GetCurrentProcess().Kill();
        }

        private void Connection_Received(string obj)
        {
            try
            {
                HubData xx = connection.JsonDeserializeObject<HubData>(obj);
                switch (xx.Method)
                {
                    case "sendIdentity":
                        Console.WriteLine($"[{numb}](Identity) [ {xx.List[0]} ] connection establisted");
                        //Console.WriteLine($"[{numb}]now waiting pokemon request (15sec)");
                        break;

                    case "sendPokemon":
                        var data = Program.CreateData();
                        Console.WriteLine($"[{numb}]sending.. {data.Count} count");
                        msniperHub.Invoke("RecvPokemons", data);
                        break;

                    case "Exceptions":
                        var defaultc = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: " + xx.List.FirstOrDefault());
                        Console.ForegroundColor = defaultc;
                        break;
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
