using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace User_to_UserMessagingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string a;
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-OC5036T\MSSQLSERVER1;Initial Catalog=DBUser-to-UserMessagingSystem;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            
            

            if (textBox2.Text.Length<=20)
            {
                if (textBox2.Text.Trim() != "")
                {
                    if(maskedTextBox1.Text.Trim()!="")
                    {

                        connection.Open();
                        SqlCommand command = new SqlCommand("Select * From TblUsers where UserNumeric=@p1 and UserPass=@p2", connection);
                        command.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
                        command.Parameters.AddWithValue("@p2", textBox2.Text);
                        SqlDataReader dr = command.ExecuteReader();
                        if(dr.Read())
                        {
                            FrmMessage fr = new FrmMessage();

                            fr.numeric = maskedTextBox1.Text;
                            
                            fr.Show();

                            this.Hide();
                        }
                        
                        else
                        {
                            MessageBox.Show("Kullanıcı Adı ya da Şifre Hatalı");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Boş Alanları Doldurunuz");
                    }
                }
                
                else
                {
                    MessageBox.Show("Lütfen Boş Alanları Doldurunuz");
                }
            }
            else
            {
                Application.Exit();
            }
            connection.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            

        }
    }
}
