namespace Domain.Functionalitys;

public class Functionality
{
    public FunctionalityId Id { get;}
    
    public string Title { get; private set; }

    
    public Functionality(FunctionalityId id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public static Functionality New(FunctionalityId id, string title)
        => new(id, title);

    public void UpdateDetails(string title)
    {
        Title = title;
    }
}