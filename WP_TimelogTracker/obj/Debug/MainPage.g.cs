﻿#pragma checksum "C:\Users\mrm\ThumbReg-for-TimeLog-Project\WP_TimelogTracker\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "858F7B57ADA46D3E6C4166FEE5085ED6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.530
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace WP_TimelogTracker {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.DataTemplate TaskGridItemTemplate;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot pivot1;
        
        internal Microsoft.Phone.Controls.PivotItem RecentList;
        
        internal Microsoft.Phone.Controls.PivotItem AllList;
        
        internal System.Windows.Controls.TextBox txtFilter;
        
        internal Microsoft.Phone.Controls.PivotItem NewestList;
        
        internal Microsoft.Phone.Shell.ProgressIndicator Progress;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP_TimelogTracker;component/MainPage.xaml", System.UriKind.Relative));
            this.TaskGridItemTemplate = ((System.Windows.DataTemplate)(this.FindName("TaskGridItemTemplate")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.pivot1 = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivot1")));
            this.RecentList = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("RecentList")));
            this.AllList = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("AllList")));
            this.txtFilter = ((System.Windows.Controls.TextBox)(this.FindName("txtFilter")));
            this.NewestList = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("NewestList")));
            this.Progress = ((Microsoft.Phone.Shell.ProgressIndicator)(this.FindName("Progress")));
        }
    }
}

