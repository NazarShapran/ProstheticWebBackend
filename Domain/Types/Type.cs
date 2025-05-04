namespace Domain.Types;

public class Type
{
    public TypeId Id { get; }
    
    public string Title { get; private set; }

    
    private Type(TypeId id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public static Type New(TypeId id, string title)
        => new(id, title);

    public void UpdateDetails(string title)
    {
        Title = title;
    }
}