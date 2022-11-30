using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AliceSeleniumHelper;
using Aurae_Facebook_Care.BUS.Selenium;
using CrawlGroupFb.Models;
using OpenQA.Selenium.Chrome;
using static System.Windows.Forms.LinkLabel;

namespace CrawlGroupFb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            int delay = Int32.Parse(textBoxDelay.Text);
            string[] keyWords = richTextBox1.Lines.ToArray();
            string[] cookies = richTextBox2.Lines.ToArray();
            var randomLineNumber = rnd.Next(0, cookies.Length - 1);
          

            new Thread(() =>
            {
                try
                {
                  
                    foreach (string word in keyWords)
                    {
                        var cookie = cookies[randomLineNumber];
                        LoginRequest.CrawlIdGroup(cookie,word);
                        //Thread.Sleep(delay * 60000);
                    }
                   
                 
                   
                }
                catch 
                {

                 
                }
               
            }).Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = @"idGroup.txt", UseShellExecute = true });
            }
            catch (Exception ex)
            {

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
