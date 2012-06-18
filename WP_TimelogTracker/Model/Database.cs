using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace WP_TimelogTracker.Model
{
    public class Database : DataContext
    {
        public Database() : base("Data Source=isostore:/ThumbReg.sdf")  
        {  
        
        }
        
        public Table<WPTask> tasksTable;
        public Table<WPTask> newestTasksTable;
        public Table<WPTask> recentUsedTasksTable;
    }      
}
