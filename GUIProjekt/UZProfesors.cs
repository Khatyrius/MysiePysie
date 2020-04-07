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
    public partial class UZProfesors : Form
    {

        PROFESSORS prof = new PROFESSORS();

        public UZProfesors()
        {
            InitializeComponent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxFirst.Text = "";
            textBoxLast.Text = "";
            textBoxPhone.Text = "";
            textBoxDegree.Text = "";
            textBoxEmail.Text = "";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            String fname = textBoxFirst.Text;
            String lname = textBoxLast.Text;
            String degree = textBoxDegree.Text;
            String phone = textBoxPhone.Text;
            String email = textBoxEmail.Text;
            Boolean insertProfessors = prof.insertProfessors(fname, lname, degree, phone, email);

            if (insertProfessors)
            {
                MessageBox.Show("new Professor insertes successful","Professors", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Client not inserted", "ProfessorsError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
