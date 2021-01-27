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
using DevExpress.XtraBars.Docking2010;

namespace DXApplication1
{
    public partial class add_new : DevExpress.XtraEditors.XtraForm
    {
        public add_new()
        {
            InitializeComponent();
        }

        private void add_new_Load(object sender, EventArgs e)
        {
            this.dateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy";
            dateEdit1.EditValue = DateTime.Now;

            //dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            ExecuteQueryvehecules();

        }
        private void ExecuteQueryvehecules()
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
            comboBox1.SelectedIndex = -1;



            Program.sql_con.Close();

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

        private void windowsUIButtonPanelMain_Click(object sender, EventArgs e)
        {
            

        }
        //setexecutequery
        private void ExecuteQuery(string txtQuery)
        {
            //Program.SetConnection();
            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
            Program.sql_cmd = Program.sql_con.CreateCommand();
            Program.sql_cmd.CommandText = txtQuery;
            Program.sql_cmd.ExecuteNonQuery();
            Program.sql_con.Close();
        }

        private void windowsUIButtonPanelMain_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Tag != null && btn.Tag.Equals("Enregistrer"))
            {
                toastNotificationsManager1.ShowNotification("1d00270b-1651-4ed4-a139-bd59d5d8cf8e");

            }
            if (e.Button == windowsUIButtonPanelMain.Buttons[1])
            {
                
                if (textEditname.Text == "" || textEdit4.Text == "" || textEdit5.Text == "" || textEdit1_marque.Text == "" ||textEdit3_matricul.Text == ""|| listBoxControl1.Items.IndexOf(listBoxControl1.SelectedItem) == -1)
                {
                    MessageBox.Show("champs obligatoires");
                  
                }

                else
                {



                    string textquery = "insert into [dbo].[Reaparation](nom ,[Carburant],[vehecule],[first_kilometrage],[_date])VALUES('" + textEditname.Text + "','"+textEdit4+"',"+comboBox1.SelectedValue+",'"+textEdit5.Text+"','"+dateEdit1.EditValue.ToString()+"')";
                    ExecuteQuery(textquery);
                    MessageBox.Show(dateEdit1.EditValue.ToString());
                        //this.Alert("add facture Success", Form_Alert.enmType.Success);
                        //textBox1.Text = ""; textBox6.Text = ""; textBox3.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
                        //dateTimePicker2.Value = DateTime.Now;
                        //this.ActiveControl = textBox3;
                    

                }

            }

        }
    }
}