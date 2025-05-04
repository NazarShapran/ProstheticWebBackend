using Domain.Prosthetics;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProstheticRepository
{
    Task<Prosthetic> Add(Prosthetic prosthetic, CancellationToken cancellationToken);
    Task<Prosthetic> Update(Prosthetic prosthetic, CancellationToken cancellationToken);
    Task<Prosthetic> Delete(Prosthetic prosthetic, CancellationToken cancellationToken);
    
    Task<Option<Prosthetic>> SearchByTitle(string title, CancellationToken cancellationToken);

    Task<Option<Prosthetic>> GetById(ProstheticId id, CancellationToken cancellationToken);
}