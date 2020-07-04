using System.Windows.Forms;

namespace SetupCardGenerator.Service
{
    public static class Msg
    {
        public static void Information(string text)
        {
            MessageBox.Show(text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult Exclamation(string text)
        {
            return MessageBox.Show(text, "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        }
        public static DialogResult Error(string text)
        {
            return MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static DialogResult Question(string text)
        {
            return MessageBox.Show(text, "Вопрос", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        public static DialogResult Choise(string text)
        {
            return MessageBox.Show(text, "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
