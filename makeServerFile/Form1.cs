﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace makeServerFile
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog fileDialog = new FolderBrowserDialog();
        string userName = Environment.UserName;

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateFile(string path, string dataToWrite) //Custom file creator. Works really well.
        {
            try
            {
                using (FileStream fs = System.IO.File.Create(path))
                {
                    byte[] dataToWriteArr = new UTF8Encoding(true).GetBytes(dataToWrite);
                    fs.Write(dataToWriteArr, 0, dataToWrite.Length);
                }

                using (StreamReader sr = System.IO.File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e) // Handles the folder selection.
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.SelectedPath = @"C:\Users\" + userName + @"\Desktop"; // Starts in the Desktop of the current user.
            folder.ShowNewFolderButton = false;
            DialogResult result = folder.ShowDialog();
            if(result == DialogResult.OK) // Not really needed, but whatever.
            {
                textBox1.Text = folder.SelectedPath;
                textBox2.Text = Path.GetDirectoryName(textBox1.Text + @"\Server Start.bat"); // Grabs the name of the directory for the the bat file that launches the server.
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder startBat = new StringBuilder(); // The start bat file
            StringBuilder settingsBat = new StringBuilder(); // The settings bat file
            StringBuilder startServer = new StringBuilder(); // The server starting bat file on the Desktop
            
            string filePath = textBox1.Text + @"\Server Start"; // Location of the internal Server Start bat File
            string filePath2 = textBox1.Text + @"\settings"; // Location of the internal Settings bat file
            string filePath3 = @"C:\Users\" + userName + @"\Desktop\Start Minecraft Server.bat"; // Location of the Start Server file on the desktop

            //Making Server Start File

            startBat.Append(@"@echo off
call settings.bat
java -Xmx%MAX_RAM% -Xms%MIN_RAM% -jar ");

            startBat.Append(textBox4.Text + ".jar");

            startBat.Append(" nogui \npause");

            //End Server Start File

            //Making Server Settings File

            settingsBat.Append("REM Generated by Server File Creator \nREM Author: Zane Reisbig \n");

            settingsBat.Append("set INSTALL_JAR=");
            settingsBat.Append(textBox3.Text + ".jar\n");

            settingsBat.Append("set INSTALL_JAR=");
            settingsBat.Append(textBox4.Text + ".jar\n");

            settingsBat.Append("set MIN_RAM=");
            settingsBat.Append(numericUpDown1.Value + "M \n");
            
            settingsBat.Append("set MAX_RAM=");
            settingsBat.Append(numericUpDown2.Value + "M \n");

            settingsBat.Append("set JAVA_PARAMETERS=-XX:+UseG1GC -Dsun.rmi.dgc.server.gcInterval=2147483646 -XX:+UnlockExperimentalVMOptions -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M -Dfml.readTimeout=180 \n");

            //End Server Start File

            //Making Start File Link
            startServer.Append("cd ");

            startServer.Append(Path.GetDirectoryName(textBox1.Text + @"\Server Start.bat"));

            startServer.Append("\n\"Server Start.bat\"\n");

            //End Start File Link

            CreateFile(filePath + ".bat", startBat.ToString()); // Creates the files
            CreateFile(filePath2 + ".bat", settingsBat.ToString()); // Creates the files
            CreateFile(filePath3, startServer.ToString()); // Creates the files

        }

        private void button3_Click(object sender, EventArgs e) // Helper for the minecraft server file
        {
            MessageBox.Show("This should be in your file folder. Something along the lines of \n\"minecraft_server.1.X.X\"");
        }

        private void button4_Click(object sender, EventArgs e) // Helper for the name of the parent folder
        {
            MessageBox.Show("This is the name of the file folder that you want the server file made for.");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) // Settings for easy ram selection
        {
            numericUpDown1.Value = 4069;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) // Settings for easy ram selection
        {
            numericUpDown1.Value = 8165;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) // Settings for easy ram selection
        {
            numericUpDown1.Value = 10213;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) // Settings for easy ram selection
        {
            numericUpDown2.Value = numericUpDown1.Value + 1;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) // Settings for easy ram selection
        {
            numericUpDown2.Value = numericUpDown1.Value + 1024;
        }
    }
}
