using FluentAssertions;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Tests.Features.Inventory;

public class InventoryTests
{
    [Fact]
    public void Increase_Should_Increase_Quantity()
    {
        // Arrange
        var inventory = new Inventory(
            Guid.NewGuid(),
            Guid.NewGuid(),
            10);

        // Act
        inventory.Increase(5);

        // Assert
        inventory.Quantity.Should().Be(15);
    }

    [Fact]
    public void Decrease_Should_Decrease_Quantity()
    {
        // Arrange
        var inventory = new Inventory(
            Guid.NewGuid(),
            Guid.NewGuid(),
            20);

        // Act
        inventory.Decrease(5);

        // Assert
        inventory.Quantity.Should().Be(15);
    }

    [Fact]
    public void Decrease_Should_Throw_When_Quantity_Is_Insufficient()
    {
        // Arrange
        var inventory = new Inventory(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5);

        // Act
        Action action = () => inventory.Decrease(10);

        // Assert
        action.Should().Throw<Exception>()
            .WithMessage("Insufficient inventory.");
    }

    [Fact]
    public void Increase_Should_Throw_When_Quantity_Is_Less_Than_Or_Equal_To_Zero()
    {
        // Arrange
        var inventory = new Inventory(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5);

        // Act
        Action action = () => inventory.Increase(0);

        // Assert
        action.Should().Throw<Exception>()
            .WithMessage("Quantity must be greater than zero.");
    }

    [Fact]
    public void Decrease_Should_Throw_When_Quantity_Is_Less_Than_Or_Equal_To_Zero()
    {
        // Arrange
        var inventory = new Inventory(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5);

        // Act
        Action action = () => inventory.Decrease(0);

        // Assert
        action.Should().Throw<Exception>()
            .WithMessage("Quantity must be greater than zero.");
    }
}