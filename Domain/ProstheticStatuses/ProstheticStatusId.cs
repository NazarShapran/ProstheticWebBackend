namespace Domain.ProstheticStatuses;

public record ProstheticStatusId(Guid Value)
{
    public static ProstheticStatusId New() => new(Guid.NewGuid());
    public static ProstheticStatusId Empty() => new(Guid.Empty);
    
    public static ProstheticStatusId FromString(string guid) => new(Guid.Parse(guid));

    public override string ToString() => Value.ToString();
} 