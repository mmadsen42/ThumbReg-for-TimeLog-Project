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
using System.Collections.Generic;
using WP_TimelogTracker.tlp;

namespace WP_TimelogTracker.ViewModels
{
    public class ProjectHeaderComparer:  IEqualityComparer<ProjectHeader>
    {

        public bool Equals(ProjectHeader x, ProjectHeader y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(ProjectHeader obj)
        {
            return obj.ID;
        }
    }
}
