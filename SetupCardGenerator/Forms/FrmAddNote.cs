using SetupCardGenerator.Model;
using SetupCardGenerator.Service;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmAddNote : Form
    {
        private Set set;

        public Note Note { get; set; }

        public FrmAddNote(Set s)
        {
            set = s;
            InitializeComponent();
        }

        public FrmAddNote(Set s, Note n) : this(s)
        {
            Note = n;
        }

        private void FrmAddNote_Load(object sender, EventArgs e)
        {
            if (set.Notes == null)
                return;
            if (Note == null)
            {
                if (set.Notes.Count == 0)
                    txtNumOp.Text = "1";
                else
                    txtNumOp.Text = (set.Notes.Max(x => x.Num) + 1).ToString();
            }
            else
            {
                txtNumOp.Text = Note.Num.ToString();
            }
            var tmp = set.ToolSetups.OrderBy(x => x.Num_T).ThenBy(x => x.Num_D).ToList();
            cmbTools.DataSource = tmp;

            if (Note != null)
            {
                if (Note.Tool != null)
                {
                    chkWithoutTool.Checked = false;
                    int index = tmp.IndexOf(Note.Tool);
                    if (index == -1)
                    {
                        Msg.Exclamation("В установе сейчас отсутствует та инструментальная сборка, которая записана в редактируемом переходе.\n" +
                            "Возможно она была удалена. Выберите новый инструмент");
                    }
                    else
                    {
                        cmbTools.SelectedIndex = index;
                    }
                    txtNote.Text = Note.Description;
                }
                else
                {
                    txtNote.Text = Note.Description;
                    chkWithoutTool.Checked = true;
                }
                nmrCoeff.Value = Convert.ToDecimal(Note.Coeff);
                nmrTime.Value = Convert.ToDecimal(Note.Time);
            }

        }

        private void AddSpecText(object sender, EventArgs e)
        {
            ToolStripButton ts = (ToolStripButton)sender;
            int index = txtNote.SelectionStart;

            txtNote.Text = txtNote.Text.Insert(index, ts.Text);
            txtNote.Focus();
            if (index < 0)
                txtNote.SelectionStart = 0;
            else
                txtNote.SelectionStart = index + 1;

        }

        private void chkWithoutTool_CheckedChanged(object sender, EventArgs e)
        {
            nmrCoeff.Enabled = !chkWithoutTool.Checked;
            cmbTools.Enabled = !chkWithoutTool.Checked;
        }

        private void nmrCoeff_Enter(object sender, EventArgs e)
        {
            nmrCoeff.Select(0, nmrCoeff.Text.Length);
        }

        private void nmrCoeff_Click(object sender, EventArgs e)
        {
            nmrCoeff.Select(0, nmrCoeff.Text.Length);
        }

        private void nmrTime_Click(object sender, EventArgs e)
        {
            nmrTime.Select(0, nmrTime.Text.Length);
        }

        private void nmrTime_Enter(object sender, EventArgs e)
        {
            nmrTime.Select(0, nmrTime.Text.Length);
        }

        private void ShowToolInfo(object sender, EventArgs e)
        {
            if (!chkWithoutTool.Checked)
                pictureBox1.Image = cmbTools.Items.Count != 0 ? ((ToolSetup)cmbTools.SelectedItem).Image : null;
            else
                pictureBox1.Image = null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Note == null)
                Note = new Note();
            Note.Num = Int32.Parse(txtNumOp.Text);
            if (!chkWithoutTool.Checked)
            {
                Note.Tool = (ToolSetup)cmbTools.SelectedItem;
                Note.Coeff = (int)nmrCoeff.Value;
            }
            else
            {
                Note.Tool = null;
                Note.Coeff = 0;
            }
            Note.Time = Math.Round((double)nmrTime.Value, 2);

            Note.Description = txtNote.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CalculateTool(object sender, EventArgs e)
        {
            if(nmrCoeff.Value == 0 || nmrTime.Value == 0 || chkWithoutTool.Checked)
            {
                lblResult.Text = "Норма расхода не посчитана.";
                return;
            }
            PartOfToolSetup tool = ((ToolSetup)cmbTools.SelectedItem).Tools.First(x => x.Tool.Type.Equals("РИ"));
            decimal d = (nmrTime.Value * (decimal)tool.Count) / (15.0M * nmrCutCount.Value * (decimal)nmrCoeff.Value);
            lblResult.Text = $"Норма расхода составляет {Math.Round(d, 4)}. 1 шт РИ хватит для обработки {Math.Round(1/d, 0)} деталей";
        }

        private void nmrCutCount_Click(object sender, EventArgs e)
        {
            nmrCutCount.Select(0, nmrTime.Text.Length);
        }
    }
}
