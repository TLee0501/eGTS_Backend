using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public string? Description { get; set; }

    public string? Certificate { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public bool IsLock { get; set; }

    public virtual ICollection<Contract> ContractExperts { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractGymers { get; set; } = new List<Contract>();

    public virtual ICollection<FoodSchedule> FoodSchedules { get; set; } = new List<FoodSchedule>();

    public virtual ICollection<Message> MessageRecivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Package> PackageGymers { get; set; } = new List<Package>();

    public virtual ICollection<Package> PackageNes { get; set; } = new List<Package>();

    public virtual ICollection<Package> PackagePts { get; set; } = new List<Package>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public Account(Guid id, string phoneNo, string password, string role, string fullName, DateTime createTime, bool isLock)
    {
        Id = id;
        PhoneNo = phoneNo;
        Password = password;
        Role = role;
        FullName = fullName;
        CreateTime = createTime;
        IsLock = isLock;
    }
}
