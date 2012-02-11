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


namespace WP_TimelogTracker.ViewModels
{
    public class ProjectViewModel
    {       

        private ObservableCollection<WPTask> _Tasks = new ObservableCollection<WPTask>();
        private ObservableCollection<WPTask> _RecentTasks = new ObservableCollection<WPTask>();
        private ObservableCollection<WPTask> _NewestTasks = new ObservableCollection<WPTask>();
        
        //private ObservableCollection<ProjectHeader> _projects = new ObservableCollection<ProjectHeader>();
        //private ObservableCollection<CustomerHeader> _customers = new ObservableCollection<CustomerHeader>();
        private static readonly ProjectViewModel _instance = new ProjectViewModel();

        public ProjectViewModel() { }
        
        private bool _isDataLoaded = false;
        public bool IsDataLoaded{
            get { return _isDataLoaded; }
            set { _isDataLoaded = value;}
        } 
        public void LoadData() {
            //LoadProjects();
            LoadSampleData();
        }

        public ObservableCollection<WPTask> Tasks {
            get { return _Tasks; }
        }

        public ObservableCollection<WPTask> RecentTasks {
            get { return _RecentTasks; }
        }

        public ObservableCollection<WPTask> NewestTasks {
            get { return _NewestTasks; }
        }

        
        //public ObservableCollection<ProjectHeader> Projects {
        //    get { return _projects; }
        //}
		
        //public ObservableCollection<CustomerHeader> Customers {
        //    get { return _customers; }
        //}
        public void LoadProjects(){
         
			WP_TimelogTracker.tlpSecurity.SecurityServiceClient _secClient = new WP_TimelogTracker.tlpSecurity.SecurityServiceClient();
            _secClient.GetTokenCompleted += new EventHandler<WP_TimelogTracker.tlpSecurity.GetTokenCompletedEventArgs>(_secClient_GetTokenCompleted);
            _secClient.GetTokenAsync(new tlpSecurity.GetTokenRequest("mrm", "Kaffetime42"));
        }

         void _secClient_GetTokenCompleted(object sender, WP_TimelogTracker.tlpSecurity.GetTokenCompletedEventArgs e)
        {
            tlpSecurity.SecurityToken _token =  e.Result.GetTokenResult.Return.FirstOrDefault();
            tlp.SecurityToken _prjToken = new tlp.SecurityToken
            {
                Expires = _token.Expires,
                Hash = _token.Hash,
                Initials = _token.Initials
               
            };

            App.IdentityViewModel.ProjectToken = _prjToken;
            //var bind =  new System.ServiceModel.BasicHttpBinding();
            //bind.MaxReceivedMessageSize = 10000000;
            //var add = new System.ServiceModel.EndpointAddress("https://app.timelog.dk/local/WebServices/ProjectManagement/V1_1/ProjectManagementService.svc");
            tlp.ProjectManagementServiceClient _prjClient = new tlp.ProjectManagementServiceClient();
            _prjClient.GetTasksAllocatedToEmployeeCompleted += new EventHandler<tlp.GetTasksAllocatedToEmployeeCompletedEventArgs>(_prjClient_GetTasksAllocatedToEmployeeCompleted);
            _prjClient.GetTasksAllocatedToEmployeeAsync(new tlp.GetTasksAllocatedToEmployeeRequest(App.IdentityViewModel.User, _prjToken));
        }

        void _prjClient_GetTasksAllocatedToEmployeeCompleted(object sender, WP_TimelogTracker.tlp.GetTasksAllocatedToEmployeeCompletedEventArgs e)
        {
            //get all task that is not parent task
            var _childTasks = e.Result.GetTasksAllocatedToEmployeeResult.Return.Where(t => !t.IsParent);

            //construct the all Task list
            _Tasks.Clear();
            foreach(var _T in  _childTasks.OrderBy(t=>t.Details.CustomerHeader.Name).ThenBy(t=>t.Details.ProjectHeader.Name).ThenBy(t=>t.SortOrder)){
               _Tasks.Add(FromAPItoDomain(_T));
           }
           
            _RecentTasks.Clear();

            
            _NewestTasks.Clear();
            //constuct the list of the 10 newest task
            foreach(var _T in  _childTasks.OrderByDescending(t=>t.StartDate).Take(10).OrderBy(t=>t.Details.CustomerHeader.Name).ThenBy(t=>t.Details.ProjectHeader.Name).ThenBy(t=>t.SortOrder)){
               _NewestTasks.Add(FromAPItoDomain(_T));
           }
 

            IsDataLoaded = true;
            //foreach (var _prj in _Task.GroupBy(t => t.Details.ProjectHeader).Select(p => p.Key))
            //{
            //    _projects.Add( _prj);    
            //}
			
            //foreach (var _cust in _Task.GroupBy(t => t.Details.CustomerHeader).Select(p => p.Key))
            //{
            //    _customers.Add( _cust);    
            //}
            
        }

       private WPTask FromAPItoDomain(Task t){
            return new WPTask(t.ID, t.FullName, t.WBS, t.Name, t.SortOrder, t.Details.ProjectHeader.Name, t.Details.CustomerHeader.Name); 
       }


       internal void LoadSampleData()
       {
           _Tasks.Add(new WPTask(1,"TimeLogTest", "1", "Test",1, "Log", "Time"));
           _Tasks.Add(new WPTask(2,"Project 1 - task 1", "2", "Task A",2, "Proj A", "cust A"));
           _Tasks.Add(new WPTask(33,"TimeLogTest", "1.2", "Task B",3, "Proj A","cust A"));
           _RecentTasks.Clear();            
           _NewestTasks.Clear();
           IsDataLoaded = true;
       }
    }

     
}
