﻿#pragma checksum "C:\Users\Martin\Documents\GitHub\ThumbReg-for-TimeLog-Project\WP_TimelogTracker\AddRegistrationPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D1810CB68FD8AA5302B24D9E7B7535D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Coding4Fun.Phone.Controls.Toolkit;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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
    
    
    public partial class AddRegistrationPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        private Microsoft.Phone.Shell.ProgressIndicator Progress;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock CustomerName;
        
        internal System.Windows.Controls.TextBlock ProjectName;
        
        internal System.Windows.Controls.TextBlock TaskName;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.StackPanel stack;
        
        private Coding4Fun.Phone.Controls.Toolkit.TimeSpanPicker inpDuration;
        
        private System.Windows.Controls.Slider slider1;
        
        private System.Windows.Controls.TextBox txtComment;
        
        private System.Windows.Controls.TextBlock txtStatus;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP_TimelogTracker;component/AddRegistrationPage.xaml", System.UriKind.Relative));
            this.Progress = ((Microsoft.Phone.Shell.ProgressIndicator)(this.FindName("Progress")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.CustomerName = ((System.Windows.Controls.TextBlock)(this.FindName("CustomerName")));
            this.ProjectName = ((System.Windows.Controls.TextBlock)(this.FindName("ProjectName")));
            this.TaskName = ((System.Windows.Controls.TextBlock)(this.FindName("TaskName")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.stack = ((System.Windows.Controls.StackPanel)(this.FindName("stack")));
            this.inpDuration = ((Coding4Fun.Phone.Controls.Toolkit.TimeSpanPicker)(this.FindName("inpDuration")));
            this.slider1 = ((System.Windows.Controls.Slider)(this.FindName("slider1")));
            this.txtComment = ((System.Windows.Controls.TextBox)(this.FindName("txtComment")));
            this.txtStatus = ((System.Windows.Controls.TextBlock)(this.FindName("txtStatus")));
        }
    }
}

