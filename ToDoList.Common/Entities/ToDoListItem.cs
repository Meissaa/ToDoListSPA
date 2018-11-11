using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.Entities
{
    public class ToDoListItem
    {
        public ToDoListItem()
        {
         
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public virtual IList<ToDoListTask> Tasks { get; set; }
    }
}
