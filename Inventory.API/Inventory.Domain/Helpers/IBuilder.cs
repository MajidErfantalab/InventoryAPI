namespace Inventory.Domain.Helpers;

public interface IBuilder<out T>
{
    T Build();
}