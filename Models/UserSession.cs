using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTracker.Models
{
    internal class UserSession
    {
        // Privatūs statiniai kintamieji, saugantys sesijos informaciją
        private static int _userId;
        private static string _username;

        // Viešai prieinami metodai sesijos informacijai gauti
        public static int UserId => _userId;
        public static string Username => _username;

        // Metodas sesijos pradžiai, iškviečiamas sėkmingai prisijungus
        public static void InitializeUserSession(int userId, string username)
        {
            _userId = userId;
            _username = username;
          
        }

        // Metodas sesijos išvalymui, iškviečiamas atsijungiant
        public static void ClearSession()
        {
            _userId = 0;
            _username = null;
            
        }
    }
}
