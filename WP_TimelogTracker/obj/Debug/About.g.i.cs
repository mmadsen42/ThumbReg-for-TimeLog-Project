﻿#pragma checksum "C:\Users\Martin\Documents\GitHub\ThumbReg-for-TimeLog-Project\WP_TimelogTracker\About.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0D53F3226FB84C517FA92A346A6A78F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ThumbReg {
    
    
    public partial class About : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.TextBlock txtAbout;
        
        internal System.Windows.Controls.TextBlock txtDemo;
        
        private System.Windows.Controls.TextBlock txtVersion;
        
        internal System.Windows.Controls.TextBlock txtContact;
        
        internal System.Windows.Controls.Button btnSendMail;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP_TimelogTracker;component/About.xaml", System.UriKind.Relative));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.txtAbout = ((System.Windows.Controls.TextBlock)(this.FindName("txtAbout")));
            this.txtDemo = ((System.Windows.Controls.TextBlock)(this.FindName("txtDemo")));
            this.txtVersion = ((System.Windows.Controls.TextBlock)(this.FindName("txtVersion")));
            this.txtContact = ((System.Windows.Controls.TextBlock)(this.FindName("txtContact")));
            this.btnSendMail = ((System.Windows.Controls.Button)(this.FindName("btnSendMail")));
        }
    }
}

