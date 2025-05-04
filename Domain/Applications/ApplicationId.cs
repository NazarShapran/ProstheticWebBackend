namespace Domain.Applications;

public record ApplicationId(Guid Value)
{
    public static ApplicationId New() => new(Guid.NewGuid());
    public static ApplicationId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}