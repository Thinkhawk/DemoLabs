using DemoWebApiDB.Infrastructure.Results;


namespace DemoWebApiDB.Controllers;


/// <summary>
///     Provides a common base controller for all API controllers.
/// 
///     Centralizes translation of the application-layer Result pattern
///     into proper HTTP responses compliant with RFC 7807 (Problem Details).
/// </summary>
/// <remarks>
///     Ensure that:
///     - Reduced duplication across controllers
///     - Clean separation between application services and HTTP layer
///     - Result Pattern aligned consistent API responses
///         - Structured and predictable RFC7807 compliant error handling
/// </remarks>
[ApiController]
public abstract class BaseApiController : ControllerBase
{

    #region Result TO IActionResult Translator Helper methods

    /// <summary>
    ///     Converts a non-generic Result into an appropriate IActionResult.
    ///     Used for operations that do not return a payload (e.g., Update, Delete).
    /// </summary>
    /// <param name="result">Service layer result.</param>
    protected IActionResult HandleResult(Result result)
    {
        return result.Status switch
        {
            ResultStatus.Success => NoContent(),

            ResultStatus.Created => StatusCode(StatusCodes.Status201Created),

            ResultStatus.Accepted => Accepted(),

            ResultStatus.NotFound =>
                NotFound(CreateProblemDetails(result)),

            ResultStatus.Conflict =>
                Conflict(CreateProblemDetails(result)),

            ResultStatus.ValidationError =>
                BadRequest(CreateValidationProblemDetails(result)),

            ResultStatus.Unauthorized =>
                Unauthorized(CreateProblemDetails(result)),

            ResultStatus.Forbidden =>
                StatusCode(StatusCodes.Status403Forbidden,
                    CreateProblemDetails(result)),

            _ => 
                StatusCode(StatusCodes.Status500InternalServerError,
                    CreateProblemDetails(result,
                        title: "Unexpected error",
                        detail: "An unexpected server error occurred."))
        };
    }


    /// <summary>
    ///     Converts a generic Result(of Type T) into an IActionResult.
    ///     Used for operations returning data payloads.
    /// </summary>
    /// <typeparam name="T">Payload type.</typeparam>
    /// <param name="result">Service result.</param>
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Status switch
            {
                ResultStatus.Success => 
                    Ok(result.Data),

                ResultStatus.Created =>
                    Created(string.Empty, result.Data),

                ResultStatus.Accepted =>
                    Accepted(result.Data),

                _ =>
                    Ok(result.Data)
            };
        }

        return result.Status switch
        {
            ResultStatus.NotFound =>
                NotFound(CreateProblemDetails(result)),

            ResultStatus.Conflict =>
                Conflict(CreateProblemDetails(result)),

            ResultStatus.ValidationError =>
                BadRequest(CreateValidationProblemDetails(result)),

            ResultStatus.Unauthorized =>
                Unauthorized(CreateProblemDetails(result)),

            ResultStatus.Forbidden =>
                StatusCode(StatusCodes.Status403Forbidden,
                    CreateProblemDetails(result)),

            _ => 
                StatusCode(StatusCodes.Status500InternalServerError,
                    CreateProblemDetails(result,
                        title: "Unexpected error",
                        detail: "An unexpected server error occurred."))
        };
    }

    #endregion



    #region ProblemDetails Builder Helper methods

    /// <summary>
    ///     Creates RFC 7807 ProblemDetails from a non-generic Result.
    /// </summary>
    protected ProblemDetails CreateProblemDetails(
        Result result,
        string? title = null,
        string? detail = null)
    {
        var error = result.Errors.FirstOrDefault();

        return new ProblemDetails
        {
            Title = title ?? error?.Code ?? "Error",
            Detail = detail ?? error?.Message,
            Status = MapStatusCode(result.Status),
            Instance = HttpContext.Request.Path
        };
    }


    /// <summary>
    ///     Creates RFC 7807 ProblemDetails from a generic Result.
    /// </summary>
    protected ProblemDetails CreateProblemDetails<T>(
        Result<T> result,
        string? title = null,
        string? detail = null)
    {
        var error = result.Errors.FirstOrDefault();

        return new ProblemDetails
        {
            Title = title ?? error?.Code ?? "Error",
            Detail = detail ?? error?.Message,
            Status = MapStatusCode(result.Status),
            Instance = HttpContext.Request.Path
        };
    }


    /// <summary>
    ///     Creates ValidationProblemDetails from a non-generic Result.
    ///     Used when validation failures occur.
    /// </summary>
    protected ValidationProblemDetails CreateValidationProblemDetails(Result result)
    {
        var errors = result.ValidationErrors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key, 
                g => g.Select(e => e.ErrorMessage).ToArray());

        return new ValidationProblemDetails(errors)
        {
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Instance = HttpContext.Request.Path
        };
    }


    /// <summary>
    ///     Creates ValidationProblemDetails from a generic Result.
    /// </summary>
    protected ValidationProblemDetails CreateValidationProblemDetails<T>(Result<T> result)
    {
        var errors = result.ValidationErrors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        return new ValidationProblemDetails(errors)
        {
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Instance = HttpContext.Request.Path
        };
    }

    #endregion



    #region Status Mapping Helper methods

    /// <summary>
    ///     Maps ResultStatus to HTTP status code.
    ///     Used for ProblemDetails responses.
    /// </summary>
    private static int MapStatusCode(ResultStatus status) =>
        status switch
        {
            ResultStatus.NotFound           => StatusCodes.Status404NotFound,
            ResultStatus.Conflict           => StatusCodes.Status409Conflict,
            ResultStatus.ValidationError    => StatusCodes.Status400BadRequest,
            ResultStatus.Unauthorized       => StatusCodes.Status401Unauthorized,
            ResultStatus.Forbidden          => StatusCodes.Status403Forbidden,
            _                               => StatusCodes.Status500InternalServerError
        };

    #endregion

}
