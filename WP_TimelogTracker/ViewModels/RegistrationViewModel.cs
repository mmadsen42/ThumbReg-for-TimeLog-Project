using System;
using System.Windows;
using ThumbReg.tlp;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using ThumbReg.Model;

namespace ThumbReg.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly IsolatedStorageSettings _isoStore = IsolatedStorageSettings.ApplicationSettings;

        private DateTime TimeOfLastRegistration
        {
            get
            {
                return _isoStore.Contains("_TimeOfLastRegistration") ? (DateTime)_isoStore["_TimeOfLastRegistration"] : DateTime.Now;
            }
            set
            {
                _isoStore["_TimeOfLastRegistration"] = value;
            }
        }

        public ObservableCollection<WorkUnit> TodayRegistrations = new ObservableCollection<WorkUnit>();
        public ObservableCollection<WorkUnit> YesterdayRegistrations = new ObservableCollection<WorkUnit>();
        public WorkUnit w = new WorkUnit();
      

        public Duration TimeSinceLastRegistration
        {
            get
            {
                return DateTime.Now.Subtract(TimeOfLastRegistration).Duration();
                //w.Details.ProjectHeader.Name
            }
        }

        private string _status;

        public string Status
        {
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
            get
            {
                return _status;
            }

        }

        public void SendRegistrationToServer(Task task, TimeSpan duration, string comment)
        {
            DateTime now = DateTime.Now;
            DateTime start = now.Subtract(duration);
            DateTime end = now;

            var workunit = new WorkUnit
                               {
                Description = comment,
                Duration = duration,
                TaskID = task.ID,
                GUID = new Guid(),
                EmployeeInitials = App.IdentityViewModel.User,
                EndDateTime = end,
                StartDateTime = start
            };

            var workUnits = new ObservableCollection<WorkUnit> {workunit};

            var prjClient = new ProjectManagementServiceClient();
            prjClient.InsertWorkCompleted += PrjClientInsertWorkCompleted;
            prjClient.InsertWorkAsync(new InsertWorkRequest(workUnits, 99, App.IdentityViewModel.ProjectToken));
        }

        void PrjClientInsertWorkCompleted(object sender, InsertWorkCompletedEventArgs e)
        {
            if (e.Result.InsertWorkResult.ErrorCode == 0)
            {
                if (e.Result.InsertWorkResult.Return[0].Status == ExecutionStatus.Success)
                {
                    Status = "Done!";
                    TimeOfLastRegistration = DateTime.Now;
                }
                else
                {
                    Status = e.Result.InsertWorkResult.Return[0].Message;
                }

            }
            else
            {
                Status = e.Result.InsertWorkResult.Messages[0].Message;
            }

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