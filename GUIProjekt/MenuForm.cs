using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIProjekt
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void uZProfesorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UZProfesors prof = new UZProfesors();
            prof.ShowDialog();
        }

        private void uZStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UZStudents stud = new UZStudents();
            stud.ShowDialog();
        }

        private void uZSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UZSupports supp = new UZSupports();
            supp.ShowDialog();
        }
    }
}
