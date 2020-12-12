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
using WinFormsLab1;

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
                case 2:
                    richTextBox1.Text = Refactor.AddParameter(richTextBox1.Text, textBox1.Text, textBox2.Text);
                    break;
                case 3:
                    richTextBox1.Text = Refactor.RenameVariable(richTextBox1.Text, textBox1.Text, textBox2.Text);
                    break;
            }    
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    label1.Text = "Имя метода:";
                    label2.Text = "Имя параметра:";
                    button1.Text = "Удалить";
                    break;
                case 1:
                    label1.Text = "Имя метода:";
                    label2.Text = "Новое имя метода:";
                    button1.Text = "Переименовать";
                    break;
                case 2:
                    label1.Text = "Имя метода:";
                    label2.Text = "Имя параметра:";
                    button1.Text = "Добавить";
                    break;
                case 3:
                    label1.Text = "Имя параметра:";
                    label2.Text = "Новое имя параметра:";
                    button1.Text = "Переименовать";
                    break;
            }
        }
    }
}
