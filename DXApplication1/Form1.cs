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
using Dapper;

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            select_new_Data();
            select_old_Data();
        }

        private void select_new_Data()
        {
            using (SqlConnection sql_con = new SqlConnection(@"server =192.168.100.92;database = simpleDatabase ; user id = log1; password=P@ssword1965** ;MultipleActiveResultSets = True;"))

            {
                if (sql_con.State == ConnectionState.Closed)
                    sql_con.Open();

                string query = $"select id ,Marque,Immatriculation  ,id_v,  KILOMÉTRAGE_V ,  Date_V ,  Taux  from(select  n.id as id, v.Marque as Marque, v.matricule as Immatriculation, v.id as id_v, n.[KILOMÉTRAGE] as KILOMÉTRAGE_V, n.[DATE] as Date_V, n.[Taux] as Taux,row_number() over(partition by v.id  order by n.[DATE]  desc) as rn from vehicules v inner join [NOUVEAU] n on n.[VEHICULE] = v.id) as T where rn = 1; ";
                anticipationBindingSource.DataSource = sql_con.Query<Anticipation>(query, commandType: CommandType.Text);


            }
        }


        private void select_old_Data()
        {
            using (SqlConnection sql_con = new SqlConnection(@"server =192.168.100.92;database = simpleDatabase ; user id = log1; password=P@ssword1965** ;MultipleActiveResultSets = True;"))

            {

                if (sql_con.State == ConnectionState.Closed)
                    sql_con.Open();

                string query = $"select r.id ,r.[nom] as 'Nom',r.[Carburant] as Carburant ,r.[first_kilometrage] as kilometrage,v.Marque as Marque,v.matricule as Immatriculation , v.id as id_vehecule ,r.[_date] as 'Date' from [Reaparation] r inner join vehicules v on r.vehecule = v.id";

                reaparationBindingSource.DataSource = sql_con.Query<Reaparation>(query, commandType: CommandType.Text);


            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var rowM = gridView1.FocusedRowHandle;
            var focusRowView = (DataRowView)gridView1.GetFocusedRow();
            if (focusRowView == null || focusRowView.IsNew) return;
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
            toastNotificationsManager1.ShowNotification("c19f92f7-3e15-4e42-a3cc-f14b50caf736");

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
    }
}