using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using ThumbReg.Model;
using Microsoft.Phone.Shell;

namespace ThumbReg
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            DataContext = App.ViewModel;

            InitializeComponent();
            Loaded += MainPageLoaded;
            App.ViewModel.PropertyChanged += ProjectViewModelPropertyChanged;

        }


        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            try
            {
                if (!App.ViewModel.IsDataLoaded)
                {
                    if (!String.IsNullOrWhiteSpace(App.IdentityViewModel.User))
                    {
                        App.ViewModel.LoadData(false);
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
                    }
                }
            }
            catch (Exception)
            {
                NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ClearSearchResult();
        }

        private void ClearSearchResult()
        {
            Dispatcher.BeginInvoke(() => App.ViewModel.FilterTask(String.Empty));
        }

        private void TaskListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((ListBox)sender).SelectedItem as WPTask;
            if (selected != null)
            {
                NavigationService.Navigate(new Uri("/AddRegistrationPage.xaml?selectedItem=" + selected.ID, UriKind.Relative));
            }
            // Reset selected index to -1 (no selection)
            ((ListBox)sender).SelectedIndex = -1;

        }

        private void ReloadTasksClick(object sender, EventArgs e)
        {
            try
            {
                App.ViewModel.LoadData(true);
            }
            catch (Exception ex)
            {

                MessageBoxResult _return = MessageBox.Show(ex.Message + Environment.NewLine + " Check Username and Password?", "Connection error", MessageBoxButton.OKCancel);
                if (_return == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
                }
            }

        }

        private void MenuItemPinToStartClick(object sender, RoutedEventArgs e)
        {
            WPTask selected = (((MenuItem) sender).DataContext) as WPTask;
            if (selected != null)
            {
                CreateLiveTile(selected);
            }
        }

        private void CreateLiveTile(WPTask selected)
        {
            StandardTileData tiledata = new StandardTileData
            {
                Title = selected.Name, 
                BackTitle = selected.CustomerName,
                BackContent = selected.ProjectName,
                BackgroundImage = new Uri("/Images/ApplicationIcon.png",UriKind.Relative)
                
            };
            ShellTile tileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("AddRegistrationPage.xaml?selectedItem=" + selected.ID));
            if (tileToFind == null) { 
                ShellTile.Create(new Uri("/AddRegistrationPage.xaml?selectedItem=" + selected.ID, UriKind.Relative), tiledata);    
            }
            
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void AboutClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        void ProjectViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("LoadInProgress"))
            {
                Progress.IsVisible = App.ViewModel.LoadInProgress;
            }
            if (e.PropertyName.Equals("ConnectionStatus"))
            {
                MessageBox.Show(App.ViewModel.ConnectionStatus);
            }
            if (e.PropertyName.Equals("Tasks")) {
                pivot1.DataContext = App.ViewModel.Tasks;
            }

        }

        private void OpenSearchBarClick(object sender, EventArgs e)
        {
            if (txtFilter.Visibility == Visibility.Collapsed)
            {
                txtFilter.Visibility = Visibility.Visible;
                txtFilter.Text = String.Empty;
                txtFilter.Focus();
                ////hide appbar to maximize the space for keyboard                
                ApplicationBar.IsVisible = !ApplicationBar.IsVisible;

            }
            else
            {
                txtFilter.Visibility = Visibility.Collapsed;
                txtFilter.Text = String.Empty;

                ////Clear the search result
                ClearSearchResult();
            }

        }

        private void TxtFilterLostFocus(object sender, RoutedEventArgs e)
        {
            txtFilter.Visibility = Visibility.Collapsed;
            ApplicationBar.IsVisible = !this.ApplicationBar.IsVisible;

        }



        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            Dispatcher.BeginInvoke(() => App.ViewModel.FilterTask(txtFilter.Text));
            DataContext = App.ViewModel;
            if (e.Key == Key.Enter) {
                pivot1.Focus();
            }
        }

     
        private void pivot1_LoadedPivotItem(object sender, Microsoft.Phone.Controls.PivotItemEventArgs e)
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = (e.Item.Name == "AllList"); //Enable search icon
                    
        }

        private void RegistrationsIcon_Click(object sender, System.EventArgs e)
        {
        	 NavigationService.Navigate(new Uri("/RegistrationsPage.xaml", UriKind.Relative));
        }

    }
}
