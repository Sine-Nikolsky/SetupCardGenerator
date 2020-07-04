using SetupCardGenerator.DPO;
using SetupCardGenerator.Model;
using SetupCardGenerator.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmGetTool : Form
    {
        List<Tool> _toolsDB;

        List<PartOfToolSetup> toolSetup;

        public List<PartOfToolSetup> retTool;

        private List<PartOfToolSetupDPO> dpo;
        private FrmGetTool()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
        }

        public FrmGetTool(List<Tool> t, List<PartOfToolSetup> ts) : this()
        {
            _toolsDB = t;
            toolSetup = ts;
            retTool = new List<PartOfToolSetup>();
            foreach (var i in toolSetup)
            {
                retTool.Add(i);
            }
        }

        private void FrmGetTool_Load(object sender, EventArgs e)
        {
            database.DataSource = _toolsDB.ToList();
            toolStripTextBox1.Focus();
            UpdateGrid();
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (setup.SelectedRows.Count == 0)
                return;
            retTool.Remove(retTool.First(x => x.Tool.Id == Guid.Parse(setup.SelectedRows[0].Cells[0].Value.ToString())));
            toolStripTextBox1.Focus();
            UpdateGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (database.SelectedRows.Count == 0)
                return;
            var frm = new FrmGetNum();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            PartOfToolSetup partOfToolSetup = new PartOfToolSetup();
            partOfToolSetup.Tool = _toolsDB.First(x => x.Id == Guid.Parse(database.SelectedRows[0].Cells[0].Value.ToString()));
            partOfToolSetup.Count = frm.Count;
            retTool.Add(partOfToolSetup);

            UpdateGrid();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Length != 0)
            {
                string key = "*" + toolStripTextBox1.Text + "*";
                database.DataSource = _toolsDB.Where(x => x.Name.ToUpper().Contains(toolStripTextBox1.Text.ToUpper())).OrderBy(x => x.Name).ToList();
            }
        }

        private void UpdateGrid()
        {
            dpo = new List<PartOfToolSetupDPO>();

            foreach (var item in retTool)
                dpo.Add(new PartOfToolSetupDPO(item));
            setup.DataSource = dpo.ToList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (setup.SelectedRows.Count == 0)
            {
                Msg.Exclamation("Выберите элемент сборки");
                return;
            }
            PartOfToolSetup p = retTool.First(x => x.Tool.Id == Guid.Parse(setup.SelectedRows[0].Cells[0].Value.ToString()));
            var frm = new FrmGetNum();
            frm.Count = p.Count;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                p.Count = frm.Count;
                UpdateGrid();
            }
        }
    }
}
