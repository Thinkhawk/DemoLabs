namespace DemoWebApiDB.Infrastructure.Results;


/// <summary>
///     Represents a structured error returned from the service layer.
/// </summary>
public sealed record class ErrorModel
(

    /// <summary>
    ///     Machine-readable error code.
    /// </summary>
    string Code,


    /// <summary>
    ///     Human-readable error message.
    /// </summary>
    string Message

);
