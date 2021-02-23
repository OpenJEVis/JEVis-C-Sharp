using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public class JEVisException : Exception
    {

        private static bool _debug = true;
        private int _code = 0;
        private String _message = "Missing Exception message";
        private static readonly ILog logger = LogManager.GetLogger(typeof(JEVisException));

        /**
         *
         * @param message
         * @param faultCode
         */
        public JEVisException(String message, int faultCode) : base(message)
        {

            _message = message;
            _code = faultCode;
            //        debug(null);
        }

        public JEVisException(String message, int faultCode, Exception cause) : base(message, cause)
        {
            _message = message;
            _code = faultCode;
            debug(cause);
        }

        public int getCode()
        {
            return _code;
        }

        //TODo, add translatable interface?
        private void debug(Exception cause)
        {
            logger.Error(string.Format("[{0}] {1}", _code, _message));
        }
    }
}
