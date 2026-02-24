namespace DemoWebApiDB.Infrastructure.Results;


/// <summary>
///     Represents the outcome of a service operation.
///     Encapsulates success data or failure information without using exceptions for control flow.
/// </summary>
/// <typeparam name="T">Type of data returned when successful.</typeparam>
public sealed class Result<T>
{

    /// <summary>
    ///     Status of the operation.
    /// </summary>
    public ResultStatus Status { get; }


    /// <summary>
    ///     Data returned when operation succeeds.
    /// </summary>
    public T? Data { get; }


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
        T? data = default,
        IEnumerable<ErrorModel>? errors = null,
        IEnumerable<ValidationErrorModel>? validationErrors = null)
    {
        Status = status;
        Data = data;
        Errors = errors?.ToList() ?? new List<ErrorModel>();
        ValidationErrors = validationErrors?.ToList() ?? new List<ValidationErrorModel>();
    }


    #region Factory Methods

    public static Result<T> Success(T data) =>
        new(ResultStatus.Success, data);


    public static Result<T> Created(T data) =>
        new(ResultStatus.Created, data);


    public static Result<T> Accepted(T data) =>
        new(ResultStatus.Accepted, data);


    public static Result<T> NotFound(string message) =>
        new(ResultStatus.NotFound,
            errors: new[] { new ErrorModel("NotFound", message) });


    public static Result<T> Conflict(string message) =>
        new(ResultStatus.Conflict,
            errors: new[] { new ErrorModel("Conflict", message) });


    public static Result<T> ValidationFailure(IEnumerable<ValidationErrorModel> validationErrors) =>
        new(ResultStatus.ValidationError,
            validationErrors: validationErrors);


    public static Result<T> Unauthorized(string message) =>
        new(ResultStatus.Unauthorized,
            errors: new[] { new ErrorModel("Unauthorized", message) });


    public static Result<T> Forbidden(string message) =>
        new(ResultStatus.Forbidden,
            errors: new[] { new ErrorModel("Forbidden", message) });


    public static Result<T> Error(string message) =>
        new(ResultStatus.Error,
            errors: new[] { new ErrorModel("Error", message) });

    #endregion

}