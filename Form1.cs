using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SchoolADO_Master
{
    public partial class Form1 : Form
    {
        readonly string cs = ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString;
        SqlConnection Con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
            InitializeTable();
        }

        private DataTable dt = new DataTable();
        private void InitializeTable()
        {
            dt.Columns.Add("ClassName", typeof(string));
            dt.Columns.Add("SubjectName", typeof(string));
            dt.Columns.Add("AdmissionDate", typeof(DateTime));
            dt.Columns.Add("AdmissionFee", typeof(decimal));
        }

        public int InsertStudent(string StudentName, string Address, string Phone)
        {
            int patientID = 0;
            string query = "INSERT INTO Student (StudentName, Address, Phone) VALUES (@StudentName, @Address, @Phone); SELECT SCOPE_IDENTITY();";

            Con = new SqlConnection(cs);
            cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@StudentName", StudentName);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            try
            {
                Con.Open();
                patientID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting order: " + ex.Message);
            }
            return patientID;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox2.Text == " " || textBox3.Text == " " || textBox4.Text == "")
                {
                    MessageBox.Show(" Missing Information");
                }
                else
                {
                    dt.Rows.Add(comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), dateTimePicker1.Value.Date, textBox5.Text);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime testDate = dateTimePicker1.Value.Date;
                string name = textBox2.Text.Trim();
                string address = textBox3.Text.Trim();
                string phone = textBox4.Text.Trim();
                int StudentID = InsertStudent(name, address, phone);

                foreach (DataRow row in dt.Rows)
                {
                    string tname = (string)row["ClassName"];
                    string sname = (string)row["SubjectName"];
                    DateTime date = (DateTime)row["AdmissionDate"];
                    decimal fee = (decimal)row["AdmissionFee"];


                    InsertClassDetail(StudentID, tname, sname, date, fee);
                }
                MessageBox.Show("confirmed successfully.");
                dt.Clear();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void InsertClassDetail(int StudentID, string tname, string sname, DateTime date, decimal fee)
        {
            string query = "INSERT INTO ClassDetails (StudentID, ClassName,SubjectName,AdmissionDate, AdmissionFee) " +
                          " VALUES (@StudentID, @ClassName, @SubjectName, @AdmissionDate, @AdmissionFee); ";

            Con = new SqlConnection(cs);
            cmd = new SqlCommand(query, Con);

            cmd.Parameters.AddWithValue("@StudentID", StudentID);
            cmd.Parameters.AddWithValue("@ClassName", tname);
            cmd.Parameters.AddWithValue("@SubjectName", sname);
            cmd.Parameters.AddWithValue("@AdmissionDate", date);
            cmd.Parameters.AddWithValue("@AdmissionFee", fee);

            try
            {
                Con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting Class Detail: " + ex.Message);
            }
        }

        private void but_view_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.Show();
            this.Hide();

        }
    }
}
