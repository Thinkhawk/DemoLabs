namespace DemoWebApiDB.Infrastructure.Results;


/// <summary>
///     Represents a validation error for a specific property.
/// </summary>
/// <remarks>
///     Is used by ValidationProblemDetails
/// </remarks>
public sealed record class ValidationErrorModel
(

    /// <summary>
    ///     Name of the property that failed validation.
    /// </summary>
    string PropertyName,


    /// <summary>
    ///     Validation error message.
    /// </summary>
    string ErrorMessage

);
