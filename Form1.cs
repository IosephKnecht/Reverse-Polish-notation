using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ОбратнаяПольскаяНотация
{
    delegate void Q(char input_sym);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public bool ParseBrackets(string input)
        {
            int n = 0;
            foreach (var c in input)
            {
                if (c == '(')
                    n++;
                if (c == ')')
                    n--;
                if (n < 0)
                    return false;
            }
            return n == 0 ? true : false;
        }

            private void button1_Click(object sender, EventArgs e)
        {
            string input= textBox1.Text;
            input = input.Replace(" ", string.Empty);
            if (!ParseBrackets(input))
            {
                MessageBox.Show("Введенная строка имела неправильный формат расстановки скобок...");
            }
            else
            {
                Calcul poland = new Calcul(input);
                poland.EndProcedure += Poland_EndProcedure;
                poland.Error += Poland_Error;
                poland.PolandNotationCalcul();
            }
        }

        private void Poland_Error()
        {
            requestLabel.Text = string.Empty;
            rpnLabel.Text = string.Empty;

            requestLabel.Text = "Error";
            rpnLabel.Text = "Error";
        }

        private void Poland_EndProcedure(double request, string rpn)
        {
            requestLabel.Text = string.Empty;
            rpnLabel.Text = string.Empty;

            requestLabel.Text = request.ToString();
            rpnLabel.Text = rpn;
        }
    }
}
