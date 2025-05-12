namespace Domain.AmputationLevels;

public class AmputationLevel
{
    public AmputationLevelId Id { get; }
    
    public string Title { get; private set; }

    
    public AmputationLevel(AmputationLevelId id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public static AmputationLevel New(AmputationLevelId id, string title)
        => new(id, title);

    public void UpdateDetails(string title)
    {
        Title = title;
    }
}