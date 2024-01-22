using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autok
{
    internal static class Program
    {
        public static List<Auto> autok = new List<Auto>();
        public static Adatbazis db = null;
        public static FormNyito formNyito = null;
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            db = new Adatbazis();
            autok = db.getAllAuto();
            formNyito = new FormNyito();
            Application.Run(formNyito);
        }
    }
}
