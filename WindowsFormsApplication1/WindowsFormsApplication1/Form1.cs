using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VK.NET;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void navigateBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string token, id, url = navigateBrowser.Url.ToString();
            bool auth;
            Authorization.Authorize(url, out token, out id, out auth);

            Properties.Settings.Default.token = token;
            Properties.Settings.Default.id = id;
            Properties.Settings.Default.auth = auth;
            Properties.Settings.Default.Save();

            if (auth)
                this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var auth = new Authorization(5224789, display: "popup", scope: new string[] { "audio", "friends" });
            navigateBrowser.Navigate(auth.Request());
        }
    }
}
