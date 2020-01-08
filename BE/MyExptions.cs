using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace BE
{ 
    [Serializable]
    public class DuplicateIdException : Exception
    {
        private string v;
        private int hostingUnitKey;

        public DuplicateIdException()
        {
        }

        public DuplicateIdException(string message) : base(message)
        {
        }

        public DuplicateIdException(string v, int hostingUnitKey)
        {
            this.v = v;
            this.hostingUnitKey = hostingUnitKey;
        }

        public DuplicateIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class MissingIdException : Exception
    {
        private string v;
        private int hostingUnitKey;

        public MissingIdException()
        {
        }

        public MissingIdException(string message) : base(message)
        {
        }

        public MissingIdException(string v, int hostingUnitKey)
        {
            this.v = v;
            this.hostingUnitKey = hostingUnitKey;
        }

        public MissingIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class NoDaysException : Exception
    {
        public NoDaysException()
        {
        }

        public NoDaysException(string message) : base(message)
        {
        }

        public NoDaysException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDaysException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
