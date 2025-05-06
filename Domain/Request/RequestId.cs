namespace Domain.Request;

public record RequestId(Guid Value)
{
    public static RequestId New() => new(Guid.NewGuid());
    public static RequestId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}