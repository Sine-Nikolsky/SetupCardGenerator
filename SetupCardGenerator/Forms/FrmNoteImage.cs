using SetupCardGenerator.Service;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmNoteImage : Form
    {
        public string Note { get; set; }

        public FrmNoteImage()
        {
            InitializeComponent();
        }

        private void FrmNoteImage_Load(object sender, EventArgs e)
        {
            txtAbout.Text = Note;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtAbout.Text.Count() == 0)
            {
                Msg.Exclamation("Введите описание изображения");
                return;
            }
            DialogResult = DialogResult.OK;
            Note = txtAbout.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
