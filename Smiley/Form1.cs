using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static smiley.Emoji;

namespace smiley
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var list = GetEmojiList();
            Categories(list);
            flowLayoutPanel1.BackColor = Color.LightGray;
        }
        private void ASCII_click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;
            string[] infos = clickedBtn.Text.Split('\n');
            Clipboard.SetText(infos[0]);

            MessageBox.Show(clickedBtn.Text + " has copied to clipboard");
        }

        private void Categories(List<Category> list)
        {
            foreach (Category c in list)
            {
                Label cat = new Label() { Text = c.category };
                cat.AutoSize = false;
                cat.Width = this.ClientSize.Width - 30;
                cat.Font = new Font(FontFamily.GenericSerif, 16f);
                cat.TextAlign = ContentAlignment.MiddleCenter;
                cat.Margin = new Padding(0, 20, 0, 20);
                flowLayoutPanel1.SetFlowBreak(cat, true);
                flowLayoutPanel1.Controls.Add(cat);

                DisplayItems(c);
            }
        }

        private void DisplayItems(Category c)
        {
            foreach (Item item in c.items)
            {
                Button btn = new Button();
                btn.Text = item.art + Environment.NewLine + item.name;
                btn.Font = new Font(FontFamily.GenericSerif, 10);
                btn.Padding = new Padding(5);
                btn.Width = flowLayoutPanel1.ClientSize.Width / 2 - 15;
                btn.Height = 70;
                btn.BackColor = Color.LightSkyBlue;
                btn.Click += ASCII_click;
                flowLayoutPanel1.Controls.Add(btn);
            }

            Label empty = new Label() { Text = " " };
            flowLayoutPanel1.SetFlowBreak(empty, true);
        }

        private List<Category> GetEmojiList()
        {
            string json = File.ReadAllText("smiley_content.json");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<List<Category>>(json);
        }
    }
}
