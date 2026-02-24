namespace DemoWebApiDB.Infrastructure.Helpers;


/// <summary>
///     Provides helper methods to convert SQL RowVersion (byte[]) 
///     to and from Base64 string representation for the APIs.
/// </summary>
/// <remarks>
///     What this helper does:
///     - Convert byte[] → Base64 string
///     - Convert Base64 → byte[]
///     - Handle NULLs
///     - Throw meaningful error message for invalid input
///     - Be reusable everywhere
/// </remarks>
public static class RowVersionHelper
{

    /// <summary>
    ///     Converts a RowVersion byte array to Base64 string.
    ///     Used when sending data to clients.
    /// </summary>
    /// <param name="rowVersion">Database rowversion value.</param>
    /// <returns>Base64 encoded string.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string ToBase64(byte[] rowVersion)
    {
        if (rowVersion is null || rowVersion.Length == 0)
        {
            throw new ArgumentNullException(
                nameof(rowVersion),
                "RowVersion cannot be null or empty.");
        }

        return Convert.ToBase64String(rowVersion);
    }

    /// <summary>
    ///     Converts Base64 string back to RowVersion byte array.
    ///     Used when receiving data from clients during update/delete operations.
    /// </summary>
    /// <param name="base64RowVersion">Base64 encoded rowversion.</param>
    /// <returns>Byte array for EF concurrency check.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static byte[] FromBase64(string base64RowVersion)
    {
        if (string.IsNullOrWhiteSpace(base64RowVersion))
        {
            throw new ArgumentException(
                "RowVersion cannot be null or empty.",
                nameof(base64RowVersion));
        }

        try
        {
            return Convert.FromBase64String(base64RowVersion.Trim());
        }
        catch (FormatException exp)
        {
            throw new ArgumentException(
                "Invalid RowVersion format. Expected Base64 string.",
                nameof(base64RowVersion),
                exp);
        }
    }

}
