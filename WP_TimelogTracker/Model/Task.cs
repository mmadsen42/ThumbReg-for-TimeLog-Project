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

namespace WP_TimelogTracker.Model
{
    public class WPTask
    {
        public WPTask(int id, string fullName, string wbs, string  name, int sortOrder, string projectName, string customerName) {
            ID = id;
            FullName = fullName;
            Name = name;
            WBS = wbs;
            SortOrder = sortOrder;
            ProjectName = projectName;
            CustomerName = customerName;
        }

        public string FullName { get; private set; }
        public string WBS { get; private set; }
        public int SortOrder { get; private set; }
        public string ProjectName { get; private set; }
        public string CustomerName { get; private set; }
        public string No { get; private set; }
        public string Name { get; private set; }
        public int ID { get; private set; }

    }
}
