namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.navigate = new System.Windows.Forms.WebBrowser();
            this.navigateBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // navigate
            // 
            this.navigate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigate.Location = new System.Drawing.Point(0, 0);
            this.navigate.MinimumSize = new System.Drawing.Size(20, 20);
            this.navigate.Name = "navigate";
            this.navigate.Size = new System.Drawing.Size(284, 261);
            this.navigate.TabIndex = 0;
            // 
            // navigateBrowser
            // 
            this.navigateBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.navigateBrowser.Location = new System.Drawing.Point(0, 0);
            this.navigateBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.navigateBrowser.Name = "navigateBrowser";
            this.navigateBrowser.Size = new System.Drawing.Size(284, 261);
            this.navigateBrowser.TabIndex = 1;
            this.navigateBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.navigateBrowser_DocumentCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.navigateBrowser);
            this.Controls.Add(this.navigate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser navigate;
        private System.Windows.Forms.WebBrowser navigateBrowser;
    }
}

