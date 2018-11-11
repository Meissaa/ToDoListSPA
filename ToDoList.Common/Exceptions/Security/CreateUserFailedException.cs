using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Common.Exceptions.Security
{

    [Serializable]
    public class CreateUserFailedException : SecurityException
    {
        public CreateUserFailedException() { }
        public CreateUserFailedException(string message) : base(message) { }
        public CreateUserFailedException(string message, Exception inner) : base(message, inner) { }
        protected CreateUserFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
