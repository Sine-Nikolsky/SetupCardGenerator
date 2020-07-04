using NLog;
using SetupCardGenerator.DPO;
using SetupCardGenerator.Forms;
using SetupCardGenerator.Model;
using SetupCardGenerator.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SetupCardGenerator
{

    public partial class FrmMainWindow : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Project Project { get; set; }

        private string pathToProject;
        public List<Tool> Tools { get; set; }

        public List<Machine> Machines { get; set; }
        public FrmMainWindow()
        {
            InitializeComponent();
        }

        public FrmMainWindow(List<Tool> tools, List<Machine> machines) : this()
        {
            Tools = tools;
            Machines = machines;
        }

        private void btnAddSet_Click(object sender, EventArgs e)
        {
            if (Project.Sets == null)
            {
                Project.Sets = new List<Set>();
            }

            var frm = new FrmImputText(Machines);
            frm.StartPosition = FormStartPosition.CenterParent;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Set set = new Set();
                set.ToolSetups = new List<ToolSetup>();
                set.SetName = frm.Value;
                set.Machine = frm.Machine;
                if (Project.Sets.Any(x => x.SetName.Equals(set.SetName)))
                {
                    Msg.Error("Установ с таким названием уже существует");
                    return;
                }
                Project.Sets.Add(set);
                int index = lstSets.Items.Count - 1;
                UpdateSetList();
                lstSets.SelectedIndex = index + 1;
            }

        }

        private void btnEditSet_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            Set edited = Project.Sets.Find(x => x.SetName.Equals(((Set)lstSets.SelectedItem).SetName));
            var frm = new FrmImputText(Machines, edited);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (Project.Sets.Any(x => x.SetName.Equals(frm.Value) && x.Machine.Equals(frm.Machine)))
                {
                    Msg.Error("Такой установ уже существует. Выберите другое имя");
                    return;
                }
                edited.Machine = frm.Machine;
                edited.SetName = frm.Value;
                int index = lstSets.SelectedIndex;
                UpdateSetList();
                lstSets.SelectedIndex = index;
            }

        }

        private void btnDeleteSet_Click(object sender, EventArgs e)
        {
            if (Project.Sets == null)
            {
                Msg.Error("Набора установов не существует в текущем проекте");
            }
            if (lstSets.SelectedIndex == -1)
                return;
            if (Msg.Question("Удалить запись?") == DialogResult.OK)
            {
                Set set = Project.Sets.First(x => x.SetName.Equals(((Set)lstSets.SelectedItem).SetName) && x.Machine.Equals(((Set)lstSets.SelectedItem).Machine));
                int index = lstSets.SelectedIndex;
                int count = lstSets.Items.Count - 1;
                Project.Sets.Remove(set);
                UpdateSetList();
                if (count == 0)
                    return;
                else
                {
                    if (index - 1 <= 0)
                        lstSets.SelectedIndex = 0;
                    else
                    {
                        lstSets.SelectedIndex = index - 1;
                    }
                }
            }
        }

        private void btnToolSetupAdd_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Установ не выбран");
                return;
            }

            ToolSetup ts = new ToolSetup(); ;
            Set s = (Set)lstSets.SelectedItem;
            var frm = new FrmGetNumTool(s);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ts.Name = frm.ToolSetupName;
                ts.Num_D = Convert.ToInt32(frm.D);
                ts.Num_T = Convert.ToInt32(frm.T);
                if (s.ToolSetups.Any(x => x.Num_D == ts.Num_D && x.Num_T == ts.Num_T))
                {
                    Msg.Error($"В этом установе уже есть инструмент в гнезде {ts.Num_T} с корректором {ts.Num_D}. Сборка не создана");
                    return;
                }
                ts.OutHand = 30;
                ts.Tools = new List<PartOfToolSetup>();
                Project.Sets.First(x => x.Id == s.Id).ToolSetups.Add(ts);
                UpdateToolSetupList();
                lstToolSetups.SelectedIndex = lstToolSetups.Items.IndexOf(ts);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;

            ToolSetup ts = (ToolSetup)lstToolSetups.SelectedItem;

            Set s = (Set)lstSets.SelectedItem;

            var frm = new FrmGetNumTool(s, ts);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                bool tChanged, dChanged, nameChanged;
                int i = ts.Num_T;
                int j = ts.Num_D;

                nameChanged = ts.Name != frm.ToolSetupName;
                dChanged = ts.Num_D != Convert.ToInt32(frm.D);
                tChanged = ts.Num_T != Convert.ToInt32(frm.T);

                if (nameChanged)
                    ts.Name = frm.ToolSetupName;
                if (dChanged)
                    j = Convert.ToInt32(frm.D);
                if (tChanged)
                    i = Convert.ToInt32(frm.T);

                if (tChanged || dChanged)
                {
                    if (s.ToolSetups.Any(x => x.Num_T == i))
                    {
                        if (s.ToolSetups.Any(x => x.Num_D == j && x.Num_T == i))
                        {
                            Msg.Error($"В этом установе уже есть инструмент в гнезде {i} с корректором {j}. Изменения не внесены");
                            return;
                        }
                    }
                    ts.Num_D = j;
                    ts.Num_T = i;
                }
                ToolSetup t = Project.Sets.First(x => x.Id == s.Id).ToolSetups.First(x => x.Id == ts.Id);
                t = ts;
                UpdateToolSetupList();
                lstToolSetups.SelectedIndex = lstToolSetups.Items.IndexOf(t);
            }
        }

        private void btnToolSetupDelete_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;
            ToolSetup ts = (ToolSetup)lstToolSetups.SelectedItem;
            if (Msg.Question("Удалить сборку инструмента?") == DialogResult.OK)
            {
                int index = lstToolSetups.Items.IndexOf(ts);
                Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id).ToolSetups.Remove(ts);
                UpdateToolSetupList();
                if (index <= 0)
                    lstToolSetups.SelectedIndex = 0;
                else
                    lstToolSetups.SelectedIndex = index - 1;
            }
        }

        private void btnSaveFavoriteToolSetup_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;
            ToolSetup ts = (ToolSetup)lstToolSetups.SelectedItem;
            try
            {
                using (var saveFile = new SaveFileDialog())
                {
                    saveFile.InitialDirectory = Application.StartupPath + @"\Res\ToolSetup\";
                    saveFile.Filter = "Сборка инструмента (*.scgts) | *.scgts";
                    saveFile.FileName = $"{ts.Name}";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFile.FileName;
                        Stream SaveFileStream = File.Create(path);
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(SaveFileStream, ts);
                        SaveFileStream.Close();
                        Msg.Information($"Сборка инструмента сохранена в {path}");
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "{0} Ошибка сохранение сборки инструмента", Environment.UserName);
                Msg.Error("Ошибка сохранения сборки инструмента");
            }
        }

        private void btnToolAdd_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ.\nСначала выберите установ.");
                return;
            }
            if (lstToolSetups.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбрана сборка инструмента.\nСначала выберите сборку.");
                return;
            }

            ToolSetup ts = (ToolSetup)lstToolSetups.SelectedItem;

            var frm = new FrmGetTool(Tools, ts.Tools);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
                ToolSetup t = s.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
                t.Tools = frm.retTool;
                UpdateToolSetup();
            }
        }

        private void btnToolEdit_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ.\nСначала выберите установ.");
                return;
            }
            if (lstToolSetups.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбрана сборка инструмента.\nСначала выберите сборку.");
                return;
            }
            if (dgvTools.SelectedRows.Count == 0)
            {
                Msg.Exclamation("Не выбран инструмент.\nВыберите инструмент из таблицы");
                return;
            }
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
            PartOfToolSetup p = toolSetup.Tools.First(x => x.Tool.Id == Guid.Parse(dgvTools.SelectedRows[0].Cells[0].Value.ToString()));

            var frm = new FrmGetNum();
            frm.Count = p.Count;
            if (frm.ShowDialog() == DialogResult.OK)
                p.Count = frm.Count;
            UpdateToolSetup();
        }

        private void btnToolDelete_Click(object sender, EventArgs e)
        {
            if (Msg.Question("Удалить инструмент из сборки?") == DialogResult.OK)
            {
                if (lstSets.SelectedIndex == -1)
                {
                    Msg.Exclamation("Не выбран установ.\nСначала выберите установ.");
                    return;
                }
                if (lstToolSetups.SelectedIndex == -1)
                {
                    Msg.Exclamation("Не выбрана сборка инструмента.\nСначала выберите сборку.");
                    return;
                }
                Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
                ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);

                toolSetup.Tools.Remove(toolSetup.Tools.First(x => x.Tool.Id == Guid.Parse(dgvTools.SelectedRows[0].Cells[0].Value.ToString())));
                UpdateToolSetup();
            }

        }

        private void btnImageAdd_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
            try
            {
                var ofd = new OpenFileDialog();
                ofd.InitialDirectory = Application.StartupPath + @"\Res\ToolPic\";
                ofd.Filter = "Изображение (*.jpg; *.jpeg;)|*.jpg; *.jpeg;";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    toolSetup.Image = Image.FromFile(ofd.FileName);
                    UpdateToolSetup();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка добавления эскиза сборки инструмента", Environment.UserName);
                Msg.Error("Ошибка добавления эскиза сборки инструмента");
            }
        }

        private void btnImageRemove_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
            toolSetup.Image = null;
            UpdateToolSetup();
        }

        private void btnSaveToolSetup_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(this, e);
        }

        private void btnResetToolSetup_Click(object sender, EventArgs e)
        {
        }

        private void txtOutHand_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void txtCountCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
        }

        private void FrmMainWindow_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            ProjectIsLoaded();
        }

        private void ProjectIsLoaded()
        {
            if (Project == null)
            {
                groupSets.Enabled = false;
                groupWorkArea.Enabled = false;
                menuStrip.Enabled = true;
            }
            else
            {
                groupSets.Enabled = true;
                groupWorkArea.Enabled = true;
                UpdateSetList();
            }
        }

        private string GetPath()
        {
            string path = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path += fbd.SelectedPath;
            }
            return path;
        }

        private void UpdateSetList()
        {
            if (Project.Sets == null)
                return;
            lstSets.DataSource = Project.Sets.ToList();
        }

        private void UpdateToolSetupList()
        {
            if (Project == null)
                return;
            if (Project.Sets == null)
                return;
            if (lstSets.SelectedIndex == -1)
                return;

            lstToolSetups.DataSource = new List<ToolSetup>();
            dgvTools.DataSource = new List<PartOfToolSetupDPO>();
            imgTool.Image = null;
            txtOutHand.Text = "1";

            Set s = (Set)lstSets.SelectedItem;
            if (s.ToolSetups == null && s.ToolSetups.Count == 0)
            {
                lstToolSetups.DataSource = new List<ToolSetup>();
                dgvTools.DataSource = new List<Tool>();
                imgTool.Image = null;
                txtOutHand.Text = "1";
                return;
            }
            lstToolSetups.DataSource = s.ToolSetups.OrderBy(x => x.Num_T).ThenBy(x => x.Num_D).ToList();
        }

        private void lstSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateToolSetupList();
                UpdateSetupScheme();
                UpdateNoteList();
            }
            catch(Exception ex)
            {
                logger.Error($"{ex.Message} {ex.StackTrace}");
            }
        }

        private void lstToolSetups_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateToolSetup();
        }

        private void UpdateToolSetup()
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;

            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

            ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
            toolSetup.Tools = toolSetup.Tools.OrderBy(x => x.Tool.Type).ThenBy(x => x.Tool.Name).ToList();


            List<PartOfToolSetupDPO> dpo = new List<PartOfToolSetupDPO>();

            foreach (PartOfToolSetup item in toolSetup.Tools)
                dpo.Add(new PartOfToolSetupDPO(item));

            dgvTools.DataSource = dpo.ToList();
            txtOutHand.Text = toolSetup.OutHand.ToString();
            imgTool.Image = toolSetup.Image;
        }

        private void openDBFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\Res\Data\");
        }

        private void openResoursesFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\Res\ToolPic\");
        }

        private void открытьПапкуСМоимиСборкамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\Res\ToolSetup\");
        }

        private void открытьПапкуСПроектамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + @"\Res\MyProject\");
        }

        private void txtOutHand_TextChanged(object sender, EventArgs e)
        {
            if (txtOutHand.Text.Length == 0 || int.Parse(txtOutHand.Text) == 0)
            {
                Msg.Error("Поля не должны быть пустыми и их значения не должны быть равны 0");
                return;
            }
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
            toolSetup.OutHand = int.Parse(txtOutHand.Text);
            UpdateToolSetup();
        }

        private void txtCountCut_TextChanged(object sender, EventArgs e)
        {
            if (txtOutHand.Text.Length == 0)
            {
                Msg.Error("Поля не должны быть пустыми и их значения не должны быть равны 0");
                return;
            }
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstToolSetups.SelectedIndex == -1)
                return;
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            ToolSetup toolSetup = set.ToolSetups.First(x => x.Id == ((ToolSetup)lstToolSetups.SelectedItem).Id);
            UpdateToolSetup();
        }

        private void btnAddSetupImage_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ. Для добавления схемы крепления создайте установ");
                return;
            }
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            try
            {
                var ofd = new OpenFileDialog();
                ofd.InitialDirectory = Application.StartupPath + @"\Res\SetupSchemas\";
                ofd.Filter = "Изображение (*.jpg; *.jpeg;)|*.jpg; *.jpeg;";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var f = new FrmNoteImage();
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        MyImage m = new MyImage(ofd.FileName);
                        m.FullPath = f.Note;
                        set.Scheme.Images.Add(m);
                        UpdateSetupScheme();
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "{0} Ошибка добавления эскиза схемы крепления", Environment.UserName);
                Msg.Error("Ошибка добавления эскиза схемы крепления");
            }
        }

        private void UpdateSetupScheme()
        {
            if (lstSets.SelectedIndex == -1)
            {
                setupImagePrewiew.Image = null;
                return;
            }

            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

            List<PartOfToolSetupDPO> list = new List<PartOfToolSetupDPO>();

            foreach (var item in set.Scheme.Gears)
            {
                list.Add(new PartOfToolSetupDPO
                {
                    Code = item.Tool.Code,
                    Id = item.Tool.Id,
                    Count = item.Count,
                    Name = item.Tool.Name,
                    Type = item.Tool.Type
                });
            }
            lstSetupImage.DataSource = set.Scheme.Images.ToList();
            gridGearTools.DataSource = list.ToList();
            txtNote.Text = set.Scheme.Note;

            if (lstSetupImage.SelectedIndex == -1)
            {
                setupImagePrewiew.Image = null;
                return;
            }
        }

        private void lstSetupImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            if (lstSetupImage.SelectedIndex == -1)
            {
                setupImagePrewiew.Image = null;
                return;
            }
            setupImagePrewiew.Image = ((MyImage)lstSetupImage.SelectedItem).Image;
        }

        private void setupImagePrewiew_Click(object sender, EventArgs e)
        {

        }

        private void setupImagePrewiew_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstSetupImage.SelectedIndex == -1)
            {
                Msg.Error("Изображение не выбрано");
                return;
            }
            var frm = new FrmImageViewer(((MyImage)lstSetupImage.SelectedItem).Image);
            frm.Show();
        }

        private void btnDeleteSetupImage_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ. Для добавления схемы крепления создайте установ");
                return;
            }
            if (lstSetupImage.SelectedIndex == -1)
            {
                Msg.Error("Изображение не выбрано");
                return;
            }
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            set.Scheme.Images.Remove(set.Scheme.Images.First(x => x.Id == ((MyImage)lstSetupImage.SelectedItem).Id));
            UpdateSetupScheme();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ. Для добавления приспособлений из базы данных создайте установ");
                return;
            }
            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

            var frm = new FrmGetTool(Tools, s.Scheme.Gears);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                s.Scheme.Gears = frm.retTool;
                UpdateSetupScheme();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtNote.Focus();
            int i = txtNote.SelectionStart;
            txtNote.Text = txtNote.Text.Insert(i, "Ø");
            txtNote.SelectionStart = i + 1;
            txtNote.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNote.Focus();
            int i = txtNote.SelectionStart;
            txtNote.Text = txtNote.Text.Insert(i, button4.Text);
            txtNote.SelectionStart = i + 1;
            txtNote.Focus();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtNote.Focus();
            int i = txtNote.SelectionStart;
            txtNote.Text = txtNote.Text.Insert(i, button7.Text);
            txtNote.SelectionStart = i + 1;
            txtNote.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtNote.Text += "";
            txtNote.Focus();
            txtNote.SelectionStart = txtNote.Text.Length;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtNote.Focus();
            int i = txtNote.SelectionStart;
            txtNote.Text = txtNote.Text.Insert(i, button3.Text);
            txtNote.SelectionStart = i + 1;
            txtNote.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ. Для добавления приспособлений из базы данных создайте установ");
                return;
            }
            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

            s.Scheme.Note = txtNote.Text;
        }

        private void FrmMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Project != null && Msg.Question("Сохранить проект перед закрытием программы?") == DialogResult.OK)
                сохранитьToolStripMenuItem_Click(this, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Msg.Question("Удалить инструмент?") == DialogResult.OK)
            {
                if (lstSets.SelectedIndex == -1)
                    return;
                Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

                set.Scheme.Gears.Remove(set.Scheme.Gears.First(x => x.Tool.Id == Guid.Parse(gridGearTools.SelectedRows[0].Cells[0].Value.ToString())));
                UpdateSetupScheme();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tabWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabWork.SelectedIndex == 2)
            {
                UpdateNoteList();
            }
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (Project == null)
                return;
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Выберите установ для добавления описания");
                return;
            }
            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

            FrmAddNote frm = new FrmAddNote(s);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                s.Notes.Add(frm.Note);
            }
            UpdateNoteList();
        }

        private void btnEditNote_Click(object sender, EventArgs e)
        {
            if (Project == null)
                return;
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Выберите установ");
                return;
            }
            if (gridNotes.SelectedRows.Count == 0)
            {
                Msg.Exclamation("Выберите запись для редактирования");
                return;
            }

            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            Note n = s.Notes.First(x => x.Id == (Guid.Parse(gridNotes.SelectedRows[0].Cells[0].Value.ToString())));
            var frm = new FrmAddNote(s, n);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                n = frm.Note;
            }
            UpdateNoteList();
        }

        private void UpdateNoteList()
        {
            if (Project == null)
                return;
            if (lstSets.SelectedIndex == -1)
                return;
            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            gridNotes.DataSource = s.Notes.ToList();

            double d = 0;
            foreach (var i in s.Notes)
            {
                d += i.Time;
            }
            lblAllTime.Text = string.Format($"{Math.Round(d, 3)} мин / {Math.Round(d / 60, 3)} ч");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            List<string> st = new List<string>();

            for (int i = 0; i < gridNotes.Rows.Count; i++)
            {
                Note n = s.Notes.First(x => x.Id == Guid.Parse(gridNotes.Rows[i].Cells[0].Value.ToString()));
                if (n.Tool == null)
                    continue;
                if (s.ToolSetups.Find(x => x.Id == n.Tool.Id) == null)
                {
                    st.Add($"{i}");
                }
            }
            if (st.Count != 0)
            {
                string ms = "В следующих строках выбран инструмент, который отсутствует в магазине установа:\n";
                foreach (var i in st)
                    ms += "Строка №" + (int.Parse(i) + 1).ToString() + Environment.NewLine;
                Msg.Exclamation(ms);
            }
            else
            {
                Msg.Information("Ошибок не обнаружено");
            }
        }

        private void btnDeleteNote_Click(object sender, EventArgs e)
        {
            if (Project == null)
                return;
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Выберите установ");
                return;
            }
            if (gridNotes.SelectedRows.Count == 0)
            {
                Msg.Exclamation("Выберите запись для редактирования");
                return;
            }

            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            Note n = s.Notes.First(x => x.Id == (Guid.Parse(gridNotes.SelectedRows[0].Cells[0].Value.ToString())));

            if (Msg.Question("Удалить запись?") == DialogResult.OK)
            {
                s.Notes.Remove(n);
                RenumNotes(s);
                UpdateNoteList();
            }
        }

        private void RenumNotes(Set s)
        {

            if (s.Notes.Count != 0)
            {
                for (int i = 0; i < s.Notes.Count; i++)
                    s.Notes[i].Num = i + 1;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            ChangeNoteIndex(-1);
        }

        private void ChangeNoteIndex(int index)
        {
            if (gridNotes.SelectedRows.Count == 0)
                return;

            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            Note n = s.Notes.First(x => x.Id == (Guid.Parse(gridNotes.SelectedRows[0].Cells[0].Value.ToString())));

            int curIndex = s.Notes.IndexOf(n);

            if (curIndex + index > s.Notes.Count - 1 || curIndex + index < 0)
                return;

            Note[] notes = s.Notes.ToArray();

            if (index < 0)
            {
                Note tmp = notes[curIndex - 1];
                notes[curIndex - 1] = notes[curIndex];
                notes[curIndex] = tmp;
            }
            else
            {
                Note tmp = notes[curIndex + 1];
                notes[curIndex + 1] = notes[curIndex];
                notes[curIndex] = tmp;
            }
            s.Notes = notes.ToList();
            UpdateNoteList();
            RenumNotes(s);
            gridNotes.CurrentCell = gridNotes.Rows[curIndex + index].Cells[1];
        }

        private void ChangeSetIndex(int index)
        {
            if (lstSets.SelectedIndex == -1)
                return;

            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);

            int curIndex = Project.Sets.IndexOf(s);

            if (curIndex + index > Project.Sets.Count - 1 || curIndex + index < 0)
                return;

            Set[] sets = Project.Sets.ToArray();

            if (index < 0)
            {
                Set tmp = sets[curIndex - 1];
                sets[curIndex - 1] = sets[curIndex];
                sets[curIndex] = tmp;
            }
            else
            {
                Set tmp = sets[curIndex + 1];
                sets[curIndex + 1] = sets[curIndex];
                sets[curIndex] = tmp;
            }
            Project.Sets = sets.ToList();
            UpdateSetList();
            lstSets.SelectedIndex = curIndex + index;
        }

        private void ChangeImageSetupIndex(int index)
        {
            if (lstSetupImage.SelectedIndex == -1)
                return;

            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            SetupScheme scheme = s.Scheme;

            MyImage i = scheme.Images.First(x => x.Id == ((MyImage)lstSetupImage.SelectedItem).Id);

            int curIndex = scheme.Images.IndexOf(i);

            if (curIndex + index > scheme.Images.Count - 1 || curIndex + index < 0)
                return;

            MyImage[] images = scheme.Images.ToArray();

            if (index < 0)
            {
                MyImage tmp = images[curIndex - 1];
                images[curIndex - 1] = images[curIndex];
                images[curIndex] = tmp;
            }
            else
            {
                MyImage tmp = images[curIndex + 1];
                images[curIndex + 1] = images[curIndex];
                images[curIndex] = tmp;
            }
            scheme.Images = images.ToList();
            UpdateSetupScheme();
            lstSetupImage.SelectedIndex = curIndex + index;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            ChangeNoteIndex(1);
        }

        private void сформироватьКНToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                Msg.Error("Проект отсутствует");
                return;
            }

            var frm = new FrmReport(Project);
            frm.ShowDialog();
        }

        private void cmAdd_Click(object sender, EventArgs e)
        {
            btnAddSet_Click(this, e);
        }

        private void cmEdit_Click(object sender, EventArgs e)
        {
            btnEditSet_Click(this, e);
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            btnDeleteSet_Click(this, e);
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnToolSetupAdd_Click(this, e);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(this, e);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnToolSetupDelete_Click(this, e);
        }

        private void ClearForm()
        {
            lstSets.DataSource = new List<Set>();
            lstToolSetups.DataSource = new List<ToolSetup>();
            dgvTools.DataSource = new List<PartOfToolSetupDPO>();
            imgTool.Image = null;
            txtOutHand.Text = "1";
            lstSetupImage.DataSource = new List<MyImage>();
            setupImagePrewiew.Image = null;
            txtNote.Text = "";
            gridGearTools.DataSource = new List<PartOfToolSetupDPO>();
            gridNotes.DataSource = new List<Note>();
            ProjectIsLoaded();
        }

        private void btnLoadToolSetup_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;
            Set s = (Set)lstSets.SelectedItem;
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath + @"\Res\ToolSetup\";
            ofd.Filter = "Сборка инструмента (*.scgts) | *.scgts";
            ToolSetup tsLoaded;
            try 
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;
                    Stream openFileStream = File.OpenRead(path);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    tsLoaded = new ToolSetup((ToolSetup)deserializer.Deserialize(openFileStream));
                    openFileStream.Close();

                    if (s.ToolSetups.Count != 0)
                        tsLoaded.Num_T = s.ToolSetups.Max(x => Convert.ToInt32(x.Num_T)) + 1;
                    else
                        tsLoaded.Num_T = 1;
                    var f = new FrmGetNumTool(s, tsLoaded);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        tsLoaded.Name = f.ToolSetupName;
                        tsLoaded.Num_D = Convert.ToInt32(f.D);
                        tsLoaded.Num_T = Convert.ToInt32(f.T);
                        if (s.ToolSetups.Any(x => x.Num_D == tsLoaded.Num_D && x.Num_T == tsLoaded.Num_T))
                        {
                            Msg.Error($"В этом установе уже есть инструмент в гнезде {tsLoaded.Num_T} с корректором {tsLoaded.Num_D}. Сборка не создана");
                            return;
                        }
                        Project.Sets.First(x => x.Id == s.Id).ToolSetups.Add(tsLoaded);
                        UpdateToolSetupList();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка загрузки сборки инструмента", Environment.UserName);
                Msg.Error("Ошибка загрузки сборки инструмента");
            }
        }

        private void btnSetUp_Click(object sender, EventArgs e)
        {
            ChangeSetIndex(-1);
        }

        private void btnSetDown_Click(object sender, EventArgs e)
        {
            ChangeSetIndex(1);
        }

        private void btnSetupImageUp_Click(object sender, EventArgs e)
        {
            ChangeImageSetupIndex(-1);
        }

        private void btnSetupImageDown_Click(object sender, EventArgs e)
        {
            ChangeImageSetupIndex(1);
        }

        private void btnSaveGears_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;

            Set s = (Set)lstSets.SelectedItem;

            var saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Application.StartupPath + @"\Res\Gears\";
            saveFile.Filter = "Набор приспособлений (*.scggears) | *.scggears";

            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    ToolSetup ts = new ToolSetup();
                    ts.Tools = new List<PartOfToolSetup>();

                    foreach (var item in s.Scheme.Gears)
                    {
                        ts.Tools.Add(new PartOfToolSetup
                        {
                            Count = item.Count,
                            Tool = item.Tool
                        });
                    }
                    string path = saveFile.FileName;
                    Stream SaveFileStream = File.Create(path);
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(SaveFileStream, ts);
                    SaveFileStream.Close();
                    Msg.Information($"Набор приспособлений сохранен в {path}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка сохранения набора приспособлений", Environment.UserName);
                Msg.Error("Ошибка сохранения набора приспособлений");
            }
        }

        private void btnLoadGears_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
                return;


            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath + @"\Res\Gears\";
            ofd.Filter = "Набор приспособлений (*.scggears) | *.scggears";
            ToolSetup tsLoaded;
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;
                    Stream openFileStream = File.OpenRead(path);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    tsLoaded = new ToolSetup((ToolSetup)deserializer.Deserialize(openFileStream));
                    openFileStream.Close();

                    var frm = new FrmChoise();
                    DialogResult dr = frm.ShowDialog();

                    switch (dr)
                    {
                        case DialogResult.Yes:
                            {
                                s.Scheme.Gears.AddRange(tsLoaded.Tools);
                            }
                            break;
                        case DialogResult.No:
                            {
                                s.Scheme.Gears = tsLoaded.Tools;
                            }
                            break;
                    }
                    UpdateSetupScheme();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка загрузки набора приспособлений", Environment.UserName);
                Msg.Error("Ошибка загрузки набора приспособлений");
            }
        }

        private void btnEditGearsQuantity_Click(object sender, EventArgs e)
        {
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Не выбран установ.\nСначала выберите установ.");
                return;
            }
            if (gridGearTools.SelectedRows.Count == 0)
            {
                Msg.Exclamation("Не выбран инструмент.\nВыберите инструмент из таблицы");
                return;
            }
            Set set = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            PartOfToolSetup p = set.Scheme.Gears.First(x => x.Tool.Id == Guid.Parse(gridGearTools.SelectedRows[0].Cells[0].Value.ToString()));

            var frm = new FrmGetNum();
            frm.Count = p.Count;
            if (frm.ShowDialog() == DialogResult.OK)
                p.Count = frm.Count;
            UpdateSetupScheme();
        }

        private void btnEditSetupImage_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                Msg.Exclamation("Проект не открыт!");
                return;
            }
            if (lstSets.SelectedIndex == -1)
            {
                Msg.Exclamation("Установ не выбран!");
                return;
            }
            if (lstSetupImage.SelectedIndex == -1)
            {
                Msg.Exclamation("Изображение не выбрано");
                return;
            }
            Set s = Project.Sets.First(x => x.Id == ((Set)lstSets.SelectedItem).Id);
            MyImage m = (MyImage)lstSetupImage.SelectedItem;
            var f = new FrmNoteImage();
            f.Note = m.FullPath;
            if (f.ShowDialog() == DialogResult.OK)
            {
                MyImage i = s.Scheme.Images.First(x => x.Id == m.Id);
                i.FullPath = f.Note;
                UpdateSetupScheme();
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project != null)
            {
                Project = null;
                ClearForm();
            }
            Project = new Project();
            var frm = new FrmAbout();
            frm.Text = "Информация о проекте";
            frm.CreateDate = Project.CreateDate;
            if (frm.ShowDialog() != DialogResult.OK)
            {
                Msg.Information("Создание проекта отменено");
                return;
            }
            Project.Author = frm.Author;
            Project.Detail = frm.Detail;

            try
            {
                var saveFile = new SaveFileDialog();
                saveFile.InitialDirectory = Application.StartupPath + @"\Res\MyProject\";
                saveFile.Filter = "Проект карты наладки (*.scgproject) | *.scgproject";
                saveFile.FileName = string.Format($"КН {Project.Detail} ({Project.Author})");
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    if (IOService.CanReadFile(saveFile.FileName))
                    {
                        pathToProject = saveFile.FileName;
                        Stream SaveFileStream = File.Create(pathToProject);
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(SaveFileStream, Project);
                        SaveFileStream.Close();
                        Msg.Information($"Проект сохранен в {pathToProject}");
                        ProjectIsLoaded();
                        txtInfo.Text = $"Последнее сохранение в {DateTime.Now.ToShortTimeString()}";
                    }
                    else
                        throw new FileLoadException("Ошибка создания проекта {0}", saveFile.FileName);
                }
                else
                {
                    Msg.Information("Создание проекта отменено");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка создания проекта", Environment.UserName);
                Msg.Error("Ошибка создания проекта");
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.InitialDirectory = Application.StartupPath + @"\Res\MyProject\";
                ofd.Filter = "Проект карты наладки (*.scgproject) | *.scgproject";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pathToProject = ofd.FileName;
                    Stream openFileStream = File.OpenRead(pathToProject);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    Project = (Project)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    ProjectIsLoaded();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка загрузки проекта", Environment.UserName);
                Msg.Error("Ошибка загрузки проекта");
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(pathToProject))
                {
                    Stream SaveFileStream = File.Create(pathToProject);
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(SaveFileStream, Project);
                    SaveFileStream.Close();
                    Msg.Information($"Проект сохранен в {pathToProject}");
                    ProjectIsLoaded();
                    txtInfo.Text = $"Последнее сохранение в {DateTime.Now.ToShortTimeString()}";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка сохранения проекта", Environment.UserName);
                Msg.Error("Ошибка сохранения проекта");
            }
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                Msg.Error("Сначала создайте проект");
                return;
            }

            var saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Application.StartupPath + @"\Res\MyProject\";
            saveFile.Filter = "Проект карты наладки (*.scgproject) | *.scgproject";
            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    pathToProject = saveFile.FileName;
                    Stream SaveFileStream = File.Create(pathToProject);
                    if (IOService.CanReadFile(pathToProject))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        serializer.Serialize(SaveFileStream, Project);
                        SaveFileStream.Close();
                        Msg.Information($"Проект сохранен в {pathToProject}");
                        ProjectIsLoaded();
                    }
                    else
                    {
                        throw new FileLoadException("Ошибка сохранения проекта {0}", pathToProject);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} Ошибка сохранения проекта", Environment.UserName);
                Msg.Error("Ошибка сохранения проекта");
            }
        }

        private void закрытьПроектToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Project = null;
            ClearForm();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.N)
                создатьToolStripMenuItem_Click(this, e);
            if (e.Control == true && e.KeyCode == Keys.O)
                открытьToolStripMenuItem_Click(this, e);
            if (e.Control == true && e.KeyCode == Keys.S)
                сохранитьToolStripMenuItem_Click(this, e);
            if (e.Control == true && e.KeyCode == Keys.R)
                сформироватьКНToolStripMenuItem_Click(this, e);

            if (e.Control == true && e.KeyCode == Keys.D1)
                if (Project != null)
                    tabWork.SelectedTab = tabTools;
            if (e.Control == true && e.KeyCode == Keys.D2)
                if (Project != null)
                    tabWork.SelectedTab = tabPage3;
            if (e.Control == true && e.KeyCode == Keys.D3)
                if (Project != null)
                    tabWork.SelectedTab = tabPage1;
        }

        private void опрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new AboutBox1();
            frm.ShowDialog();
        }

        private void оПроектеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                Msg.Exclamation("Проект не открыт");
                return;
            }
            var frm = new FrmAbout();
            string author = Project.Author;
            string detail = Project.Detail;
            frm.Author = author;
            frm.Detail = detail;
            frm.CreateDate = Project.CreateDate;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                author = frm.Author;
                detail = frm.Detail;
                if ((!Project.Author.Equals(author)) || !Project.Detail.Equals(detail))
                {
                    if (Msg.Question("Информация о проекте была обновлена. Сохранить изменения?") == DialogResult.OK)
                    {
                        Project.Author = author;
                        Project.Detail = detail;
                        сохранитьToolStripMenuItem_Click(this, e);
                    }
                }

            }
        }

    }
}
