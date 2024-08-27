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

namespace SchoolADO_Master
{
    public partial class Form2 : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString;
        public Form2()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            RefreshDataGridView();
        }
        public void InitializeDatabaseConnection()
        {
            string connectionString = "Data Source=(localDB)\\Local;Initial Catalog=kpmSchoolDB;Integrated Security=True";

        }
        public void RefreshDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT \r\n    p.StudentName,\r\n    p.Address,\r\n    p.Phone,\r\n    pd.AdmissionDate,\r\n    pd.ClassName,\r\n  pd.SubjectName,\r\n   pd.AdmissionFee\r\nFROM \r\n    Student p\r\nINNER JOIN \r\n    ClassDetails pd ON p.StudentID = pd.StudentID;";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable patientDetails = new DataTable();
                    adapter.Fill(patientDetails);
                    dataGridView1.DataSource = patientDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Form2_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportView report= new ReportView(); 
            report.Show();
        }
    }
}
