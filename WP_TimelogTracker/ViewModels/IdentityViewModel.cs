using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WP_TimelogTracker.tlp;
using System.ComponentModel;

namespace WP_TimelogTracker.ViewModels
{
      


    
    public class IdentityViewModel
    {
        private string _user = "mrm";
        public string User {
            get {
                return _user;
            }
        }

        private string _password = "Kaffetime42";
        public string Password
        {
            get
            {
                return _password;
            }
        }

        public SecurityToken  ProjectToken{get;set;}

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
