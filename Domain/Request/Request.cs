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
    public StatusId StatusId { get;  }
    public Status? Status { get;  }

    public Request(RequestId id, string description, UserId userId, ProstheticId prostheticId,
        StatusId statusId)
    {
        Id = id;
        Description = description;
        UserId = userId;
        ProstheticId = prostheticId;
        StatusId = statusId;
    }
    public static Request New(RequestId id, string description, UserId userId, ProstheticId prostheticId,
        StatusId statusId)
        => new(id, description, userId, prostheticId, statusId);
    
    public void UpdateDetails(string description, UserId userId, ProstheticId prostheticId, StatusId statusId)
    {
        Description = description;
    }
}