using Core.Entities;

namespace Entities.Concrete;

public class Offer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UnitId { get; set; }
    public int UnitPrice { get; set; }
}