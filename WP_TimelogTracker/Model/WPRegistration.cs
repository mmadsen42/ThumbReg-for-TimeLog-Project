using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ThumbReg.Model
{
    public class WPRegistration
    {
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string Comment { get; set; }
        public int TaskID { get; set; }
        public string ProjectName { get; set; }
        public string TaskFullName { get; set; }
    }
}
