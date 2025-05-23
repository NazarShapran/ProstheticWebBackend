﻿using Domain.Users;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<Option<User>> GetById(UserId userId, CancellationToken cancellationToken);
    Task<User> Create(User user, CancellationToken cancellationToken);
    Task<User> Update(User user, CancellationToken cancellationToken);
    Task<User> Delete(User user, CancellationToken cancellationToken);
    
    Task<Option<User>> GetByEmailAndPassword(string email, string password, CancellationToken cancellationToken);
    Task<Option<User>> GetByEmail(string email, CancellationToken cancellationToken);
}