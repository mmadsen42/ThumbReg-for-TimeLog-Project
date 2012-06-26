using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using ThumbReg.Model;

namespace ThumbReg
{
    public partial class AddRegistrationPage
    {
        private int _hours;
        private int _minutes = 30;

        public AddRegistrationPage()
        {
            InitializeComponent();
            App.RegistrationViewModel.PropertyChanged += RegistrationViewModelPropertyChanged;
            App.IdentityViewModel.PropertyChanged += IdentityViewModelPropertyChanged;

            if (App.RegistrationViewModel.TimeSinceLastRegistration < new TimeSpan(18, 0, 0))
            {
                _hours = App.RegistrationViewModel.TimeSinceLastRegistration.TimeSpan.Hours;
                _minutes = App.RegistrationViewModel.TimeSinceLastRegistration.TimeSpan.Minutes;
            }

            if (_hours > 0 || _minutes > 0)
            {
                inpDuration.Value = new TimeSpan(_hours, _minutes, 0);
            }


        }

        void IdentityViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ProjectToken")
            {
                SaveRegistationOnServer();
            }
        }

        void RegistrationViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Progress.IsVisible = false;
            txtStatus.Text = App.RegistrationViewModel.Status;
        }


        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex;
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                if (!App.ViewModel.IsDataLoaded)
                {
                    App.ViewModel.LoadData(false);
                }
                int index = int.Parse(selectedIndex);
                DataContext = App.ViewModel.Tasks.FirstOrDefault(t => t.ID == index);
            }
        }

        private void Slider1ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var totalminutes = (int)slider1.Value;
            _hours = totalminutes / 60;
            _minutes = totalminutes - (60 * _hours);
            inpDuration.Value = new TimeSpan(_hours, _minutes, 0);
        }



        private void Slider1Tap(object sender, GestureEventArgs e)
        {
            int totalmin = (int)slider1.Value;
            int _nearestquarter = totalmin - (totalmin % 15);
            slider1.Value = _nearestquarter;

            int totalminutes = (int)slider1.Value;
            _hours = totalminutes / 60;
            _minutes = totalminutes - (60 * _hours);
            inpDuration.Value = new TimeSpan(_hours, _minutes, 0);

        }


        private void ApplicationBarIconButtonClick(object sender, EventArgs e)
        {

            if (App.IdentityViewModel.ProjectToken == null)
            {
                App.IdentityViewModel.Login();
            }
            else
            {
                SaveRegistationOnServer();
            }

        }

        private void SaveRegistationOnServer()
        {
            var duration = new TimeSpan(_hours, _minutes, 0);
            Progress.IsVisible = true;
            var wpTask = ((WPTask)DataContext);
            tlp.Task _tlpTask = new tlp.Task
            {
                ID = wpTask.ID,
                No = wpTask.No,
                Name = wpTask.Name



            };
            App.RegistrationViewModel.SendRegistrationToServer(_tlpTask, duration, txtComment.Text);
        }

        private void InpDurationValueChanged(object sender, RoutedPropertyChangedEventArgs<TimeSpan> e)
        {
            TimeSpan? duration = inpDuration.Value;
            if (duration.HasValue)
            {
                _hours = duration.Value.Hours;
                _minutes = duration.Value.Minutes;
                slider1.Value = _hours * 60 + _minutes;
            }
        }
    }
}