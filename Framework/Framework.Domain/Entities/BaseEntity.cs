namespace Framework.Domain.Entities;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedByIp { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public int Version { get; set; } = 1;
    public Status Status { get; set; }
    public string? Metadata { get; set; }
    public string? CreatedByIp { get; set; }
    public string? UpdatedByIp { get; set; }
    public bool IsLocked { get; set; } = false;
    public List<string>? Tags { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ChangeLog { get; set; }

    public void UpdateTimestamp() => UpdatedAt = DateTime.UtcNow;

    public void SoftDelete(Guid? deletedBy = null)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
    }

    public void UpdateAudit(Guid? updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

}