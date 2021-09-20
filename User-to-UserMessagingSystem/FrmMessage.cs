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
    public partial class FrmMessage : Form
    {
        public string numeric;
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-OC5036T\MSSQLSERVER1;Initial Catalog=DBUser-to-UserMessagingSystem;Integrated Security=True");
        public FrmMessage()
        {
            InitializeComponent();
        }

        private void FrmMessage_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void FrmMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void FrmMessage_Deactivate(object sender, EventArgs e)
        {
            
        }
        void MessageSender()
        {
            SqlDataAdapter sqlData1 = new SqlDataAdapter("select MessageBuyer as 'Alıcı Numara',UserNameSurname as 'Alıcı', MessajeTitle as 'Mesaj Başlığı',MessageText as 'Mesaj İçeriği'  from TblMessage INNER JOIN TblUsers ON TblUsers.UserNumeric=TblMessage.MessageBuyer where MessageSender=" + numeric, connection);
            DataTable dt1 = new DataTable();
            sqlData1.Fill(dt1);
            dataGridView2.DataSource = dt1;


        }
        void MessageBuyer()
        {
            SqlDataAdapter sqlData = new SqlDataAdapter("select   MessageSender as 'Gönderen Numara',UserNameSurname as 'Gönderen', MessajeTitle as 'Mesaj Başlığı',MessageText as 'Messaj İçeriği' from TblMessage Inner JOIN TblUsers on TblUsers.UserNumeric=TblMessage.MessageSender where MessageBuyer=" + numeric, connection);
            DataTable dt = new DataTable();
            sqlData.Fill(dt);
            dataGridView1.DataSource = dt;
            

        }
        private void FrmMessage_Load(object sender, EventArgs e)
        {
            LblNumeric.Text = numeric;
            
            
            MessageBuyer();
            MessageSender();

            connection.Open();
            SqlCommand command = new SqlCommand("select UserNameSurname from TblUsers where UserNumeric="+numeric, connection);
           
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                LblNameSurname.Text = dr[0].ToString();
            }
            connection.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Trim() != "")
            {
                if (richTextBox1.Text.Trim() != "")
                {
                    if (maskedTextBox1.Text.Trim() != "")
                    {
                        if (richTextBox1.Text.Length <= 700)
                        {
                            if (richTextBox2.Text.Length <= 50)
                            {
                                connection.Open();
                                SqlCommand command1 = new SqlCommand("insert into TblMessage (MessageSender,MessageBuyer,MessajeTitle,MessageText) values (@p1,@p2,@p3,@p4)", connection);
                                command1.Parameters.AddWithValue("@p1", numeric);
                                command1.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
                                command1.Parameters.AddWithValue("@p3", richTextBox2.Text);
                                command1.Parameters.AddWithValue("@p4", richTextBox1.Text);
                                command1.ExecuteNonQuery();
                                connection.Close();
                                MessageSender();
                            }
                            else
                            {
                                MessageBox.Show("Mesaj Başlığı Çok Uzun, En Fazla 50 Karakter Olmalıdır");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mesaj İçeriği Çok Uzun, En Fazla 700 Karakter Olmalıdır");
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
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBuyer();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maskedTextBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
