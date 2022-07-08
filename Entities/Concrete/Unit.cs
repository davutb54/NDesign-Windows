using Core.Entities;

namespace Entities.Concrete;

public class Unit : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

}