using Dapper;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class add_new : DevExpress.XtraEditors.XtraForm
    {
        DateTime datenowedit = DateTime.Now;
        DateTime dateedit = DateTime.Now;

        int idEdit = 0;

        string st = "";
        public add_new(DateTime date1, string vehicule_id, string marque, string matriculation, string kilomitrage)
        {
            InitializeComponent();

            st = vehicule_id;
            numericUpDown1.Text = kilomitrage;
            dateedit = date1;
            textEdit1_marque.Text = marque;
            textEdit3_matricul.Text = matriculation;
        }

        public add_new(string numero, int id, int id_v, string marque, string matricule, DateTime date, string kilo, string nom, string carb, DateTime cell_newdate)
        {

            InitializeComponent();
            idEdit = id;
            st = id_v.ToString();
            numericUpDown1.Text = kilo;
            dateedit = date;
            textEdit1_marque.Text = marque;
            textEdit3_matricul.Text = matricule;
            textEditname.Text = nom;
            textEdit4.Text = carb;
            textEdit1.Text = numero;
            datenowedit = cell_newdate;

            try
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                {
                    SqlCommand cmd = Program.sql_con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT [_description] from descriptions where id_Reaparation =" + id + "";
                    DataTable table = new DataTable();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        listBoxControl1.Items.Add(row["_description"].ToString());

                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }





        }


        public add_new()
        {
            InitializeComponent();
        }

        private void add_new_Load(object sender, EventArgs e)
        {
            //XtraMessageBox.Show(dateEdit1.Text);
            this.dateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy";


            dateEdit1now.EditValue = datenowedit;
            dateEdit1.EditValue = dateedit;

            //dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;

            ExecuteQueryvehecules();

        }
        private void ExecuteQueryvehecules()
        {
            try
            {

                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                Program.sql_cmd = Program.sql_con.CreateCommand();
                Program.sql_cmd.CommandType = CommandType.Text;
                Program.sql_cmd.CommandText = "select id , concat(Marque,+'         '+matricule) as Vehicule  from [vehicules] ORDER BY Marque";
                Program.sql_cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                Program.ad = new SqlDataAdapter(Program.sql_cmd);
                Program.ad.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.ValueMember = "id";
                comboBox1.DisplayMember = "Vehicule";
                //comboBox1.SelectedIndex = -1;
                if (st != "")
                {
                    comboBox1.SelectedValue = st;

                }




                Program.sql_con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }


            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void add_to_list_Click(object sender, EventArgs e)
        {


            if (textEdit6.Text != "")
            {
                listBoxControl1.Items.Add(textEdit6.Text);

            }

            textEdit6.Text = "";
        }

        private void add_new_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listBoxControl1_MeasureItem(object sender, MeasureItemEventArgs e)
        {

        }

        private void listBoxControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (listBoxControl1.Items.IndexOf(listBoxControl1.SelectedItem) != -1)
            {

                if (e.KeyCode == Keys.Delete)
                {
                    listBoxControl1.Items.RemoveAt(listBoxControl1.Items.IndexOf(listBoxControl1.SelectedItem));

                }
            }
        }

        private void textEdit6_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void add_new_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                string s = "";
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                if (comboBox1.SelectedIndex > -1)
                {
                    SqlCommand cmd = Program.sql_con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    s = comboBox1.SelectedValue.ToString();
                    cmd.CommandText = "SELECT [Marque] , [matricule] from [dbo].[vehicules] where id =" + s + "";
                    DataTable table = new DataTable();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        textEdit1_marque.Text = row["Marque"].ToString();
                        textEdit3_matricul.Text = row["matricule"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }

        }

        private void windowsUIButtonPanelMain_Click(object sender, EventArgs e)
        {


        }
        //setexecutequery
        private void ExecuteQuery(string txtQuery)
        {
            try
            {


                //Program.SetConnection();
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                Program.sql_cmd = Program.sql_con.CreateCommand();
                Program.sql_cmd.CommandText = txtQuery;
                Program.sql_cmd.ExecuteNonQuery();
                Program.sql_con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }

        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;

            //enregistre
            if (e.Button == windowsUIButtonPanelMain.Buttons[0])
            {
                try
                {



                    if (textEditname.Text == "" || textEdit4.Text == "" || numericUpDown1.Text == "" || textEdit1_marque.Text == "" || textEdit3_matricul.Text == "" || listBoxControl1.Items.IndexOf(listBoxControl1.SelectedItem) == -1)
                    {
                        XtraMessageBox.Show("champs obligatoires");

                    }

                    else
                    {


                        DateTime dt = Convert.ToDateTime(dateEdit1.EditValue);
                        DateTime dtnow = Convert.ToDateTime(dateEdit1now.EditValue);


                        if (idEdit != 0)
                        //update
                        {
                            string textquery2 = "update  [Reaparation] set [n/] = '" + textEdit1.Text + "',nom  = '" + textEditname.Text + "' ,[Carburant] = '" + textEdit4.Text + "'  ,[vehecule] = " + comboBox1.SelectedValue + " ,[first_kilometrage] =  '" + numericUpDown1.Text + "' ,[_date]  = '" + dt.ToString("yyyy-MM-dd") + "' ,[datenow] = '" + dtnow.ToString("yyyy-MM-dd") + "'  where id = " + idEdit + "";
                            ExecuteQuery(textquery2);

                            string textquery3 = "DELETE  from descriptions where id_Reaparation = " + idEdit + " ";
                            ExecuteQuery(textquery3);

                            if (listBoxControl1.Items.Count != 0)
                            {
                                string sql = "INSERT INTO [dbo].[descriptions](_description,id_Reaparation) VALUES(@DESCRIPTION,@id_reparation)";

                                foreach (string _description in listBoxControl1.Items)
                                {
                                    Program.sql_cmd = new SqlCommand(sql, Program.sql_con);
                                    Program.sql_cmd.Parameters.AddWithValue("@DESCRIPTION", _description);
                                    Program.sql_cmd.Parameters.AddWithValue("@id_reparation", idEdit);

                                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                                    Program.sql_cmd.ExecuteNonQuery();
                                    Program.sql_con.Close();
                                }
                                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");
                                this.Close();
                            }

                        }
                        else
                        {
                            string textquery = "insert into [Reaparation]([n/],nom ,[Carburant],[vehecule],[first_kilometrage],[_date],[datenow])VALUES('" + textEdit1.Text + "','" + textEditname.Text + "','" + textEdit4.Text + "'," + comboBox1.SelectedValue + ",'" + numericUpDown1.Text + "','" + dt.ToString("yyyy-MM-dd") + "' ,'" + dtnow.ToString("yyyy-MM-dd") + "' )";
                            ExecuteQuery(textquery);
                            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                            Program.sql_cmd = new SqlCommand("SELECT TOP 1 id FROM [Reaparation] ORDER BY id DESC ", Program.sql_con);
                            string id = "";
                            Program.db = Program.sql_cmd.ExecuteReader();
                            if (Program.db.HasRows)
                            {


                                Program.db.Read();


                                id = Program.db[0].ToString();

                            }

                            if (listBoxControl1.Items.Count != 0)
                            {
                                string sql = "INSERT INTO [dbo].[descriptions](_description,id_Reaparation) VALUES(@DESCRIPTION,@id_reparation)";

                                foreach (string _description in listBoxControl1.Items)
                                {
                                    Program.sql_cmd = new SqlCommand(sql, Program.sql_con);
                                    Program.sql_cmd.Parameters.AddWithValue("@DESCRIPTION", _description);
                                    Program.sql_cmd.Parameters.AddWithValue("@id_reparation", id);

                                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                                    Program.sql_cmd.ExecuteNonQuery();
                                    Program.sql_con.Close();
                                }
                                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");
                            }
                            else
                            {
                                XtraMessageBox.Show("Error!!!");
                            }

                            this.Close();


                        }


                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    //this.Dispose();
                }

            }
            //enregistre and print
            if (e.Button == windowsUIButtonPanelMain.Buttons[1])
            {
                try
                {



                    if (textEditname.Text == "" || textEdit4.Text == "" || numericUpDown1.Text == "" || textEdit1_marque.Text == "" || textEdit3_matricul.Text == "" || listBoxControl1.Items.IndexOf(listBoxControl1.SelectedItem) == -1)
                    {
                        XtraMessageBox.Show("champs obligatoires");

                    }

                    else
                    {
                        DateTime dt = Convert.ToDateTime(dateEdit1.EditValue);
                        DateTime dtnow = Convert.ToDateTime(dateEdit1now.EditValue);


                        if (idEdit != 0)
                        //update 
                        {
                            string textquery2 = "update  [Reaparation] set [n/] = '" + textEdit1.Text + "',nom  = '" + textEditname.Text + "' ,[Carburant] = '" + textEdit4.Text + "'  ,[vehecule] = " + comboBox1.SelectedValue + " ,[first_kilometrage] =  '" + numericUpDown1.Text + "' ,[_date]  = '" + dt.ToString("yyyy-MM-dd") + "' ,[datenow] = '" + dtnow.ToString("yyyy-MM-dd") + "'  where id = " + idEdit + "";
                            ExecuteQuery(textquery2);

                            string textquery3 = "DELETE  from descriptions where id_Reaparation = " + idEdit + " ";
                            ExecuteQuery(textquery3);

                            if (listBoxControl1.Items.Count != 0)
                            {
                                string sql = "INSERT INTO [dbo].[descriptions](_description,id_Reaparation) VALUES(@DESCRIPTION,@id_reparation)";

                                foreach (string _description in listBoxControl1.Items)
                                {
                                    Program.sql_cmd = new SqlCommand(sql, Program.sql_con);
                                    Program.sql_cmd.Parameters.AddWithValue("@DESCRIPTION", _description);
                                    Program.sql_cmd.Parameters.AddWithValue("@id_reparation", idEdit);

                                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                                    Program.sql_cmd.ExecuteNonQuery();
                                    Program.sql_con.Close();
                                }
                                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");
                                this.Close();
                            }
                            if (Program.sql_con.State == ConnectionState.Closed)
                                Program.sql_con.Open();
                            string query = $" select  [_description] from [dbo].[descriptions] where [id_Reaparation] = { idEdit}";
                            List<description> descriptions = Program.sql_con.Query<description>(query, commandType: CommandType.Text).ToList();
                            using (printfrm frm = new printfrm())
                            {
                                frm.PrintInvoice(idEdit, descriptions);
                                frm.WindowState = FormWindowState.Maximized;
                                frm.ShowDialog();

                            }

                        }
                        else
                        {




                            string textquery = "insert into [Reaparation]([n/],nom ,[Carburant],[vehecule],[first_kilometrage],[_date],[datenow])VALUES('" + textEdit1.Text + "','" + textEditname.Text + "','" + textEdit4.Text + "'," + comboBox1.SelectedValue + ",'" + numericUpDown1.Text + "','" + dt.ToString("yyyy-MM-dd") + "' ,'" + dtnow.ToString("yyyy-MM-dd") + "')";
                            ExecuteQuery(textquery);
                            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                            Program.sql_cmd = new SqlCommand("SELECT TOP 1 id FROM [Reaparation] ORDER BY id DESC ", Program.sql_con);
                            string id = "";
                            Program.db = Program.sql_cmd.ExecuteReader();
                            if (Program.db.HasRows)
                            {


                                Program.db.Read();


                                id = Program.db[0].ToString();

                            }

                            if (listBoxControl1.Items.Count != 0)
                            {
                                string sql = "INSERT INTO [dbo].[descriptions](_description,id_Reaparation) VALUES(@DESCRIPTION,@id_reparation)";

                                foreach (string _description in listBoxControl1.Items)
                                {
                                    Program.sql_cmd = new SqlCommand(sql, Program.sql_con);
                                    Program.sql_cmd.Parameters.AddWithValue("@DESCRIPTION", _description);
                                    Program.sql_cmd.Parameters.AddWithValue("@id_reparation", id);

                                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                                    Program.sql_cmd.ExecuteNonQuery();
                                    Program.sql_con.Close();
                                }
                                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");




                                if (Program.sql_con.State == ConnectionState.Closed)
                                    Program.sql_con.Open();
                                string query = $" select  [_description] from [dbo].[descriptions] where [id_Reaparation] = { id}";
                                List<description> descriptions = Program.sql_con.Query<description>(query, commandType: CommandType.Text).ToList();
                                using (printfrm frm = new printfrm())
                                {
                                    frm.PrintInvoice(int.Parse(id), descriptions);
                                    frm.WindowState = FormWindowState.Maximized;
                                    frm.ShowDialog();

                                }

                            }
                            else
                            {
                                XtraMessageBox.Show("Error!!!");
                            }


                        }



                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    //this.Dispose();
                }

            }

            if (e.Button == windowsUIButtonPanelMain.Buttons[2])
            {
                try
                {



                    if (textEditname.Text == "" || textEdit4.Text == "" || numericUpDown1.Text == "" || textEdit1_marque.Text == "" || textEdit3_matricul.Text == "" || listBoxControl1.Items.IndexOf(listBoxControl1.SelectedItem) == -1)
                    {
                        XtraMessageBox.Show("champs obligatoires");

                    }

                    else
                    {



                        DateTime dt = Convert.ToDateTime(dateEdit1.EditValue);

                        DateTime dtnow = Convert.ToDateTime(dateEdit1now.EditValue);


                        if (idEdit != 0)
                        //update
                        {
                            string textquery2 = "update  [Reaparation] set [n/] = '" + textEdit1.Text + "',nom  = '" + textEditname.Text + "' ,[Carburant] = '" + textEdit4.Text + "'  ,[vehecule] = " + comboBox1.SelectedValue + " ,[first_kilometrage] =  '" + numericUpDown1.Text + "' ,[_date]  = '" + dt.ToString("yyyy-MM-dd") + "' ,[datenow] = '" + dtnow.ToString("yyyy-MM-dd") + "'  where id = " + idEdit + "";
                            ExecuteQuery(textquery2);

                            string textquery3 = "DELETE  from descriptions where id_Reaparation = " + idEdit + " ";
                            ExecuteQuery(textquery3);

                            if (listBoxControl1.Items.Count != 0)
                            {
                                string sql = "INSERT INTO [dbo].[descriptions](_description,id_Reaparation) VALUES(@DESCRIPTION,@id_reparation)";

                                foreach (string _description in listBoxControl1.Items)
                                {
                                    Program.sql_cmd = new SqlCommand(sql, Program.sql_con);
                                    Program.sql_cmd.Parameters.AddWithValue("@DESCRIPTION", _description);
                                    Program.sql_cmd.Parameters.AddWithValue("@id_reparation", idEdit);

                                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                                    Program.sql_cmd.ExecuteNonQuery();
                                    Program.sql_con.Close();
                                }
                                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");
                                this.Close();
                            }

                        }

                        else
                        {
                            string textquery = "insert into [Reaparation]([n/],nom ,[Carburant],[vehecule],[first_kilometrage],[_date],[datenow])VALUES('" + textEdit1.Text + "','" + textEditname.Text + "','" + textEdit4.Text + "'," + comboBox1.SelectedValue + ",'" + numericUpDown1.Text + "','" + dt.ToString("yyyy-MM-dd") + "' ,'" + dtnow.ToString("yyyy-MM-dd") + "')";
                            ExecuteQuery(textquery);
                            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                            Program.sql_cmd = new SqlCommand("SELECT TOP 1 id FROM [Reaparation] ORDER BY id DESC ", Program.sql_con);
                            string id = "";
                            Program.db = Program.sql_cmd.ExecuteReader();
                            if (Program.db.HasRows)
                            {


                                Program.db.Read();


                                id = Program.db[0].ToString();

                            }

                            if (listBoxControl1.Items.Count != 0)
                            {
                                string sql = "INSERT INTO [dbo].[descriptions](_description,id_Reaparation) VALUES(@DESCRIPTION,@id_reparation)";

                                foreach (string _description in listBoxControl1.Items)
                                {
                                    Program.sql_cmd = new SqlCommand(sql, Program.sql_con);
                                    Program.sql_cmd.Parameters.AddWithValue("@DESCRIPTION", _description);
                                    Program.sql_cmd.Parameters.AddWithValue("@id_reparation", id);

                                    if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                                    Program.sql_cmd.ExecuteNonQuery();
                                    Program.sql_con.Close();
                                }
                                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");
                            }
                            else
                            {
                                XtraMessageBox.Show("Error!!!");
                            }

                        }



                      

                        textEditname.Text = ""; textEdit4.Text = ""; numericUpDown1.Text = ""; textEdit1_marque.Text = ""; textEdit3_matricul.Text = ""; listBoxControl1.Items.Clear();
                        idEdit = 0;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    //this.Dispose();
                }

            }
            if (e.Button == windowsUIButtonPanelMain.Buttons[3])
            {
                textEditname.Text = ""; textEdit4.Text = ""; numericUpDown1.Text = ""; textEdit1_marque.Text = ""; textEdit3_matricul.Text = ""; listBoxControl1.Items.Clear();

            }

        }

        private void labelControl_Click(object sender, EventArgs e)
        {

        }
    }
}