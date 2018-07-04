using System;
using System.Collections.Generic;
using System.Text;

namespace ICU4N.Impl.Locale
{
    class LocaleSyntaxException : Exception
    {
        private static readonly long s_serialVersionUID = 1L;

        private int _index = -1;

        public LocaleSyntaxException(string msg):this(msg,0)
        {     
        }

        public LocaleSyntaxException(string msg,int errorIndex):base(msg)
        {
            _index = errorIndex;
        }

        public int GetErrorIndex()
        {
            return _index;
        }
    }
}
