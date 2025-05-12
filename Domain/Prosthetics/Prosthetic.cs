using Domain.AmputationLevels;
using Domain.Functionalities;
using Domain.Materials;
using Domain.ProstheticTypes;

namespace Domain.Prosthetics;

public class Prosthetic
{
    public ProstheticId Id { get;  }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public double Weight { get; private set; }
    
    public TypeId TypeId { get; }
    public ProstheticType? Type { get;  }
    
    public MaterialId MaterialId { get; }
    public Material? Material { get; }
    
    public FunctionalityId FunctionalityId { get; }
    public Functionality? Functionality { get; }
    
    public AmputationLevelId? AmputationLevelId { get; }
    public AmputationLevel? AmputationLevel { get;  }
    
    public Prosthetic(ProstheticId id, string title, string description, double weight, 
        TypeId typeId, MaterialId materialId, FunctionalityId functionalityId, AmputationLevelId amputationLevelId)
    {
        Id = id;
        Title = title;
        Description = description;
        Weight = weight;
        TypeId = typeId;
        MaterialId = materialId;
        FunctionalityId = functionalityId;
        AmputationLevelId = amputationLevelId;
    }
    
    public static Prosthetic New(ProstheticId id, string title, string description, double weight, 
        TypeId typeId, MaterialId materialId, FunctionalityId functionalityId, AmputationLevelId amputationLevelId)
        => new(id, title, description, weight, typeId, materialId, functionalityId, amputationLevelId);

    public void UpdateDetails(string title, string description, double weight)
    {
        Title = title;
        Description = description;
        Weight = weight;
    }
    
}