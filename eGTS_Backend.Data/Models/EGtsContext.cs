using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eGTS_Backend.Data.Models;

public partial class EGtsContext : DbContext
{
    public EGtsContext()
    {
    }

    public EGtsContext(DbContextOptions<EGtsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodSchedule> FoodSchedules { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<WorkoutList> WorkoutLists { get; set; }

    public virtual DbSet<WorkoutResult> WorkoutResults { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = eGTS;uid=sa;pwd=123;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Certificate).IsUnicode(false);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsLock).HasColumnName("isLock");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Gymer_Contract");

            entity.ToTable("Contract");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FinishDate).HasColumnType("date");
            entity.Property(e => e.GymerId).HasColumnName("GymerID");
            entity.Property(e => e.Neid).HasColumnName("NEID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Ptid).HasColumnName("PTID");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Gymer).WithMany(p => p.ContractGymers)
                .HasForeignKey(d => d.GymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Account");

            entity.HasOne(d => d.Package).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Package");

            entity.HasOne(d => d.Pt).WithMany(p => p.ContractPts)
                .HasForeignKey(d => d.Ptid)
                .HasConstraintName("FK_Contract_Account1");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Feedback1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Feedback");

            entity.HasOne(d => d.Contract).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Contract");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.ToTable("Food");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FoodScheduleId).HasColumnName("FoodScheduleID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.FoodSchedule).WithMany(p => p.Foods)
                .HasForeignKey(d => d.FoodScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_Food_Schedule");
        });

        modelBuilder.Entity<FoodSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Food Schedule");

            entity.ToTable("Food_Schedule");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Neid).HasColumnName("NEID");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Contract).WithMany(p => p.FoodSchedules)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_Schedule_Contract");

            entity.HasOne(d => d.Ne).WithMany(p => p.FoodSchedules)
                .HasForeignKey(d => d.Neid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_Schedule_Account1");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Message1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Message");
            entity.Property(e => e.ReciverId).HasColumnName("ReciverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Reciver).WithMany(p => p.MessageRecivers)
                .HasForeignKey(d => d.ReciverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Account1");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Account");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.ToTable("Package");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.HasNe).HasColumnName("hasNE");
            entity.Property(e => e.HasPt).HasColumnName("hasPT");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.PaymentDate).HasColumnType("date");

            entity.HasOne(d => d.Contract).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Contract");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Contract).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedule_Contract");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.ToTable("Workout");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.VidieoId).HasColumnName("VidieoID");
        });

        modelBuilder.Entity<WorkoutList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WorkoutList");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.WorkoutId).HasColumnName("WorkoutID");

            entity.HasOne(d => d.Schedule).WithMany()
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkoutList_Schedule");

            entity.HasOne(d => d.Workout).WithMany()
                .HasForeignKey(d => d.WorkoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkoutList_Workout");
        });

        modelBuilder.Entity<WorkoutResult>(entity =>
        {
            entity.ToTable("Workout_Result");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Description).IsUnicode(false);

            entity.HasOne(d => d.Contract).WithMany(p => p.WorkoutResults)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workout_Result_Contract");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
