namespace Domain.Statuses;

public record StatusId(Guid Value)
{
    public static StatusId New() => new(Guid.NewGuid());
    public static StatusId Empty() => new(Guid.Empty);
    
    public static StatusId FromString(string guid) => new(Guid.Parse(guid));

    public override string ToString() => Value.ToString();
}