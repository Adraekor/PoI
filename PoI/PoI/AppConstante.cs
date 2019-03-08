using System;
using System.IO;

namespace PoI
{
    public static class AppConstante
    {
        public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PoI.db");
        public static string BasicDateFormat = "dd/MM/yyyy hh:mm:ss";                            

    }
}
