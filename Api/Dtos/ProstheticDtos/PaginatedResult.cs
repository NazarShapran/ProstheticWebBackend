namespace Api.Dtos.ProstheticDtos;

public class PaginatedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
}