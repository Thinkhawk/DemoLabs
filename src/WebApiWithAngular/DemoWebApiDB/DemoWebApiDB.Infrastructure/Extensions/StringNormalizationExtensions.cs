namespace DemoWebApiDB.Infrastructure.Extensions;

/// <summary>
///     Provides helper extension methods for String, 
///     for trimming, normalization, and safe text comparisons.
/// </summary>
/// <remarks>
///     Extension methods cannot be called in EF expressions.
///     For example:
///         bool exists = await _dbContext.Categories.AnyAsync( c => c.Name.ToUpper() == dto.Name.NormalizeKey() )      // Compiler Error
///     EF cannot translate custom extension methods to SQL.
///     It will not be able to Normalize the DTO value, and Compare using SQL-friendly expression.
///     Solution:
///         var normalized = dto.Name.NormalizeKey();
///         bool exists = await _db.Categories.AnyAsync(c => c.Name.ToUpper() == normalized);
/// </remarks>
public static class StringNormalizationExtensions
{

    /// <summary>
    ///     Trims a string safely. 
    ///     Returns null if input is null or whitespace.
    /// </summary>
    public static string? NullIfWhiteSpace(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return value.Trim();
    }

    /// <summary>
    ///     Trims and returns empty string if null.
    ///     Useful for required fields.
    /// </summary>
    public static string TrimOrEmpty(this string? value)
        => value?.Trim() ?? string.Empty;


    /// <summary>
    ///     Normalizes string for case-insensitive comparisons.
    ///     Trims and converts to upper invariant.
    /// </summary>
    public static string NormalizeKey(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return value.Trim().ToUpperInvariant();
    }


    /// <summary>
    ///     Determines whether two strings are equal after normalization.
    /// </summary>
    public static bool EqualsNormalized(this string? value, string? other)
        => value?.NormalizeKey() == other?.NormalizeKey();


    /// <summary>
    ///     Checks if string has actual content after trimming.
    /// </summary>
    public static bool HasValue(this string? value)
        => !string.IsNullOrWhiteSpace(value);

}