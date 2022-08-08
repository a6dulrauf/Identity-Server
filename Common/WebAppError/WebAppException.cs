using System;
using System.Runtime.Serialization;

namespace Nami.DXP.Common
{
    [Serializable]
    public class WebAppException : Exception
    {
        public int ErrorCode { get; private set; }

        public WebAppException(int errorcode, string errormsg) : base(errormsg)
        {
            ErrorCode = errorcode;
        }

        protected WebAppException(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }
    }
}
