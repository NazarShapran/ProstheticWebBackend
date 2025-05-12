namespace Domain.AmputationLevels;

public record AmputationLevelId(Guid Value)
{
    public static AmputationLevelId New() => new(Guid.NewGuid());
    public static AmputationLevelId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}