namespace Domain.ProstheticStatuses;

public class ProstheticStatus
{
    public ProstheticStatusId Id { get; }
    public string Title { get; private set; }

    public ProstheticStatus(ProstheticStatusId id, string title)
    {
        Id = id;
        Title = title;
    }

    public static ProstheticStatus New(ProstheticStatusId id, string title)
        => new(id, title);

    public void ChangeTitle(string newTitle)
    {
        Title = newTitle;
    }
} 