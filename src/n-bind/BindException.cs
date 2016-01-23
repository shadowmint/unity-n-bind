using System;

namespace N.Package.Bind
{
    /// Various possible error types
    public enum BindError
    {
        // An attempt was made to register a binding for an interface
        // that was already bound to some other implementation.
        DUPLICATE_BINDING,

        // Attempting to created an instance of TImpl didn't work.
        // See the innerException value for details
        CREATE_INSTANCE_FAILED
    }

    /// Custom error type for this module
    public class BindException : System.Exception
    {
        /// The error code
        public BindError errorCode;

        /// The parent exception, if any
        public Exception innerException;

        public BindException(BindError errorCode, string format, params object[] args) : base(string.Format(format, args))
        { this.errorCode = errorCode; }

        public BindException(BindError errorCode, string message) : base(message)
        { this.errorCode = errorCode; }

        public BindException(BindError errorCode)
        { this.errorCode = errorCode; }
    }
}
