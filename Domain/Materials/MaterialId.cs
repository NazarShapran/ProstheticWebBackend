namespace Domain.Materials;

public record MaterialId(Guid Value)
{
    public static MaterialId New() => new(Guid.NewGuid());
    public static MaterialId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}