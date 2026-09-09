using TaskHub.Domain.Entities;
using Xunit;

namespace TaskHub.UnitTests.Entities;

public sealed class WorkspaceTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesWorkspace()
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        // Act
        var workspace = new Workspace("  Meu workspace  ", ownerId);

        // Assert
        Assert.NotEqual(Guid.Empty, workspace.Id);
        Assert.Equal("Meu workspace", workspace.Name);
        Assert.Equal(ownerId, workspace.OwnerId);
        Assert.NotEqual(default(DateTimeOffset), workspace.CreatedAtUtc);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidName_ThrowsArgumentException(
        string invalidName
    )
    {
        // Arrange
        var ownerId = Guid.NewGuid();

        // Act
        Action action = () => new Workspace(invalidName, ownerId);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Constructor_WithEmptyOwnerId_ThrowsArgumentException()
    {
        // Arrange
        var emptyOwnerId = Guid.Empty;

        // Act
        Action action = () => new Workspace("Meu workspace", emptyOwnerId);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
}