using CSharpFunctionalExtensions;

namespace pis.Domain;

public class Drug : Entity<Guid>
{
    public string Name { get;private set; }
    public string Code { get;private set; }
    public double QuantityInStock { get;private set; }
    public DateTime? Updated { get;private set; }

    private Drug()
    {
        Id = Guid.NewGuid();;
    }

    public Drug(string name, string code, double quantityInStock)
        : this()
    {
        Name = name;
        Code = code;
        QuantityInStock = quantityInStock;
    }

    public void Dispense(double quantity = 1)
    {
        if (quantity > QuantityInStock)
            throw new Exception($"Cannot dispense {quantity} > Stock: {QuantityInStock}");

        Adjust(quantity * -1);
    }

    public void Adjust(double quantity)
    {
        if (quantity > 0)
        {
            QuantityInStock += quantity;
            Updated=DateTime.Now;
            return;
        }

        quantity = Math.Abs(quantity);
        
        if (quantity > QuantityInStock)
            throw new Exception($"Cannot reduce {quantity} > available: {QuantityInStock}");

        QuantityInStock -= quantity;
        Updated=DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Code}-{Name} [stock:{QuantityInStock}]";
    }
}