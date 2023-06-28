﻿using System;
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

    public virtual ICollection<Contract> ContractGymers { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractPts { get; set; } = new List<Contract>();

    public virtual ICollection<FoodSchedule> FoodSchedules { get; set; } = new List<FoodSchedule>();

    public virtual ICollection<Message> MessageRecivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}
