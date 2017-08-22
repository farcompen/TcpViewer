using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net;

namespace TcpHunter
{
    public partial class Form1 : Form
    {
        ArrayList aray;
        ArrayList karaliste_ip_listesi;
        string PID;
        int blacklist_count = 0;
        ArrayList ip_aktar;
        int timer4_ilk = 0;

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Enabled = true;
            timer2.Enabled = true;
            timer4.Enabled = true;
        }



        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1_islemler();
        }

        public void netstat_islemleri()
        {

            netstat nt = new netstat();
            nt.cmd_calistir();
            panel3.Visible = true;

            dataGridView2.Visible = false;
            dataGridView1.Visible = true;

            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Width = 977;
            dataGridView1.Height = 645;


        }

        private void button5_Click(object sender, EventArgs e)
        {

            timer2_islemler();

        }


        public void yapilacak_baglanti_listele()
        {
            panel3.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Location = new Point(3, 3);
            dataGridView2.Width = 977;
            dataGridView2.Height = 645;
            dataGridView2.Visible = true;
            tcp_baglanti_listele();

        }

        /*
         
           tcp_baglanti_listele() metodu ile o anki tcp bağlantı listesi alınır ve datagrid'e aktarılır   
         */
        public void tcp_baglanti_listele()
        {

            dataGridView2.Rows.Clear();
            int i = 0;
            baglantilar bgl = new baglantilar();
            bgl.baglanti_listele();
            for (int a = 0; a < 300; a += 5)
            {
                try
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = bgl.baglanti_listesi[a];
                    dataGridView2.Rows[i].Cells[1].Value = bgl.baglanti_listesi[a + 1];
                    dataGridView2.Rows[i].Cells[2].Value = bgl.baglanti_listesi[a + 2];

                    dataGridView2.Rows[i].Cells[3].Value = bgl.baglanti_listesi[a + 3];
                    if (bgl.baglanti_listesi[a + 4].ToString() == "Established")
                    {
                        dataGridView2.Rows[i].Cells[4].Style.BackColor = Color.Green;
                    }
                    dataGridView2.Rows[i].Cells[4].Value = bgl.baglanti_listesi[a + 4];

                    i++;
                }
                catch (Exception hata)
                {
                }
            }

        }

        /*
        otification_tanimla() kullanıcıya notification veren metoddur. Eğer kullanıcı tcp bağlantısında yer alan hedef ip'lerden biri yada birden fazlası karaliste host iplerinden biriyse kullanıcıya notification veriliyor  

          */

        public void mail_gonder(string subject, string body)
        {
            Eposta_gonder Epstagnder = new Eposta_gonder();

            Epstagnder.send_mail(subject, body);

        }


        public void alert()
        {

            karaliste_islemler();

            if (karaliste_ip_listesi.Count != 0)
            {
                string local_ip = dataGridView2.Rows[0].Cells[0].Value.ToString();
                notification_tanimla();

                mail_gonder("TCPHUNTER Zararlı ip ", local_ip + "adresinden" + karaliste_ip_listesi[0].ToString() + " ip'sine erişim var.");
            }
           

        }


        public void notification_tanimla()
        {

            

                MessageBox.Show("notifi" + karaliste_ip_listesi[0]);
                NotifyIcon noticon = new NotifyIcon();
                noticon.Icon = new Icon("C://alert.ico");
                noticon.Text = "TcpViewer yeni Bildirim Var ";
                noticon.Visible = true;

                noticon.MouseClick += delegate
                {
               //     MessageBox.Show("aa");
                };

                noticon.ShowBalloonTip(1, "Zararlı İP ", "Zararlı İP'ye erişim tespitedildi " + karaliste_ip_listesi[0].ToString(), ToolTipIcon.Info);
            }
        


        public void karaliste_islemler()
        {
          
            ip_blacklist_kiyasla();



        }


        /*
          ip_blacklist_kiyasla() metodu ile , form ilk load olduğunda , karaliste gğncelleme sınıfı olan IplistesiTxt sınıfından nesne türettik 
          böylece karaliste host bilgilerini edindik. Daha sonra datagridview2 de yer alan (hedeh ip) alanı ile karşılaştırdık. Hedef ip'miz eğer karaliste host listesinde varsa karaliste_ip_listesi arraylistine atıyoruz 


         */


        public void ip_blacklist_kiyasla()
        {

            try
            {
                if (blacklist_count == 0)
                {
                    IpListesiTxt iplstxt = new IpListesiTxt();
                    iplstxt.txt_host_bilgisi_cek();
                    karaliste_ip_listesi = new ArrayList();
                    string ip;
                    for (int a = 0; a < iplstxt.liste.Count; a++)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            ip = dataGridView2.Rows[i].Cells[2].Value.ToString();
                            if (ip == iplstxt.liste[a].ToString())
                            {

                                karaliste_ip_listesi.Add(ip);

                            }


                        }

                    }
                    blacklist_count = 1;
               //     MessageBox.Show("liste timer2 ile çekildi");

                }

                else
                {


                    karaliste_ip_listesi = new ArrayList();
                    string ip;
                    for (int a = 0; a < ip_aktar.Count; a++)
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            ip = dataGridView2.Rows[i].Cells[2].Value.ToString();
                            if (ip == ip_aktar[a].ToString())
                            {

                                karaliste_ip_listesi.Add(ip);

                            }


                        }

                 //   MessageBox.Show("liste timer4 ile çekildi");
                }

            }

            catch (Exception hata)
            {
              //  MessageBox.Show("lütfen bekleyiniz");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        
        
        /*
         # islem listele metodu ile netstat sınıfından yeni bir nesne türetilip, o an çalışan proccessler alınır ve datagride aktarılır  
          
         */

        public void islem_listele()

        {
            dataGridView1.Rows.Clear();
            netstat ntt = new netstat();
            ntt.cmd_calistir();
            int i = 0;
            int limit = ntt.aray.Count;



            for (int a = 9; a < 300; a += 5)
            {

                try
                {
                    dataGridView1.Rows.Add();
                    PID = ntt.aray[a + 4].ToString();
                    var procees_name = Process.GetProcessById(Convert.ToInt32(PID)).ProcessName;


                    dataGridView1.Rows[i].Cells[1].Value = procees_name;
                    dataGridView1.Rows[i].Cells[0].Value = PID;

                    dataGridView1.Rows[i].Cells[2].Value = ntt.aray[a + 1].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = ntt.aray[a + 2].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = ntt.aray[a + 3].ToString();
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "ESTABLISHED")
                    {
                        dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Green;
                    }
                    i++;
                }

                catch (Exception error)
                {
                   
                }

                this.dataGridView1.Sort(this.dataGridView1.Columns["Column6"], ListSortDirection.Descending);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void timer1_islemler()
        {

            netstat_islemleri();
            islem_listele();


        }

        public void timer2_islemler()
        {
            yapilacak_baglanti_listele();
            tcp_baglanti_listele();
            alert();
        }



 /* 
 
  timer 4 karaliste host listesini güncelleyen sınıftan , arraylist değerlerini yeni bir array liste atar .
                         
 */
        public void timer4_islemler()
        {

            ip_aktar = new ArrayList();
            IpListesiTxt iplst = new IpListesiTxt();
            iplst.txt_host_bilgisi_cek();
            for (int a = 0; a < iplst.liste.Count; a++)
            {
                ip_aktar.Add(iplst.liste[a].ToString());

            }



        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1_islemler();
        //    MessageBox.Show("timer1");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2_islemler();
          //  MessageBox.Show("timer 2");
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4_islemler();
        }
    }
    }

