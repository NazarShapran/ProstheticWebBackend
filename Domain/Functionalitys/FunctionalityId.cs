namespace Domain.Functionalitys;

public record FunctionalityId(Guid Value)
{
    public static FunctionalityId New() => new(Guid.NewGuid());
    public static FunctionalityId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}