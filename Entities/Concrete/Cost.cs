using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete;

public class Cost : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int UnitId { get; set; }
    public double UnitPrice { get; set; }

}