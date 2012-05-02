using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WP_TimelogTracker.Model;
using Microsoft.Phone.Shell;

namespace WP_TimelogTracker
{
    public partial class MainPage : PhoneApplicationPage
    {
       
        // Constructor
        public MainPage()
        {
            DataContext = App.ViewModel;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ProjectViewModel_PropertyChanged);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            if (!App.ViewModel.IsDataLoaded)
            {


                //App.ViewModel.LoadSampleData();


            }
            //App.ViewModel.Tasks.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Tasks_CollectionChanged);

        }


        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WPTask _selected = ((ListBox)sender).SelectedItem as WPTask;
            if (_selected != null)
            {
                NavigationService.Navigate(new Uri("/AddRegistrationPage.xaml?selectedItem=" + _selected.ID, UriKind.Relative));
            }
            // Reset selected index to -1 (no selection)
            ((ListBox)sender).SelectedIndex = -1;

        }

        private void ReloadTasks_Click(object sender, EventArgs e)
        {
            Progress.IsVisible = true;
            App.ViewModel.LoadData();
            Progress.IsVisible = false;

        }

        private void MenuItemPinToStart_Click(object sender, RoutedEventArgs e)
        {
            WPTask _selected = ((sender as MenuItem).DataContext) as WPTask;
            if (_selected != null)
            {
                CreateLiveTile(_selected);
            }
        }

        private void CreateLiveTile(WPTask _selected)
        {
            StandardTileData _tiledata = new StandardTileData
            {
                Title = _selected.Name
            };
            //TODO: Load date when navigating back on page from tile
            ShellTile.Create(new Uri("/AddRegistrationPage.xaml?selectedItem=" + _selected.ID, UriKind.Relative), _tiledata);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        void ProjectViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) { 
            if(e.PropertyName.Equals("LoadInProgress")){
                Progress.IsVisible = App.ViewModel.LoadInProgress;
            }
        }
    }
}
