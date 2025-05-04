using Domain.Functionalitys;
using Domain.Materials;
using Domain.Types;
using Type = System.Type;

namespace Domain.Prosthetics;

public class Prosthetic
{
    public ProstheticId Id { get; set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public double Weight { get; private set; }
    
    public TypeId TypeId { get; }
    public Type? Type { get;  }
    
    public MaterialId MaterialId { get; }
    public Material? Material { get; }
    
    public FunctionalityId FunctionalityId { get; }
    public Functionality? Functionality { get; }
    
    private Prosthetic(ProstheticId id, string title, string description, double weight, 
        TypeId typeId, MaterialId materialId, FunctionalityId functionalityId)
    {
        Id = id;
        Title = title;
        Description = description;
        Weight = weight;
        TypeId = typeId;
        MaterialId = materialId;
        FunctionalityId = functionalityId;
    }
    
    public static Prosthetic New(ProstheticId id, string title, string description, double weight, 
        TypeId typeId, MaterialId materialId, FunctionalityId functionalityId)
        => new(id, title, description, weight, typeId, materialId, functionalityId);

    public void UpdateDetails(string title, string description, double weight)
    {
        Title = title;
        Description = description;
        Weight = weight;
    }
    
}