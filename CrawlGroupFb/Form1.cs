using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace CrawlGroupFb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int delay = Int32.Parse(textBoxDelay.Text);
            string[] keyWords = richTextBox1.Lines.ToArray();
            new Thread(() =>
            {
                try
                {
                    Account account = new Account();
                    account.U = "quanganh12102k2@gmail.com";
                    account.P = "121211223212Anh";
                    account.F = "IZEU TPFB DSNG 2YH6 3INL L7JJ NF56 LVYY";
                    ChromeDriver chrome = new ChromeDriver();

                    LoginMBasic.Login(chrome, account);
                    foreach(string word in keyWords)
                    {
                        CrawlGroup.CrawlGroupData(chrome, word);
                        Thread.Sleep(delay * 1000);
                    }
                   
                 
                    chrome.Close();
                }
                catch 
                {

                 
                }
               
            }).Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }
    }
}
