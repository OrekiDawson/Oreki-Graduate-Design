using System;

namespace OrekiGraduationDesign
{
    internal class Assets
    {
        public static readonly string ConStr = "Server=115.159.157.220;Database=OrekiGraduateDesign;User Id=root;Password=6434;";

        public static string StuffId = string.Empty;

        public static string MachineId = String.Empty;

        public static FrontEnd FrontEnd = new FrontEnd();

        public static string Temp = "";

        public static int OkCancel = 0;

        public static Inputbox Inputbox = new Inputbox();

        public static void ShowInput(string title, string hint)
        {
            var inputbox = new Inputbox
            {
                Text = title,
                label1 = {Text = hint}
            };
            inputbox.ShowDialog();
            //return inputbox.textBox1.Text;
        }
    }
}
