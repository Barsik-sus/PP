using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RefactorMethodLib;

namespace Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefactorMethod refact = new RefactorMethod();
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    richTextBox1.Text =  refact.DelParam(richTextBox1.Text, textBox1.Text, textBox2.Text); 
                    break;
                case 1: 
                    richTextBox1.Text =  refact.Rename(richTextBox1.Text, textBox1.Text, textBox2.Text); 
                    break;
            }    
        }
    }
}
