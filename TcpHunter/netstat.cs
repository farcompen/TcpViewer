using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace TcpHunter
{
    class netstat
    {
        public ArrayList aray;
        public ArrayList aray1;
        public ArrayList aray2;
        Form1 fm = new Form1();
        public void cmd_calistir()
        {
           
            try
            {
                aray = new ArrayList();
                aray.Clear();
                Process islem = new Process();
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.Arguments = "-a -n -o";
                ps.FileName = "netstat.exe";
                ps.UseShellExecute = false;
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.RedirectStandardInput = true;
                ps.RedirectStandardOutput = true;
                ps.RedirectStandardError = true;

                islem.StartInfo = ps;
                islem.Start();

                StreamReader komut_ciktisi = islem.StandardOutput;
                StreamReader komut_hata_cikti = islem.StandardError;
                string cikti = komut_ciktisi.ReadToEnd() + komut_hata_cikti.ReadToEnd();
                string exitStatus = islem.ExitCode.ToString();
                if (exitStatus != "0")
                {
                   

                }
                string[] satir = Regex.Split(cikti, "\r\n");
                foreach (string veri in satir)
                {
                    if (veri!="")
                    {
                       
                        string[] okunan = veri.Split(' ');
                        foreach (string alindi in okunan)
                        {
                            if (alindi != "")
                               
                                aray.Add(alindi);
                                   
                        }

                    }
                    
                   
                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }


        }


    }
}
