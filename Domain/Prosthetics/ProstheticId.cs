namespace Domain.Prosthetics;

public record ProstheticId(Guid Value)
{
    public static ProstheticId New() => new(Guid.NewGuid());
    public static ProstheticId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}