using System;
using System.Net;
using System.Data.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq.Mapping;

namespace ThumbReg.Model
{
    [Table]
    public class WPTask
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int DBID { get; set; }       
        [Column]
        public int ID { get; set; }       
        [Column]
        public string FullName { get; set; }
        [Column]
        public string WBS { get; set; }
        [Column]
        public int SortOrder { get; set; }
        [Column]
        public string ProjectName { get; set; }
        [Column]
        public string CustomerName { get; set; }
        [Column]
        public string No { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public DateTime StartDate { get; set; }
        [Column]
        public Boolean RecentUsed { get; set; }
        [Column]
        public string RecentComment { get; set; }



    }
}
