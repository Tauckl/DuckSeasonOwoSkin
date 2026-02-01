using OWOGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DuckSeason_OWO_Form
{
    public partial class Form1 : Form
    {
        public DirectoryInfo GameDirectory;

        public Form1()
        {
            InitializeComponent();

            GameDirectory = new DirectoryInfo(dsExeFilePath.Text).Parent;
            Console.WriteLine(GameDirectory.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!dsExeFilePath.Text.EndsWith("DuckSeason.exe"))
            {
                // Not correct path to DuckSeason executable
                MessageBox.Show("Please input the path to DuckSeason.exe", "Incorrect file path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // File for duck season executable
            DirectoryInfo dsDir = new DirectoryInfo(dsExeFilePath.Text).Parent;

            if (!File.Exists(dsDir.FullName + "\\MelonLoader\\net35\\MelonLoader.dll"))
            {
                // MelonLoader is not installed
                MessageBox.Show("MelonLoader was not found.\n" +
                    "Please install MelonLoader for DuckSeason before attempting to install this mod.", "MelonLoader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set game dir
            GameDirectory = dsDir;

            var owoLogDir = dsDir.FullName + "\\Mods\\owo";

            // Create owo sensation log directory
            if (!Directory.Exists(owoLogDir))
            {
                WriteLog("Creating owo sensations directory...");
                Directory.CreateDirectory(owoLogDir);
            }



            WriteLog("Mod installed successfully.");
        }

        private void browseExePath_Click(object sender, EventArgs e)
        {
            if (browseExeOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Update file path
                dsExeFilePath.Text = browseExeOpenFileDialog.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (DuckSeason_Owo.OwoConnectionState == OWOGame.ConnectionState.Connected)
            {
                // Disconnect
                OWO.Disconnect();

                // Update button text
                connectBtn.Text = "Connect";
            }
            else if (DuckSeason_Owo.OwoConnectionState == OWOGame.ConnectionState.Disconnected)
            {
                // Connect to owo skin
                DuckSeason_Owo.ConnectOwo(owoIpText.Text);

                // Update button text
                connectBtn.Text = "Disconnect";
            }
        }

        public void WriteLog(string msg)
        {
            logOutTextBox.Invoke(new MethodInvoker(delegate
            {
                logOutTextBox.Text += "\n" + "[" + DateTime.Now.ToLongTimeString() + "] " + msg;

                // Scroll to bottom
                logOutTextBox.SelectionStart = logOutTextBox.Text.Length;
                logOutTextBox.ScrollToCaret();
            }));
        }

        private void sendTestSense_Click(object sender, EventArgs e)
        {
            if (DuckSeason_Owo.OwoConnectionState != OWOGame.ConnectionState.Connected)
            {
                MessageBox.Show("Please connect to the owoskin first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            OWO.Send(DuckSeason_Owo.OwoSensations[DuckSeason_bHaptics.Owo.DSSensation.Test], Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Dorsal_L, Muscle.Dorsal_R);
        }

        /// <summary>
        /// Updates the connection state text on the form
        /// </summary>
        /// <param name="state"></param>
        public void UpdateConnStateText(OWOGame.ConnectionState state)
        {
            owoConStat.Invoke(new MethodInvoker(delegate
            {
                owoConStat.Text = "OWO Connection Status: " + state.ToString();
            }));
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
