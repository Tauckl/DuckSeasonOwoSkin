using OWOGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

            // Check if mod data exists
            var requiredFiles = new string[] { "DuckSeason_owo.dll", "System.Threading.dll" };

            foreach (string file in requiredFiles)
            {
                // Check that each required file exists
                if (!File.Exists(Directory.GetCurrentDirectory() + "/MLMod/" + file))
                {
                    // Not all mod data found
                    MessageBox.Show("Some files are missing.\n" +
                        "Please redownload all files and try again.", "Missing files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            // Copy directory
            var installFromDir = Directory.GetCurrentDirectory() + "\\MLMod";
            var installToDir = dsDir.FullName + "\\Mods";

            CopyFilesRecursively(installFromDir, installToDir);


            WriteLog("Mod installed successfully.");
        }

        /// <summary>
        /// Copies a directory from sourcePath to targetPath
        /// 
        /// Just a copy pasta from
        /// https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            // Create all the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                var dirTo = dirPath.Replace(sourcePath, targetPath);

                WriteLog("Attempting to create directory at " + dirTo);
                Directory.CreateDirectory(dirTo);
            }

            // Copy all the files & replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                var fileTo = newPath.Replace(sourcePath, targetPath);

                WriteLog("Attempting to copy file " + fileTo);
                File.Copy(newPath, fileTo, true);
            }
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

        public void ClearLog()
        {
            logOutTextBox.Invoke(new MethodInvoker(delegate
            {
                logOutTextBox.Text = "";

                // Scroll
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


        /// <summary>
        /// Clear log button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ClearLog();
        }

        /// <summary>
        /// Launch game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (!dsExeFilePath.Text.EndsWith("DuckSeason.exe"))
            {
                // Not correct path to DuckSeason executable
                MessageBox.Show("Please input the path to DuckSeason.exe", "Incorrect file path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WriteLog("Starting " + dsExeFilePath.Text);

            // Prepare process
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = dsExeFilePath.Text;

            // Start Duck Season
            Process.Start(proc);
        }
    }
}
