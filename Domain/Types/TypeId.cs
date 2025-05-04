namespace Domain.Types;

public record TypeId(Guid Value)
{
    public static TypeId New() => new(Guid.NewGuid());
    public static TypeId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}