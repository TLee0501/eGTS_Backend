using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class Contract
{
    public Guid Id { get; set; }

    public Guid GymerId { get; set; }

    public Guid ExpertId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime FinishDate { get; set; }

    public short NumberOfDays { get; set; }

    public virtual Account Expert { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<FoodSchedule> FoodSchedules { get; set; } = new List<FoodSchedule>();

    public virtual Account Gymer { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<WorkoutResult> WorkoutResults { get; set; } = new List<WorkoutResult>();
}
