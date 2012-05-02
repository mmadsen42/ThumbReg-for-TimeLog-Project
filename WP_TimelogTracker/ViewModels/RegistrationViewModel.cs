using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WP_TimelogTracker.tlp;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WP_TimelogTracker
{
	public class RegistrationViewModel : INotifyPropertyChanged
	{
		public RegistrationViewModel()
		{
		   
		}

        private string _status;
		public string Status{
			set{ 				
				_status  = value;
				OnPropertyChanged("Status");	
			}
			get{ return _status; }
			
		}

        public void SendRegistrationToServer(Task task, TimeSpan duration, string comment)
        {
            DateTime _now = DateTime.Now;
            DateTime _start = _now.Subtract(duration);
            DateTime _end = _now;

            var _workunit = new WorkUnit()
            {
                Description = comment,
                Duration = duration,
                TaskID = task.ID,
                GUID = new Guid(),
                EmployeeInitials = App.IdentityViewModel.User,
                EndDateTime = _end,
                StartDateTime =  _start
            };

            var _workUnits = new ObservableCollection<WP_TimelogTracker.tlp.WorkUnit>();
            _workUnits.Add(_workunit);
            
             tlp.ProjectManagementServiceClient _prjClient = new tlp.ProjectManagementServiceClient();
             _prjClient.InsertWorkCompleted += new EventHandler<InsertWorkCompletedEventArgs>(_prjClient_InsertWorkCompleted);
            _prjClient.InsertWorkAsync(new InsertWorkRequest(_workUnits,99, App.IdentityViewModel.ProjectToken));           
		}

        void _prjClient_InsertWorkCompleted(object sender, InsertWorkCompletedEventArgs e)
        {
            Status = e.Result.InsertWorkResult.Messages[0].Message;
        }

         // Create the OnPropertyChanged method to raise the event
          protected void OnPropertyChanged(string name)
          {
              PropertyChangedEventHandler handler = PropertyChanged;
              if (handler != null)
              {
                  handler(this, new PropertyChangedEventArgs(name));
              }
          }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}