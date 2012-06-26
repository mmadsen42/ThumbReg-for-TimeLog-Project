using System.Data.Linq;

namespace ThumbReg.Model
{
    public class Database : DataContext
    {
        public Database() : base("Data Source=isostore:/ThumbReg.sdf")  
        {  
        
        }
        
        public Table<WPTask> tasksTable;

    }      
}
