using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGTS_Backend.Data.Models;

public partial class Account
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(11)]
    public string PhoneNo { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(10)]
    public string Role { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string FullName { get; set; } = null!;

    [Required]
    public DateTime CreateTime { get; set; }

    public string? Description { get; set; }

    public string? Certificate { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public virtual ICollection<Contract> ContractExperts { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractGymers { get; set; } = new List<Contract>();

    public virtual ICollection<FoodSchedule> FoodSchedules { get; set; } = new List<FoodSchedule>();

    public virtual ICollection<Message> MessageRecivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Package> PackageGymers { get; set; } = new List<Package>();

    public virtual ICollection<Package> PackageNes { get; set; } = new List<Package>();

    public virtual ICollection<Package> PackagePts { get; set; } = new List<Package>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
