using SetupCardGenerator.Service;
using System;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmAbout : Form
    {
        public string Author { get; set; }

        public string Detail { get; set; }

        public DateTime CreateDate { get; set; }

        public FrmAbout()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtAuthor.Text.Length == 0 || txtDetail.Text.Length == 0)
            {
                Msg.Exclamation("Поля не должны быть пустыми");
                return;
            }
            Author = txtAuthor.Text;
            Detail = txtDetail.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            txtDate.Text = CreateDate.ToString();
            txtDetail.Text = Detail;
            txtAuthor.Text = Author;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
