using ClosedXML.Excel;
using NLog;
using SetupCardGenerator.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SetupCardGenerator
{
    public partial class FrmLoading : Form
    {
        List<Tool> tools;
        List<Machine> machines;
        string path;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void UpdateProgressBar()
        {
            bgDownload.RunWorkerAsync();
        }


        public FrmLoading()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            path = @"Res\Data\Data.xlsx";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmLoading_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path))
            {
                logger.Error("{0} Ошибка запуска программы. База данных не найдена", Environment.UserName);
                Msg.Error($"Файл с базой данных отсутствует.\nПроверьте наличие Excel-файла \"Data.xlsx\" в расположении {string.Format(Application.StartupPath + "\\Res\\Data")} и повторите попытку");
                Application.Exit();
            }           
            while(!IOService.CanReadFile(path))
            {
                logger.Error("{0} Ошибка запуска программы. Файл базы данных недоступен для чтения", Environment.UserName);
                if (Msg.Question("Файл с базой данных стал недоступен для чтения. Попробовать снова?") == DialogResult.Cancel)
                {
                    Application.Exit();
                    break;
                }
            }

            UpdateProgressBar();
        }

        private void bgDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            machines = new List<Machine>();
            tools = new List<Tool>();

            try
            {
                using (XLWorkbook workbook = new XLWorkbook(path))
                {
                    IXLWorksheet wsTools = workbook.Worksheet(1);
                    IXLWorksheet wsMashines = workbook.Worksheet(2);

                    var listTools = wsTools.Rows().ToList();
                    var listMachines = wsMashines.Rows().ToList();

                    int countOfRows = listMachines.Count + listTools.Count;
                    double percent = 0;
                    int currentRow = 0;
                    double tmp = 0;

                    for (int i = 1; i < listTools.Count; i++)
                    {
                        var row = listTools[i];
                        tools.Add(item:
                            new Tool(
                                row.Cell(1).Value.ToString(),
                                row.Cell(3).Value.ToString(),
                                row.Cell(2).Value.ToString()
                                )
                            );
                        currentRow++;

                        tmp = ((double)currentRow / (double)countOfRows) * 100;

                        if (tmp >= percent)
                        {
                            percent += 1;
                            bgDownload.ReportProgress(Convert.ToInt32(percent));
                        }
                    }
                    for (int j = 1; j < listMachines.Count; j++)
                    {
                        var row = listMachines[j];
                        machines.Add(item:
                            new Machine(
                                row.Cell(1).Value.ToString()
                            ));
                        currentRow++;

                        tmp = ((double)currentRow / (double)countOfRows) * 100;

                        if (tmp >= percent)
                        {
                            percent += 1;
                            bgDownload.ReportProgress(Convert.ToInt32(percent));
                        }
                    }

                }
            }
            catch (FileNotFoundException ex)
            {
                logger.Error(ex, "{0} Ошибка запуска программы. Ошибка во время загрузки данных", Environment.UserName);
                Msg.Error(string.Format($"{ex.FileName} не найден или открыт в дугой программе.\nПриложение будет закрыто"));
                Application.Exit();
            }
        }

        private void bgDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            machines.OrderBy(x => x.Name);
            tools.OrderBy(x => x.Name);
            progressBar1.Value = 100;
            var frm = new FrmMainWindow(tools.OrderBy(x => x.Name).ToList(), machines.OrderBy(x => x.Name).ToList());
            Hide();
            frm.ShowDialog();
            Close();
        }
    }
}
