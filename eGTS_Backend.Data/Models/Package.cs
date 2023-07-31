using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Package
{
    public Package()
    {
    }

    public Package(Guid id, string name, bool hasPt, bool hasNe, short? numberOfsession, short? numberOfMonth, double? ptcost, double? necost, double? centerCost, double price, bool isDelete)
    {
        Id = id;
        Name = name;
        HasPt = hasPt;
        HasNe = hasNe;
        NumberOfsession = numberOfsession;
        NumberOfMonth = numberOfMonth;
        Ptcost = ptcost;
        Necost = necost;
        CenterCost = centerCost;
        Price = price;
        IsDelete = isDelete;
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool HasPt { get; set; }

    public bool HasNe { get; set; }

    public short? NumberOfsession { get; set; }

    public short? NumberOfMonth { get; set; }

    public double? Ptcost { get; set; }

    public double? Necost { get; set; }

    public double? CenterCost { get; set; }

    public double Price { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<PackageGymer> PackageGymers { get; set; } = new List<PackageGymer>();
}
