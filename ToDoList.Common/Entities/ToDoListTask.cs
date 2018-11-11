using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.Entities
{
    public class ToDoListTask
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public virtual ToDoListItem ToDoListItem { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
