using SetupCardGenerator.DPO;
using SetupCardGenerator.Model;
using SetupCardGenerator.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmReport : Form
    {
        private Project _project;
        public FrmReport(Project p)
        {
            _project = p;
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {

            List<ToolDPO> tools = GetToolDPO();
            List<SchemeDPO> scheme = GetSchemeDPO();
            List<GearsDPO> gears = GetGearsDPO();
            List<NotesDPO> notes = GetNotesDPO();

            schemeDPOBindingSource.DataSource = scheme;
            toolDPOBindingSource.DataSource = tools;
            gearsDPOBindingSource.DataSource = gears;
            notesDPOBindingSource.DataSource = notes;

            projectBindingSource.DataSource = _project;
            setBindingSource.DataSource = _project.Sets;

            this.reportViewer1.RefreshReport();
        }

        private List<ToolDPO> GetToolDPO()
        {
            List<ToolDPO> list = new List<ToolDPO>();

            foreach (Set s in _project.Sets)
            {
                foreach (ToolSetup ts in s.ToolSetups)
                {
                    foreach (PartOfToolSetup tool in ts.Tools)
                    {
                        ToolDPO t = new ToolDPO
                        {
                            SetId = s.Id,
                            Machine = s.Machine,
                            ToolSetupId = ts.Id,
                            Image = Reporting.ImageToByteArray(ts.Image),
                            T_Num = ts.Num_T.ToString(),
                            D_Num = ts.Num_D.ToString(),
                            Outhand = ts.OutHand.ToString(),
                            ToolSetupName = ts.Name,
                            Code = tool.Tool.Code,
                            Name = tool.Tool.Name,
                            SetName = s.SetName,
                            Type = tool.Tool.Type,
                            Quantity = tool.Count,
                            PaddingLeft = Reporting.GetPaddingLeft(ts.Image, 113, 151),
                            PaddingTop = Reporting.GetPaddingTop(ts.Image, 113, 151),
                            SchemeId = s.Scheme.Id
                        };
                        list.Add(t);
                    }
                }
            }

            return list;
        }
        private List<SchemeDPO> GetSchemeDPO()
        {
            List<SchemeDPO> list = new List<SchemeDPO>();

            foreach (Set s in _project.Sets)
            {
                foreach (var r in s.Scheme.Images)
                {
                    list.Add(new SchemeDPO
                    {
                        Image = Reporting.ImageToByteArray(r.Image),
                        ImageName = r.Name,
                        Note = s.Scheme.Note,
                        SchemeId = s.Scheme.Id,
                        SetId = s.Id,
                        PaddingLeft = Reporting.GetPaddingLeft(r.Image, 642, 355),
                        PaddingTop = Reporting.GetPaddingTop(r.Image, 642, 355),
                        SetMachine = s.Machine,
                        SetName = s.SetName,
                        ImageNote = r.FullPath
                    });
                }
            }
            return list;
        }

        private List<GearsDPO> GetGearsDPO()
        {
            List<GearsDPO> list = new List<GearsDPO>();
            foreach (Set s in _project.Sets)
            {
                foreach (var i in s.Scheme.Gears)
                {
                    list.Add(new GearsDPO
                    {
                        SetId = s.Id,
                        SetName = s.SetName,
                        SetMachine = s.Machine,
                        Quantity = i.Count,
                        ToolCode = i.Tool.Code,
                        ToolName = i.Tool.Name,
                        ToolType = i.Tool.Type
                    });

                }
            }
            return list;
        }

        private List<NotesDPO> GetNotesDPO()
        {
            List<NotesDPO> list = new List<NotesDPO>();


            foreach (Set s in _project.Sets)
            {
                double d = 0;
                foreach (var k in s.Notes)
                {
                    d += k.Time;
                }
                foreach (var n in s.Notes)
                {
                    NotesDPO note = new NotesDPO
                    {
                        SetId = s.Id,
                        SetName = s.SetName,
                        SetMachine = s.Machine,
                        Id = n.Id,
                        Coeff = n.Coeff,
                        Description = n.Description,
                        Num = n.Num,
                        Time = n.Time,
                        FullTime = Math.Round(d, 3)

                    };
                    note.ToolNum = n.Tool == null ? "-" : string.Format($"T{n.Tool.Num_T} D{n.Tool.Num_D}");
                    list.Add(note);
                }
            }

            return list;
        }
    }
}
