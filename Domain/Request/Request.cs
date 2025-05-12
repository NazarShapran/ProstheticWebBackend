using Domain.Prosthetics;
using Domain.Statuses;
using Domain.Users;

namespace Domain.Request;

public class Request
{
    public RequestId Id { get; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
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
        StatusId = StatusId.FromString("fcbc4de6-1457-4be0-bbf6-b34f6a456866");
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