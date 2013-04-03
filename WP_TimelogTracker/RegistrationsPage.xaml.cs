using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ThumbReg.Model;
using ThumbReg.tlp;

namespace ThumbReg
{
    public partial class RegistrationsPage : PhoneApplicationPage
    {
        public RegistrationsPage()
        {
            InitializeComponent();
            Loaded += PageLoaded;
            
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.RegistrationViewModel;
            
        }
		
		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
		}

		private void AddRegistrationIcon_Click(object sender, System.EventArgs e)
		{
			 NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
		}

        private void MenuItemEditClick(object sender, RoutedEventArgs e)
        {
            var selected = ((ListBox)sender).SelectedItem as WorkUnit;
            if (selected != null)
            {
                NavigationService.Navigate(new Uri("/AddRegistrationPage.xaml?selectedItemGuid=" + selected.GUID, UriKind.Relative));
            }
            // Reset selected index to -1 (no selection)
            ((ListBox)sender).SelectedIndex = -1;
        }

        private void RegistrationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((ListBox)sender).SelectedItem as WorkUnit;
            if (selected != null)
            {
                NavigationService.Navigate(new Uri("/AddRegistrationPage.xaml?selectedItemGuid=" + selected.GUID, UriKind.Relative));
            }
            // Reset selected index to -1 (no selection)
            ((ListBox)sender).SelectedIndex = -1;
        }

        private void pivot1_LoadedPivotItem_1(object sender, PivotItemEventArgs e)
        {
            
        }

        private void pivot1_Loaded_1(object sender, RoutedEventArgs e)
        {
            Duration _totalTimeForToday = new Duration();
            foreach (var workunit in App.RegistrationViewModel.TodayRegistrations) {
                _totalTimeForToday.Add(workunit.Duration);
            }
            //txtTotal.Text = _totalTimeForToday.TimeSpan.ToString();
        }
    }
}