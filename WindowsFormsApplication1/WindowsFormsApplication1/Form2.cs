using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VK.NET;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        readonly string vkToken = Properties.Settings.Default.token;
        public Form2()
        {
            InitializeComponent();

        }

        List<Audio> audioList = new List<Audio>();

        private async void Form2_Load(object sender, EventArgs e)
        {
            (new Form1()).Show();

            audioList = await Audio.Get(vkToken);


            foreach (var audio in audioList)
            {
                listBox1.Items.Add(string.Format("{0} - {1}, lyrics_id={2}", audio.artist, audio.title, audio.lyrics_id));
            }
        }

        private async void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var audio = audioList[listBox1.SelectedIndex];
            var lyrics = await Audio.GetLyrics(vkToken, audio.lyrics_id.ToString());

            var lyrics2 = await audio.GetLyrics(vkToken);

            var audios = await Audio.GetById(vkToken, "118793152_456239041");

            textBox1.Text = lyrics;
        }
    }
}
