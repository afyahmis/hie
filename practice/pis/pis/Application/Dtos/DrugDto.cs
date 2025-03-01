namespace pis.Application.Dtos;

public class DrugDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public double QuantityInStock { get; set; }
    public DateTime? Updated { get; set; }
    
    public override string ToString()
    {
        return $"{Code}-{Name} [stock:{QuantityInStock}]";
    }
}