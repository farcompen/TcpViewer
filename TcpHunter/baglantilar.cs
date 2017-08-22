using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace TcpHunter
{
    class baglantilar
    {
       
        public List<string> baglanti_listesi = new List<string>();
        public void baglanti_listele()
        {

            baglanti_listesi.Clear();
            IPGlobalProperties ippro = IPGlobalProperties.GetIPGlobalProperties();

            IPEndPoint[] endpoint = ippro.GetActiveTcpListeners();

            TcpConnectionInformation[] baglantilar = ippro.GetActiveTcpConnections();
            
            try
            {
                foreach (TcpConnectionInformation tcp_baglanti in baglantilar)
                {

                    baglanti_listesi.Add(tcp_baglanti.LocalEndPoint.Address.ToString());
                    baglanti_listesi.Add(tcp_baglanti.LocalEndPoint.Port.ToString());
                    baglanti_listesi.Add(tcp_baglanti.RemoteEndPoint.Address.ToString());
                    baglanti_listesi.Add(tcp_baglanti.RemoteEndPoint.Port.ToString());
                    baglanti_listesi.Add(tcp_baglanti.State.ToString());
                  

                }

            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());

            }
           
        }


    }
}
