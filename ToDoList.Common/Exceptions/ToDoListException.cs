using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.Exceptions
{

    [Serializable]
    public class ToDoListException : Exception
    {
        public ToDoListException() { }
        public ToDoListException(string message) : base(message) { }
        public ToDoListException(string message, Exception inner) : base(message, inner) { }
        protected ToDoListException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
