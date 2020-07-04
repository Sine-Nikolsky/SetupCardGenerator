using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmImputText : Form
    {
        public string Value { get; set; }

        public string Machine { get; set; }

        public Set Set { get; set; }

        private List<Machine> machines;
        private FrmImputText()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
        }

        public FrmImputText(List<Machine> m) : this()
        {
            machines = m;
        }

        public FrmImputText(List<Machine> m, Set s) : this(m)
        {
            Set = s;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtValue.Text.Length != 0 && cmbMachines.SelectedIndex != -1)
            {
                Value = txtValue.Text;
                Machine = ((Machine)cmbMachines.SelectedItem).ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void FrmImputText_Load(object sender, EventArgs e)
        {
            cmbMachines.DataSource = machines;
            if (Set != null)
            {
                txtValue.Text = Set.SetName;
                int index = machines.FindIndex(x => x.Name.Equals(Set.Machine));
                cmbMachines.SelectedIndex = index;
            }
        }
    }
}
