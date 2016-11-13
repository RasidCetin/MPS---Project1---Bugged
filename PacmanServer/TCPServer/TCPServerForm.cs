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
using System.Runtime.InteropServices;

namespace TCPServer
{
    public partial class TCPServerForm : Form
    {
		GameServer _server = null;

        public TCPServerForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        IPAddress FindMyIPV4Address()
        {
            string strThisHostName = string.Empty;
            IPHostEntry thisHostDNSEntry = null;
            IPAddress[] allIPsOfThisHost = null;
            IPAddress ipv4Ret = null;

            try
            {
                strThisHostName = System.Net.Dns.GetHostName();
                thisHostDNSEntry = System.Net.Dns.GetHostEntry(strThisHostName);
                allIPsOfThisHost = thisHostDNSEntry.AddressList;

                for (int idx = allIPsOfThisHost.Length - 1; idx >= 0; idx--)
                {
                    if (allIPsOfThisHost[idx].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipv4Ret = allIPsOfThisHost[idx];
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            return ipv4Ret;
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            if (_server == null)
            {
                IPAddress ipaddr;
                int nPort;

                if (!int.TryParse(tbPort.Text, out nPort))
                {
                    nPort = 2737;
                }
                if (!IPAddress.TryParse(tbIPAddress.Text, out ipaddr))
                {
                    MessageBox.Show("Invalid IP address supplied.");
                    return;
                }

				//Server.Instance.Start(ipaddr, nPort);
				_server = new GameServer();
				_server.Start(ipaddr, nPort);
				

				btnStartListening.Text = "Stop Listening";
            }
            else
            {
				_server.Stop();
				_server = null;

                btnStartListening.Text = "Start Listening";
            }

        }

        private void btnFindIPv4IP_Click(object sender, EventArgs e)
        {
            IPAddress ipa = null;

            ipa = FindMyIPV4Address();
            if (ipa != null)
            {
                tbIPAddress.Text = ipa.ToString();
            }
        }

        private void TCPServerForm_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void btnSendToAllClients_Click(object sender, EventArgs e)
        {
			//Server.Instance.SendToAllClients();
			_server.SendToAllClients("rrrrr");
		}

		private void btnPrintClients_Click(object sender, EventArgs e)
		{
			if (_server != null)
			{
				List<string> res = _server.GetClientsInfo();
				res.ForEach(s => Console.WriteLine(s));
			}
		}

		private void btnStartGame_Click(object sender, EventArgs e)
		{
			_server.StartGame();
		}
	}
}
