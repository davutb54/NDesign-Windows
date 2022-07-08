﻿using Entities.Abstract;

namespace Entities.Concrete;

public class Costs : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UnitId { get; set; }
    public int UnitPrice { get; set; }
}