using System;
using WMPLib;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;
using System.Runtime.InteropServices;




namespace testFPT.AI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // chuyển txt ==> âm thanh 
        public string amthanh()
        {
            String result = Task.Run(async () =>
            {

                String payload =txt_input.Text;
               
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("api-key", "vSZGBnEDWIxL38x0gGdnnzcY4Q0miuPn");
                client.DefaultRequestHeaders.Add("speed", "");
                client.DefaultRequestHeaders.Add("voice", "banmai");
                var response = await client.PostAsync("https://api.fpt.ai/hmi/tts/v5", new StringContent(payload));
                return await response.Content.ReadAsStringAsync();
            }).GetAwaiter().GetResult();

            string[] str = result.Split('"');
            return str[3];

        }

        //tải file mp3 vào thư mục sound 
        public int taifile()
        {
            txt_url.Text = amthanh();
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri(amthanh()),
                    // param2 = path to save
                    @"C:\Users\Cao Anh\OneDrive - st.buh.edu.vn\Lập trình hướng đối tượng\testFPT.AI\testFPT.AI\sound\11.mp3"
                );
                return (1);
            }
        }
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            if (taifile() == 1)
            {

                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

                wplayer.URL = amthanh();
                //@"C:\Users\Cao Anh\OneDrive - st.buh.edu.vn\Lập trình hướng đối tượng\testFPT.AI\testFPT.AI\sound\11.mp3";
                wplayer.controls.play();
            }

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            txt_url.Text = amthanh();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes) Close();

        }
    }
}
