namespace DemoWebApiDB.Data.Entities;


/// <summary>
///     Base class for all entities requiring audit tracking and concurrency handling.
/// </summary>
public abstract class AuditableEntity
{

    /// <summary>
    ///     Created timestamp in UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }


    /// <summary>
    ///     Last modified timestamp in UTC.
    /// </summary>
    public DateTime? ModifiedAtUtc { get; set; }


    /// <summary>
    ///     Concurrency token for optimistic concurrency control.
    /// </summary>
    [Timestamp]
    public byte[] RowVersion { get; set; } = default!;

}
