﻿using Domain.Statuses;

namespace Api.Dtos.StatusDtos;

public record StatusDto(Guid? Id, string Title)
{
    public static StatusDto FromDomainModel(Status status)
        => new(status.Id.Value, status.Title);
}