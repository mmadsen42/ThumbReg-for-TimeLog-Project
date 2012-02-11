using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WP_TimelogTracker
{
    public partial class AddRegistrationPage : PhoneApplicationPage
    {
        private int _hours;
        private int _minutes;

        
        public AddRegistrationPage()
        {
            InitializeComponent();
            App.RegistrationViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(RegistrationViewModel_PropertyChanged);    
        }

        void RegistrationViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            txtStatus.Text = App.RegistrationViewModel.Status;
        }

       
          // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                DataContext = App.ViewModel.Tasks.FirstOrDefault(t => t.ID == index);
            }

            //fillHours();
        }

        private void fillHours()
        {
            // List<Cities> source = new List<Cities>(); 
            //source.Add(new Cities(){Name="Madrid",Country="ES",Language="Spanish"}); 
            //source.Add(new Cities() { Name = "Las Vegas", Country = "US", Language = "English" }); 
            //source.Add(new Cities() { Name = "London", Country = "UK", Language = "English" }); 
            //source.Add(new Cities() { Name = "Mexico", Country = "MX", Language = "Spanish" }); 

            //this.listPicker.ItemsSource = source;
            
        }

        private void slider1_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            int _totalminutes = (int)slider1.Value;
            _hours = _totalminutes/60;
            _minutes = _totalminutes - (60 * _hours);
           textBox1.Text = String.Format("{0}:{1:D2}", _hours, _minutes);
        }

        private void textBox1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        private void slider1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int _totalmin = (int)slider1.Value;
            int _nearestquarter = _totalmin - (_totalmin % 15);
            slider1.Value = _nearestquarter;

             int _totalminutes = (int)slider1.Value;
            _hours = _totalminutes/60;
            _minutes = _totalminutes - (60 * _hours);
           textBox1.Text = String.Format("{0}:{1}", _hours, _minutes);

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.RegistrationViewModel.SendRegistrationToServer((WP_TimelogTracker.tlp.Task)DataContext, new TimeSpan(_hours, _minutes, 0), "From WP7 tracker");
        }

        //private void ddHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    textBox1.Text = ddHours.SelectedItem.ToString();
        //}
    }
}