using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;

namespace TcpHunter
{
    class IpListesiTxt
    {
        public ArrayList liste;

        public void txt_host_bilgisi_cek()
        {

            liste = new ArrayList();
            Uri link = new Uri("http://web_Server_ip/blacklist_hosts.html");
           
            WebClient client = new WebClient();
            string html = client.DownloadString(link);



             HtmlAgilityPack.HtmlDocument döküman = new HtmlAgilityPack.HtmlDocument();
             döküman.LoadHtml(html);
             HtmlNodeCollection body = döküman.DocumentNode.SelectNodes("//br");
             foreach (var bd in body)
             {
                 liste.Add(bd.InnerText);

             }
            MessageBox.Show("blacklist güncellendi");
            for (int a = 0; a < liste.Count; a++)
            {
                MessageBox.Show(liste[a].ToString());

            }
       
        }
    }
}
