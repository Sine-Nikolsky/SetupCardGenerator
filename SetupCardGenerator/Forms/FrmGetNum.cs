using SetupCardGenerator.Service;
using System;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmGetNum : Form
    {
        public int Count { get; set; }
        public FrmGetNum()
        {
            InitializeComponent();
        }

        private void nmrCount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(this, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            nmrCount.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (nmrCount.Text.Length == 0)
            {
                Msg.Exclamation("Поле \"Количество\" не должно быть пустым");
                return;
            }
            Count = (int)nmrCount.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nmrCount_Enter(object sender, EventArgs e)
        {
            nmrCount.Select(0, nmrCount.Text.Length);
        }

        private void FrmGetNum_Load(object sender, EventArgs e)
        {
            nmrCount.Value = Count == 0 ? 1 : Count;
        }
    }
}
