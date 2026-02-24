namespace DemoWebApiDB.Infrastructure.Results;

/// <summary>
///     Represents the outcome of a service operation that does not return data.
///     Encapsulates success data or failure information without using exceptions for control flow.
/// </summary>
/// <remarks>
///     For example, a DELETE operation requires NoContent response if successful.  
///     But requires error details on failure - this is handled by Result(of Type T) - the other class.
/// </remarks>
public sealed class Result
{

    /// <summary>
    ///     Status of the operation.
    /// </summary>
    public ResultStatus Status { get; }


    /// <summary>
    ///     General errors (non-validation).
    /// </summary>
    public IReadOnlyList<ErrorModel> Errors { get; }


    /// <summary>
    ///     Validation-specific errors.
    /// </summary>
    public IReadOnlyList<ValidationErrorModel> ValidationErrors { get; }


    /// <summary>
    ///     Indicates whether the result represents a successful outcome.
    /// </summary>
    public bool IsSuccess =>
        Status is ResultStatus.Success or ResultStatus.Created or ResultStatus.Accepted;


    private Result(
        ResultStatus status,
        IEnumerable<ErrorModel>? errors = null,
        IEnumerable<ValidationErrorModel>? validationErrors = null)
    {
        Status = status;
        Errors = errors?.ToList() ?? new List<ErrorModel>();
        ValidationErrors = validationErrors?.ToList() ?? new List<ValidationErrorModel>();
    }


    #region Factory Methods

    public static Result Success() =>
        new(ResultStatus.Success);


    public static Result Created() =>
        new(ResultStatus.Created);


    public static Result Accepted() =>
        new(ResultStatus.Accepted);


    public static Result NotFound(string message) =>
        new(ResultStatus.NotFound,
            errors: new[] { new ErrorModel("NotFound", message) });


    public static Result Conflict(string message) =>
        new(ResultStatus.Conflict,
            errors: new[] { new ErrorModel("Conflict", message) });


    public static Result ValidationFailure(IEnumerable<ValidationErrorModel> errors) =>
        new(ResultStatus.ValidationError,
            validationErrors: errors);

    public static Result Unauthorized(string message) =>
        new(ResultStatus.Unauthorized,
            errors: new[] { new ErrorModel("Unauthorized", message) });


    public static Result Forbidden(string message) =>
        new(ResultStatus.Forbidden,
            errors: new[] { new ErrorModel("Forbidden", message) });


    public static Result Error(string message) =>
        new(ResultStatus.Error,
            errors: new[] { new ErrorModel("Error", message) });

    #endregion

}