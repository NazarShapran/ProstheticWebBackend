﻿using Domain.Statuses;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IStatusRepository
{
    Task<Option<Status>> GetById(StatusId statusId, CancellationToken cancellationToken);
    Task<Status> Delete(Status status, CancellationToken cancellationToken);
    Task<Status> Create(Status status, CancellationToken cancellationToken);
    Task<Status> Update(Status status, CancellationToken cancellationToken);
    
    Task<Option<Status>> SearchByTitle(string title, CancellationToken cancellationToken);
}