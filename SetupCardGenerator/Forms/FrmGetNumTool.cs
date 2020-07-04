using SetupCardGenerator.Service;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmGetNumTool : Form
    {
        private Set set;

        private ToolSetup toolSetup;

        public string T { get; set; }

        public string D { get; set; }

        public string ToolSetupName { get; set; }


        private FrmGetNumTool()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
        }

        public FrmGetNumTool(Set s) : this()
        {
            set = s;
        }

        public FrmGetNumTool(Set s, ToolSetup ts) : this(s)
        {
            toolSetup = ts;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txt_D.Text.Length == 0 && txt_T.Text.Length == 0 && txtName.Text.Length == 0)
            {
                Msg.Exclamation("Поля не должны быть пустыми");
                return;
            }
            T = txt_T.Text;
            D = txt_D.Text;
            ToolSetupName = txtName.Text;
            DialogResult = DialogResult.OK;
        }

        private void FrmGetNumTool_Load(object sender, EventArgs e)
        {
            if (toolSetup != null)
            {
                txtName.Text = toolSetup.Name;
                txt_D.Text = toolSetup.Num_D.ToString();
                txt_T.Text = toolSetup.Num_T.ToString();
            }
            else
            {
                if (set.ToolSetups != null && set.ToolSetups.Count != 0)
                    txt_T.Text = (set.ToolSetups.Max(x => Convert.ToInt32(x.Num_T)) + 1).ToString();
                else
                    txt_T.Text = "1";
                txt_D.Text = "1";
            }
        }
    }
}
