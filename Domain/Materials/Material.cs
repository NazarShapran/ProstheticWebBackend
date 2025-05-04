namespace Domain.Materials;

public class Material
{
    public MaterialId Id { get; }
    
    public string Title { get; private set; }

    
    private Material(MaterialId id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public static Material New(MaterialId id, string title)
        => new(id, title);

    public void UpdateDetails(string title)
    {
        Title = title;
    }
}