using Domain.Prosthetics;
using Domain.Statuses;
using Domain.Users;

namespace Domain.Request;

public class Request
{
    public RequestId Id { get; }
    public string Description { get; private set; }
    public UserId UserId { get; }
    public User? User { get;  }
    public ProstheticId ProstheticId { get; }
    public Prosthetic? Prosthetic { get; }
    public StatusId? StatusId { get;  private set;}
    public Status? Status { get;  }

    public Request(RequestId id, string description, UserId userId, ProstheticId prostheticId)
    {
        Id = id;
        Description = description;
        UserId = userId;
        ProstheticId = prostheticId;
        StatusId = StatusId.FromString("f80ba821-8bec-4948-a3e5-2c6722dc3d13");
    }
    public static Request New(RequestId id, string description, UserId userId, ProstheticId prostheticId)
        => new(id, description, userId, prostheticId);
    
    public void UpdateDetails(string description, UserId userId, ProstheticId prostheticId, StatusId statusId)
    {
        Description = description;
    }
    
    public void ChangeStatus(StatusId statusId)
    {
        StatusId = statusId;
    }
}