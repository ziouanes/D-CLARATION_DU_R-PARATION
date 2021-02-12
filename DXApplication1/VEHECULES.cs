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
using System.ComponentModel.DataAnnotations;
using System.IO;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraLayout;
using System.Data.SqlClient;
using Dapper;

namespace DXApplication1
{
    public partial class VEHECULES : DevExpress.XtraEditors.XtraForm
    {
        public VEHECULES()
        {
            InitializeComponent();

        }
        private void ExecuteQuery(string txtQuery)
        {
            //Program.SetConnection();
            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open(); Program.sql_cmd = Program.sql_con.CreateCommand();
            Program.sql_cmd.CommandText = txtQuery;
            Program.sql_cmd.ExecuteNonQuery();
            Program.sql_con.Close();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {

           
        }

        private void VEHECULES_Load(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection sql_con = new SqlConnection(@"server =192.168.100.92;database = simpleDatabase ; user id = log1; password=P@ssword1965** ;MultipleActiveResultSets = True;"))

                {
                    if (sql_con.State == ConnectionState.Closed)
                        sql_con.Open();

                    string query = $"select Marque,[matricule] ,[km_V] , [date_V] from [vehicules] order by Marque  ";
                    véhiculesBindingSource.DataSource = sql_con.Query<Véhicules>(query, commandType: CommandType.Text);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string a = textEdit1.Text;
                string b = string.Empty;

                for (int i = 0; i < a.Length; i++)
                {
                    if (Char.IsDigit(a[i]))
                        b += a[i];
                }




                if (textEdit1.Text != "" && textEdit2.Text != "" && b != "")
                {
                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                    Program.sql_cmd = new SqlCommand("SELECT id from vehicules where [id] =  '" + b + "'", Program.sql_con);
                    Program.db = Program.sql_cmd.ExecuteReader();
                    if (Program.db.HasRows)
                    {

                        MessageBox.Show("vehicule déjà ajouter"); ;
                    }
                    else
                    {
                        if (MessageBox.Show("Do you really want to add   vehicule  Matricule° " + textEdit2.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string textquery = "INSERT INTO vehicules(id,Marque,matricule)VALUES(  " + b + ", '" + textEdit1.Text + "','" + textEdit2.Text + "' )";
                            ExecuteQuery(textquery);
                            toastNotificationsManager1.ShowNotification("5086983c-2c85-4a05-867c-3fde646a4997");
                            textEdit1.Text = "";
                            textEdit2.Text = "";





                        }

                    }





                }
                else
                {

                    toastNotificationsManager1.ShowNotification("53884298d-df06-4d7e-9974-1a53d7bf4ce4");
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }
        }

        private void textEdit21_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
       
}