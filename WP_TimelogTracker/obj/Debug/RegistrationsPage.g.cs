﻿#pragma checksum "C:\Users\Martin\Documents\GitHub\ThumbReg-for-TimeLog-Project\WP_TimelogTracker\RegistrationsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "234BE9909D089FCC024976C3B8C371AE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
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


namespace ThumbReg {
    
    
    public partial class RegistrationsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.DataTemplate RegistrationGridItemTemplate;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem AddRegistrationMenuItem;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton AddRegistrationIcon;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot pivot1;
        
        internal System.Windows.Controls.TextBlock txtTotal;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP_TimelogTracker;component/RegistrationsPage.xaml", System.UriKind.Relative));
            this.RegistrationGridItemTemplate = ((System.Windows.DataTemplate)(this.FindName("RegistrationGridItemTemplate")));
            this.AddRegistrationMenuItem = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("AddRegistrationMenuItem")));
            this.AddRegistrationIcon = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("AddRegistrationIcon")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.pivot1 = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivot1")));
            this.txtTotal = ((System.Windows.Controls.TextBlock)(this.FindName("txtTotal")));
        }
    }
}

