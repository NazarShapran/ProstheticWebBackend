namespace Domain.Types;

public class ProstheticType
{
    public TypeId Id { get; }
    
    public string Title { get; private set; }

    
    public ProstheticType(TypeId id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public static ProstheticType New(TypeId id, string title)
        => new(id, title);

    public void UpdateDetails(string title)
    {
        Title = title;
    }
}