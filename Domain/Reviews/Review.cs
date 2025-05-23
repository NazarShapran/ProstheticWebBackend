﻿using Domain.Prosthetics;
using Domain.Users;

namespace Domain.Reviews;

public class Review
{
    public ReviewId Id { get;  }
    public string Description { get; private set; }
    public string Pros { get; private set; }
    public string Cons { get; private set; }
    public DateTime Date { get; private set; }
    public UserId UserId { get; }
    public User? User { get; }
    public ProstheticId ProstheticId { get; }
    public Prosthetic? Prosthetic { get; }
    
    public Review(ReviewId id, string description, string pros, string cons, UserId userId, ProstheticId prostheticId)
    {
        Id = id;
        Description = description;
        Pros = pros;
        Cons = cons;
        UserId = userId;
        ProstheticId = prostheticId;
        Date = DateTime.UtcNow;
    }
    public static Review New(ReviewId id, string description, string pros, string cons, UserId userId, ProstheticId prostheticId)
        => new(id, description, pros, cons, userId, prostheticId);
    
    public void UpdateDetails(string description, string pros, string cons)
    {
        Description = description;
        Pros = pros;
        Cons = cons;
    }
}