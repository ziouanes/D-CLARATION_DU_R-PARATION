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
    }
}