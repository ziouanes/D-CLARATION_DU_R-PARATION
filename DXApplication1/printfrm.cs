using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace DXApplication1
{
    public partial class printfrm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public printfrm()
        {
            InitializeComponent();
        }

        public void PrintInvoice(int id_Reaparation, List<description> descriptions)
        {
            Report_declaration report = new Report_declaration();

            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
                p.Visible = false;
            using (SqlConnection sql_con = new SqlConnection(@"server =192.168.100.92;database = simpleDatabase ; user id = log1; password=P@ssword1965** ;MultipleActiveResultSets = True;"))

            {
                if (sql_con.State == ConnectionState.Closed)
                    sql_con.Open();
                SqlCommand cmd = sql_con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select r.[n/],r.[nom] , r.[Carburant]  ,v.[Marque] ,v.[matricule]   , r.[first_kilometrage] , r.[_date] from [Reaparation] r inner join [vehicules] v on r.vehecule  =v.id  where r.id = " + id_Reaparation + "";
                DataTable table = new DataTable();
                cmd.ExecuteNonQuery();
                SqlDataAdapter ad  = new SqlDataAdapter(cmd);
                ad.Fill(table);
                foreach (DataRow row in table.Rows)
                {


                    report.InitData(row["n/"].ToString(), row["nom"].ToString(), row["Carburant"].ToString(), row["Marque"].ToString(), row["matricule"].ToString(), row["first_kilometrage"].ToString(), row["_date"].ToString(),  descriptions);
                    documentViewer1.DocumentSource = report;
                    report.CreateDocument();

                }


            }
        }

        private void printfrm_Load(object sender, EventArgs e)
        {

        }
    }
}