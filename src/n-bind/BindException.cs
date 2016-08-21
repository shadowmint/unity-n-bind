using System;
using JetBrains.Annotations;

namespace N.Package.Bind
{
  /// Various possible error types
  public enum BindError
  {
    // An attempt was made to register a binding for an interface
    // that was already bound to some other implementation.
    DuplicateBinding,

    // Attempting to created an instance of TImpl didn't work.
    // See the innerException value for details
    CreateInstanceFailed,

    // An attempt was made to bind a null object to an interface,
    // eg. .Register<IMyThing>(foo.GetComponent<MyThing>())
    NullBinding,
  }

  /// Custom error type for this module
  public class BindException : Exception
  {
    /// The error code
    public BindError ErrorCode;

    /// The inner exception that occured if any
    public new Exception InnerException { get; set; }

    public BindException(BindError errorCode, string format, params object[] args) : base(string.Format(format, args))
    {
      ErrorCode = errorCode;
    }

    public BindException(BindError errorCode, string message) : base(message)
    {
      ErrorCode = errorCode;
    }

    public BindException(BindError errorCode)
    {
      ErrorCode = errorCode;
    }
  }
}