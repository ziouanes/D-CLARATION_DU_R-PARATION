using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace DXApplication1
{
    public partial class Report_declaration : DevExpress.XtraReports.UI.XtraReport
    {
        public Report_declaration()
        {
            InitializeComponent();
        }
        public void InitData(string n, string nom, string Carburant, string Marque, string matricule, string first_kilometrage, string _date,string _datenow, List<description> description)
        {
            Parameters["parameter_n"].Value = n;
            Parameters["parameter_nom"].Value = nom;
            Parameters["parameter_Carburant"].Value = Carburant;
            Parameters["parameter_Marque"].Value = Marque;
            Parameters["parameter_matricule"].Value = matricule;
            Parameters["parameter_first_kilometrage"].Value = first_kilometrage;
            Parameters["parameter__date"].Value = _date;
            Parameters["parameter_datenow"].Value = _datenow;

            objectDataSource1.DataSource = description;

        }

    }
}
