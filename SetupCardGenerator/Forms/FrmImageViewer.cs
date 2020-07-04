using System.Drawing;
using System.Windows.Forms;

namespace SetupCardGenerator.Forms
{
    public partial class FrmImageViewer : Form
    {
        public FrmImageViewer(Image i)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.None;
            pictureBox1.Image = i;
        }
    }
}
