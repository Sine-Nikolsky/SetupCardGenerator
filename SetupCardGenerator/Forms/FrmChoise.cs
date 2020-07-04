using System;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmChoise : Form
    {
        public FrmChoise()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
