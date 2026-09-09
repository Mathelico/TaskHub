namespace TaskHub.Domain.Entities;

public sealed class Workspace
{
    private Workspace()
    {
    }

    public Workspace(string name, Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Workspace name is required.",
                nameof(name)
            );
        }

        if (ownerId == Guid.Empty)
        {
            throw new ArgumentException(
                "Owner identifier is required.",
                nameof(ownerId)
            );
        }

        Id = Guid.NewGuid();
        Name = name.Trim();
        OwnerId = ownerId;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Guid OwnerId { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException(
                "Workspace name is required.",
                nameof(newName)
            );
        }

        Name = newName.Trim();
    }
}