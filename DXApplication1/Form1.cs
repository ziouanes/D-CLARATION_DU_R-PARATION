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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.BandedGrid;
using Microsoft.Win32;


using Dapper;
using DevExpress.Xpo;
using System.Diagnostics;

namespace DXApplication1
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
          
        }
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void ribbonControl_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            add_new ss = new add_new();
            ss.ShowDialog();
            select_new_Data();
            select_old_Data();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //run startup
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Declaration du Reparation", Application.ExecutablePath.ToString());

            select_new_Data();
            select_old_Data();
            timer1.Start();



        }

        private void select_new_Data()
        {
            try { 
            using (SqlConnection sql_con = new SqlConnection(@"server =192.168.100.92;database = simpleDatabase ; user id = log1; password=P@ssword1965** ;MultipleActiveResultSets = True;"))

            {
                if (sql_con.State == ConnectionState.Closed)
                    sql_con.Open();

                string query = $"select id ,Marque,Immatriculation  ,id_v,  KILOMÉTRAGE_V ,  Date_V ,  Taux  from(select  n.id as id, v.Marque as Marque, v.matricule as Immatriculation, v.id as id_v, n.[KILOMÉTRAGE] as KILOMÉTRAGE_V, n.[DATE] as Date_V, n.[Taux] as Taux,row_number() over(partition by v.id  order by n.[DATE]  desc) as rn from vehicules v inner join [NOUVEAU] n on n.[VEHICULE] = v.id) as T where rn = 1; ";
                anticipationBindingSource.DataSource = sql_con.Query<Anticipation>(query, commandType: CommandType.Text);


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


        private void select_old_Data()
        {
            try
            {

            
            using (SqlConnection sql_con = new SqlConnection(@"server =192.168.100.92;database = simpleDatabase ; user id = log1; password=P@ssword1965** ;MultipleActiveResultSets = True;"))

            {

                if (sql_con.State == ConnectionState.Closed)
                    sql_con.Open();

                string query = $"select r.[n/] as numero , r.id ,r.[nom] as 'Nom',r.[Carburant] as Carburant ,r.[first_kilometrage] as kilometrage,v.Marque as Marque,v.matricule as Immatriculation , v.id as id_vehecule ,r.[_date] as 'Date' , r.[datenow] as datenow from [Reaparation] r inner join vehicules v on r.vehecule = v.id order by r.[_date] DESC";

                reaparationBindingSource.DataSource = sql_con.Query<Reaparation>(query, commandType: CommandType.Text);


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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var rowM = gridView1.FocusedRowHandle;

          
            if (rowM >= 0)
                popupMenu1.ShowPopup(barManager1, new Point(MousePosition.X, MousePosition.Y));
            else
                popupMenu1.HidePopup();
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView1.FocusedRowHandle;
            //gridView1.SelectRows[gridView1.Rows.Count - 1].Cells[colID].Selected = true;


            //string value = gridView1.GetFocusedDataRow()["id"].ToString();
            //update_deplacement update = new update_deplacement(value);
            //update.ShowDialog();

        }

        private void ajouter_car_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            VEHECULES VS = new VEHECULES();
            VS.ShowDialog();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                notifyIcon1.BalloonTipText = "votre application a été réduite dans la barre d'état système";
                notifyIcon1.ShowBalloonTip(1000);
                notifyIcon1.Visible = true;


            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void gridView2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var rowM = gridView2.FocusedRowHandle;

         
            if (rowM >= 0)
                popupMenu2.ShowPopup(barManager1, new Point(MousePosition.X, MousePosition.Y));
            else
                popupMenu2.HidePopup();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
           
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //ovrire nouveau

            DataRow red = gridView1.GetFocusedDataRow();
            
            var row = gridView1.FocusedRowHandle;
            string cellidV;
            DateTime cell_date;
             string cellMarque;
            string cell_kelo;
            string cellImmatriculation;

            cellidV = gridView1.GetRowCellValue(row, "id_v").ToString();
            cellMarque = gridView1.GetRowCellValue(row, "Marque").ToString();
            cellImmatriculation = gridView1.GetRowCellValue(row, "Immatriculation").ToString();
            cell_date = Convert.ToDateTime(gridView1.GetRowCellValue(row, "Date_V"));
            cell_kelo = gridView1.GetRowCellValue(row, "KILOMÉTRAGE_V").ToString();



            add_new add_New1 = new add_new(cell_date, cellidV,cellMarque,cellImmatriculation, cell_kelo);
            add_New1.ShowDialog();
            select_new_Data();
            select_old_Data();





        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //imprimer 

            var row2 = gridView2.FocusedRowHandle;
            string cellid;
            cellid = gridView2.GetRowCellValue(row2, "id").ToString();

            try { 
            if (Program.sql_con.State == ConnectionState.Closed)
                Program.sql_con.Open();
            string query = $" select  [_description] from [dbo].[descriptions] where [id_Reaparation] = { cellid}";
            List<description> descriptions = Program.sql_con.Query<description>(query, commandType: CommandType.Text).ToList();
            using (printfrm frm = new printfrm())
            {
                frm.PrintInvoice(int.Parse(cellid), descriptions);
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();

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

        private void delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row2 = gridView2.FocusedRowHandle;
            string cellid;
            cellid = gridView2.GetRowCellValue(row2, "id").ToString();


            try { 


            using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Reaparation WHERE id = @id", Program.sql_con))
            {


                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                deleteCommand.Parameters.AddWithValue("@id", int.Parse(cellid));

                deleteCommand.ExecuteNonQuery();



            }
            gridView2.DeleteRow(row2);
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

        private void update_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var row2 = gridView2.FocusedRowHandle;
            string cellid;          
            string cellidV;
            DateTime cell_date;
            string cellMarque;
            string cell_kelo;
            string cellImmatriculation;
            string cellNom;
            string cellCarb;
            string cellnumero;
            DateTime cell_newdate;


            cellid = gridView2.GetRowCellValue(row2, "id").ToString();
            cellidV = gridView2.GetRowCellValue(row2, "id_vehecule").ToString();
            cellMarque = gridView2.GetRowCellValue(row2, "Marque").ToString();
            cellImmatriculation = gridView2.GetRowCellValue(row2, "Immatriculation").ToString();
            cell_date = Convert.ToDateTime(gridView2.GetRowCellValue(row2, "Date"));
            cell_kelo = gridView2.GetRowCellValue(row2, "kilometrage").ToString();
            cellNom = gridView2.GetRowCellValue(row2, "Nom").ToString();
            cellCarb = gridView2.GetRowCellValue(row2, "Carburant").ToString();
            cellnumero = gridView2.GetRowCellValue(row2, "numero").ToString();
            cell_newdate = Convert.ToDateTime(gridView2.GetRowCellValue(row2, "datenow").ToString());





            add_new add_New2 = new add_new(cellnumero,int.Parse(cellid),int.Parse( cellidV), cellMarque , cellImmatriculation , cell_date  , cell_kelo, cellNom , cellCarb, cell_newdate);
            add_New2.ShowDialog();
            select_new_Data();
            select_old_Data();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            select_new_Data();
            select_old_Data();

            try { 
            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

            {
                SqlCommand cmd = Program.sql_con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT * from NOUVEAU where [Taux] > = 95";
                DataTable table = new DataTable();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    //listBoxControl1.Items.Add(row["_description"].ToString());
                    //MessageBox.Show("test");

                }
                    toastNotificationsManager1.ShowNotification("c19f92f7-3e15-4e42-a3cc-f14b50caf736");




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

        private void toastNotificationsManager1_Activated(object sender, DevExpress.XtraBars.ToastNotifications.ToastNotificationEventArgs e)
        {
            switch (e.NotificationID.ToString())
            {
                case "c19f92f7-3e15-4e42-a3cc-f14b50caf736":
                    this.WindowState = FormWindowState.Maximized;
                    break;
            }
        }

        private void barButtonItem6_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ziouanes/D-CLARATION_DU_R-PARATION");

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:contact@hamza-ziouane.tech");

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
            Process.Start("C:\\Program Files (x86)\\hamza\\registre_de_carburant\\simpleDatabase7.exe");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }

            //System.Diagnostics.Process.Start( @"hamza\registre_de_carburant\simpleDatabase7.exe");
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Process.Start("C:\\Program Files (x86)\\hamza\\FACTURE_DE_REPARATION\\simpleDatabase7.exe");

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
    }
}