namespace DemoWebApiDB.Infrastructure.Results;


/// <summary>
///     Represents the outcome status of a service operation.
///     This is later translated to an appropriate HTTP response in the API layer.
/// </summary>
public enum ResultStatus
{

    /// <summary>
    ///     Operation completed successfully.
    /// </summary>
    Success,

    /// <summary>
    ///     Resource successfully created.
    /// </summary>
    Created,


    /// <summary>
    ///     Operation accepted (typically for PUT updates).
    /// </summary>
    Accepted,


    /// <summary>
    ///     Requested resource was not found.
    /// </summary>
    NotFound,


    /// <summary>
    ///     Validation errors occurred.
    /// </summary>
    ValidationError,


    /// <summary>
    ///     Conflict occurred (e.g., duplicate or concurrency issue).
    /// </summary>
    Conflict,


    /// <summary>
    ///     Unauthorized access.
    /// </summary>
    Unauthorized,


    /// <summary>
    ///     Authenticated but forbidden.
    /// </summary>
    Forbidden,


    /// <summary>
    ///     Unexpected system error.
    /// </summary>
    Error

}
