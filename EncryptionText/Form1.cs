using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionText
{
    public partial class Form1 : Form
    {
        string numbers = "1234567890";
        string charTable = ".,!@#$%^&*()QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm 1234567890";
        string key = "";

        //Gets the index of a character
        private int IndexOf(ref string table,char c)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == c)
                {
                    return i;
                }
            }
            return -1;
        }

        //Shift a char forward or backward on char table
        private char Shift(ref string charTable,int amount,char c)
        {

            int i = IndexOf(ref charTable, c);

            if (i == -1)
            {
                return c;
            }

            return charTable[((i + amount) + charTable.Length) % charTable.Length];
        }

        //Gets the int from key
        private int GetKey(ref string key,int location)
        {
            if (key.Length < 1)
            {
                return 0;
            }
            return Convert.ToInt32("" + key[location % key.Length]);
        }

        //Crypt some text
        private string Crypt(ref string charTable,ref string key,string str, bool decrypt)
        {
            string f = "";
            for (int i = 0; i < str.Length; i++)
            {
                int cKey = GetKey(ref key, i);

                if (decrypt)
                    cKey *= -1;

                f += Shift(ref charTable,cKey,str[i]);
            }
            return f;
        }

        public Form1()
        {
            InitializeComponent();
        }

        //Key changed
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string f = "";
            foreach (char c2 in textBox3.Text)    
            {
                foreach (char c in numbers)
                {
                    if (c2 == c)
                    {
                        f += c;
                        break;
                    }
                }
            }
            textBox3.Text = f;
            key = f;
        }

        //Plain text
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Crypt(ref charTable, ref key, textBox2.Text, false);
        }
        
        //Cipher text
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Crypt(ref charTable, ref key, textBox1.Text, true);
        }

        //Character list changed
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            charTable = textBox4.Text;
        }

        //Set default
        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = ".,!@#$%^&*()QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm 1234567890";
        }

        //Set Alphabet
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = "abcdefghijklmnopqrstuvwxyz";
        }

        //Set numbers
        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = "0123456789";
        }
    }
}
