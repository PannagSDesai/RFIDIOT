using System;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Windows.Forms;
namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        SerialPort port = new SerialPort("COM5", 9600);
        public Form1()
        {
            InitializeComponent();
        }
        string connstr = WindowsFormsApp13.Utility.GetConnectionString();
        private void button1_Click(object sender, EventArgs e)
        {
            for (; ; )
            {
                port.Open();
                String RFIDTAG = port.ReadLine();
                MessageBox.Show(RFIDTAG);
                SqlConnection conn = new SqlConnection(connstr);
                string sql = "select Balance from DBAPP1 where RFID = @RFID";
                SqlCommand cmdRFID = new SqlCommand(sql, conn);
                cmdRFID.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar));
                textBox1.Text = RFIDTAG;
                cmdRFID.Parameters["@RFID"].Value = textBox1.Text;
            try
            {

                conn.Open();
                int newProdID = (Int32)cmdRFID.ExecuteScalar(); 
                if (newProdID>=100)
                {
                    MessageBox.Show("Welcome on Board,Have Safe Journey");
                        port.Write("b");
                    }
                else
                 {
                    MessageBox.Show("No balance");
                        
                    }
            }
            catch
            {
                MessageBox.Show("DATABASE DOESN'T EXIST");
                conn.Close();
            }
            port.Close();
        }
    }

            /*private void button2_Click(object sender, EventArgs e)
            {
                port.Open();
                SqlConnection conn = new SqlConnection(connstr);
                String s = "Insert Into DBAPP1 values (@RFID,@Balance)";
                SqlCommand cmd = new SqlCommand(s,conn);
                String RFIDTAG = port.ReadLine();
                conn.Open();
                cmd.Parameters.Add(new SqlParameter("@RFID", SqlDbType.NVarChar, 40));
                cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Int));
                cmd.Parameters["@RFID"].Value = RFIDTAG;
                textBox2.Text = RFIDTAG;
                cmd.Parameters["@Balance"].Value = 100;
            }*/

       
        private void button2_Click(object sender, EventArgs e)
        {
           /* port.Open();
            port.Write("b");
            port.Close();*/
            
        }
    }
}
    

    

