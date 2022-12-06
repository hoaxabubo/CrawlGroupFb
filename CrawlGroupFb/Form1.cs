using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
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
            string[] cookies = richTextBox2.Lines.ToArray();


            new Thread(() =>
            {
                try
                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells["cStatus"].Value = $"Đang Crawl";
                        string word = row.Cells["cKeyWord"].Value.ToString();
                        string cookie = "";
                        while(true)
                        {
                            
                            try
                            {
                               
                                var randomLineNumber = rnd.Next(0, cookies.Length - 1);
                                cookie = cookies[randomLineNumber];
                                bool check = BUS.CheckLive.CheckLiveCookie(cookie);
                                if (check)
                                {
                                    break;
                                }
                            }
                            catch 
                            {

                                
                            }

                        }
                        var re = LoginRequest.CrawlIdGroup(cookie,word);
                        if (re)
                        {
                            row.Cells["cStatus"].Value = $"Crawl group có từ khóa {word} thành công";
                        }
                      

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
            richTextBox1.Text = Settings1.Default.key;
            richTextBox2.Text = Settings1.Default.cookie;
           
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Settings1.Default.key = richTextBox1.Text;
            Settings1.Default.Save();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            Settings1.Default.cookie = richTextBox2.Text;
            Settings1.Default.Save();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string[] keyWords = richTextBox1.Lines.ToArray();
                foreach (string keyWord in keyWords)
                {

                    int iRow = dataGridView1.Rows.Add();

                    dataGridView1.Rows[iRow].Cells["cIndex"].Value = iRow + 1;
                    dataGridView1.Rows[iRow].Cells["cKeyWord"].Value = keyWord;

                }
                labelTotal.Text = "Totals:" + keyWords.Count().ToString();
            }
            catch
            {


            }
        }
    }
}
