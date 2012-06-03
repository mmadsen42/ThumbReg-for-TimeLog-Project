using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using WP_TimelogTracker;
using WP_TimelogTracker.tlp;
using WP_TimelogTracker.tlpSecurity;
using WP_TimelogTracker.Model;
using System.IO.IsolatedStorage;
using System.ComponentModel;


namespace WP_TimelogTracker.ViewModels
{
    public class ProjectViewModel
    {

        ObservableCollection<WPTask> _Tasks = new ObservableCollection<WPTask>();
        ObservableCollection<WPTask> _allTasks = new ObservableCollection<WPTask>();
        private ObservableCollection<WPTask> _RecentTasks = new ObservableCollection<WPTask>();
        private ObservableCollection<WPTask> _NewestTasks = new ObservableCollection<WPTask>();



        private IsolatedStorageSettings _isoStore = IsolatedStorageSettings.ApplicationSettings;
        //private ObservableCollection<ProjectHeader> _projects = new ObservableCollection<ProjectHeader>();
        //private ObservableCollection<CustomerHeader> _customers = new ObservableCollection<CustomerHeader>();
        private static readonly ProjectViewModel _instance = new ProjectViewModel();

        public ProjectViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isDataLoaded = false;
        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set { _isDataLoaded = value; }
        }

        private bool _LoadInProgress = false;
        public bool LoadInProgress
        {
            get { return _LoadInProgress; }
            set
            {
                _LoadInProgress = value;
                OnPropertyChanged("LoadInProgress");
            }
        }

        private string _ConnectionStatus = String.Empty;
        public String ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set
            {
                _ConnectionStatus = value;
                OnPropertyChanged("ConnectionStatus");
            }
        }

        public void LoadData(bool forceSync)
        {
            SelectTaskFromDatabase();

            try
            {
                if (forceSync)
                {
                    using (Database db = new Database())
                    {
                        if (db.DatabaseExists() == true)
                        {
                            // Create the database.
                            db.DeleteDatabase();
                        }
                    }
                }

                if (forceSync || !_Tasks.Any())
                {
                    LoadProjects();
                }

            }
            catch (SystemException _ex)
            {
                _ConnectionStatus = _ex.Message;
            }

        }

        private void SelectTaskFromDatabase()
        {
            using (Database db = new Database())
            {
                if (db.DatabaseExists() == false)
                {
                    // Create the database.
                    db.CreateDatabase();
                }
                try
                {
                    var taskInDB = from WPTask t in db.tasksTable
                                   select t;
                    _allTasks = new ObservableCollection<WPTask>(taskInDB);
                    _Tasks = new ObservableCollection<WPTask>(taskInDB);
                    //_RecentTasks = _allTasks;
                    if (_Tasks.Any())
                    {
                        IsDataLoaded = true;
                    }
                }
                catch (Exception)
                {


                }
            }
        }

        public ObservableCollection<WPTask> Tasks
        {
            get { return _Tasks; }
        }

        public ObservableCollection<WPTask> RecentTasks
        {
            get { return _RecentTasks; }
        }

        public ObservableCollection<WPTask> NewestTasks
        {
            get { return _NewestTasks; }
        }


        //public ObservableCollection<ProjectHeader> Projects {
        //    get { return _projects; }
        //}

        //public ObservableCollection<CustomerHeader> Customers {
        //    get { return _customers; }
        //}
        public void LoadProjects()
        {
            LoadInProgress = true;
            //var _add = new System.ServiceModel.EndpointAddress("https://app.timelog.dk/local/WebServices/Security/V1_0/SecurityServiceSecure.svc");
            WP_TimelogTracker.tlpSecurity.SecurityServiceClient _secClient = new WP_TimelogTracker.tlpSecurity.SecurityServiceClient();
            _secClient.GetTokenCompleted += new EventHandler<WP_TimelogTracker.tlpSecurity.GetTokenCompletedEventArgs>(_secClient_GetTokenCompleted);
            _secClient.GetTokenAsync(new tlpSecurity.GetTokenRequest(App.IdentityViewModel.User, App.IdentityViewModel.Password));
        }


        void _secClient_GetTokenCompleted(object sender, WP_TimelogTracker.tlpSecurity.GetTokenCompletedEventArgs e)
        {
            try
            {
                tlpSecurity.SecurityToken _token = e.Result.GetTokenResult.Return.FirstOrDefault();
                if (_token == null)
                {
                    throw new Exception("Unable to connect to the service: " + e.Result.GetTokenResult.Messages[0].Message);
                }
                tlp.SecurityToken _prjToken = new tlp.SecurityToken()
                {
                    Expires = _token.Expires,
                    Hash = _token.Hash,
                    Initials = _token.Initials

                };
                App.IdentityViewModel.ProjectToken = _prjToken;

                //Fetch all task from the server
                tlp.ProjectManagementServiceClient _prjClient = new tlp.ProjectManagementServiceClient();
                _prjClient.GetTasksAllocatedToEmployeeCompleted += new EventHandler<tlp.GetTasksAllocatedToEmployeeCompletedEventArgs>(_prjClient_GetTasksAllocatedToEmployeeCompleted);
                _prjClient.GetTasksAllocatedToEmployeeAsync(new tlp.GetTasksAllocatedToEmployeeRequest(App.IdentityViewModel.User, _prjToken));


            }
            catch (Exception ex)
            {
                ConnectionStatus = "Unable to connect: " + Environment.NewLine + ex.Message;
            }
        }

        void _prjClient_GetTasksAllocatedToEmployeeCompleted(object sender, WP_TimelogTracker.tlp.GetTasksAllocatedToEmployeeCompletedEventArgs e)
        {
            //get all task that is not parent task
            var _childTasks = e.Result.GetTasksAllocatedToEmployeeResult.Return.Where(t => !t.IsParent);

            //construct the all Task list
            _allTasks.Clear();
            foreach (var _T in _childTasks.OrderBy(t => t.Details.CustomerHeader.Name).ThenBy(t => t.Details.ProjectHeader.Name).ThenBy(t => t.SortOrder))
            {
                var _t = FromAPItoDomain(_T);
                _allTasks.Add(_t);
                Tasks.Add(_t);
            }

            using (Database db = new Database())
            {
                if (db.DatabaseExists() == false)
                {
                    // Create the database.
                    db.CreateDatabase();
                }
                db.tasksTable.InsertAllOnSubmit(_allTasks);
                db.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }

            _NewestTasks.Clear();
            //constuct the list of the 10 newest task
            foreach (var _T in _childTasks.OrderByDescending(t => t.StartDate).Take(10).OrderBy(t => t.Details.CustomerHeader.Name).ThenBy(t => t.Details.ProjectHeader.Name).ThenBy(t => t.SortOrder))
            {
                _NewestTasks.Add(FromAPItoDomain(_T));
            }
            using (Database db = new Database())
            {
                if (db.DatabaseExists() == false)
                {
                    // Create the database.
                    db.CreateDatabase();
                }
                db.newestTasksTable.InsertAllOnSubmit(_NewestTasks);
                db.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            IsDataLoaded = true;
            LoadInProgress = false;
            LoadLastWeeksRegistrationsFromServer();
        }

        private void LoadLastWeeksRegistrationsFromServer()
        {
            LoadInProgress = true;
            //Fetch recent registrations on the server
            tlp.ProjectManagementServiceClient _prjClient = new tlp.ProjectManagementServiceClient();
            DateTime _startdate = DateTime.Now.AddDays(-7).Date;
            DateTime _endDate = DateTime.Now.AddDays(1).Date;
            _prjClient.GetEmployeeWorkCompleted += new EventHandler<GetEmployeeWorkCompletedEventArgs>(_prjClient_GetEmployeeWorkCompleted);

            _prjClient.GetEmployeeWorkAsync(new tlp.GetEmployeeWorkRequest(App.IdentityViewModel.User, _startdate, _endDate, App.IdentityViewModel.ProjectToken));
            LoadInProgress = false;
        }

        void _prjClient_GetEmployeeWorkCompleted(object sender, GetEmployeeWorkCompletedEventArgs e)
        {
            List<int> _recentUsedTaskID = new List<int>();
            foreach (WorkUnit u in e.Result.GetEmployeeWorkResult.Return.OrderByDescending(w => w.StartDateTime))
            {
                _recentUsedTaskID.Add(u.TaskID);
            }

            _RecentTasks.Clear();
            foreach (int taskID in _recentUsedTaskID.Distinct())
            {
                var _task = _allTasks.FirstOrDefault(t => t.ID == taskID);
                if (_task != null) _RecentTasks.Add(_task);
            }
            IsDataLoaded = true;
            LoadInProgress = false;
        }


        private WPTask FromAPItoDomain(Task t)
        {
            var _t = new WPTask();
            _t.ID = t.ID;
            _t.FullName = t.FullName;
            _t.WBS = t.WBS;
            _t.Name = t.Name;
            _t.SortOrder = t.SortOrder;
            _t.ProjectName = t.Details.ProjectHeader.Name;
            _t.CustomerName = t.Details.CustomerHeader.Name;
            return _t;
        }


        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        internal void FilterTask(string filter)
        {

            if (String.IsNullOrWhiteSpace(filter))
            {
                SelectTaskFromDatabase();
                return;
            }

            _Tasks.Clear();
            var _match = from WPTask t in _allTasks
                         //where t.FullName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) > 0
                         where t.FullName.Contains(filter)
                         select t;
            
            foreach (WPTask t in _match.ToList())
            {                
                _Tasks.Add(t);
            }
        }
    }
}
