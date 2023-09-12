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

    public virtual DbSet<BodyPerameter> BodyPerameters { get; set; }

    public virtual DbSet<Excercise> Excercises { get; set; }

    public virtual DbSet<ExcerciseSchedule> ExcerciseSchedules { get; set; }

    public virtual DbSet<ExcerciseType> ExcerciseTypes { get; set; }

    public virtual DbSet<ExerciseInExerciseType> ExerciseInExerciseTypes { get; set; }

    public virtual DbSet<ExserciseInSession> ExserciseInSessions { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<FoodAndSuppliment> FoodAndSuppliments { get; set; }

    public virtual DbSet<FoodAndSupplimentInFoodAndSupplimentType> FoodAndSupplimentInFoodAndSupplimentTypes { get; set; }

    public virtual DbSet<FoodAndSupplimentInMeal> FoodAndSupplimentInMeals { get; set; }

    public virtual DbSet<FoodAndSupplimentType> FoodAndSupplimentTypes { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<NutritionSchedule> NutritionSchedules { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageGymer> PackageGymers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionResult> SessionResults { get; set; }

    public virtual DbSet<Suspend> Suspends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =egts.database.windows.net; database = eGTS;uid=egts;pwd=Passdoan2023@;Trusted_Connection=True;Encrypt=False;Integrated Security=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BodyPerameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PhysicalPerameter");

            entity.ToTable("BodyPerameter");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Bmi).HasColumnName("BMI");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Goal).HasMaxLength(300);
            entity.Property(e => e.GymerId).HasColumnName("GymerID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");

            entity.HasOne(d => d.Gymer).WithMany(p => p.BodyPerameters)
                .HasForeignKey(d => d.GymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BodyPerameter_Account");
        });

        modelBuilder.Entity<Excercise>(entity =>
        {
            entity.ToTable("Excercise");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Ptid).HasColumnName("PTID");
            entity.Property(e => e.UnitOfMeasurement).HasMaxLength(50);
            entity.Property(e => e.Video).IsUnicode(false);

            entity.HasOne(d => d.Pt).WithMany(p => p.Excercises)
                .HasForeignKey(d => d.Ptid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Excercise_Account");
        });

        modelBuilder.Entity<ExcerciseSchedule>(entity =>
        {
            entity.ToTable("ExcerciseSchedule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.From).HasColumnType("date");
            entity.Property(e => e.GymerId).HasColumnName("GymerID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.PackageGymerId).HasColumnName("PackageGymerID");
            entity.Property(e => e.Ptid).HasColumnName("PTID");
            entity.Property(e => e.To).HasColumnType("date");

            entity.HasOne(d => d.Gymer).WithMany(p => p.ExcerciseScheduleGymers)
                .HasForeignKey(d => d.GymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExcerciseSchedule_Account");

            entity.HasOne(d => d.PackageGymer).WithMany(p => p.ExcerciseSchedules)
                .HasForeignKey(d => d.PackageGymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExcerciseSchedule_PackageGymer");

            entity.HasOne(d => d.Pt).WithMany(p => p.ExcerciseSchedulePts)
                .HasForeignKey(d => d.Ptid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExcerciseSchedule_Account1");
        });

        modelBuilder.Entity<ExcerciseType>(entity =>
        {
            entity.ToTable("ExcerciseType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Ptid).HasColumnName("PTID");

            entity.HasOne(d => d.Pt).WithMany(p => p.ExcerciseTypes)
                .HasForeignKey(d => d.Ptid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExcerciseType_Account");
        });

        modelBuilder.Entity<ExerciseInExerciseType>(entity =>
        {
            entity.ToTable("ExerciseInExerciseType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");
            entity.Property(e => e.ExerciseTypeId).HasColumnName("ExerciseTypeID");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseInExerciseTypes)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseInExerciseType_Excercise");

            entity.HasOne(d => d.ExerciseType).WithMany(p => p.ExerciseInExerciseTypes)
                .HasForeignKey(d => d.ExerciseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseInExerciseType_ExcerciseType");
        });

        modelBuilder.Entity<ExserciseInSession>(entity =>
        {
            entity.ToTable("ExserciseInSession");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExserciseInSessions)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExserciseInSession_Excercise");

            entity.HasOne(d => d.Session).WithMany(p => p.ExserciseInSessions)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExserciseInSession_Session");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.ToTable("FeedBack");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Feedback1)
                .HasMaxLength(300)
                .HasColumnName("Feedback");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.PackageGymerId).HasColumnName("PackageGymerID");
            entity.Property(e => e.PtidorNeid).HasColumnName("PTIDorNEID");

            entity.HasOne(d => d.PackageGymer).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.PackageGymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeedBack_PackageGymer");

            entity.HasOne(d => d.PtidorNe).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.PtidorNeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeedBack_Account");
        });

        modelBuilder.Entity<FoodAndSuppliment>(entity =>
        {
            entity.ToTable("FoodAndSuppliment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Neid).HasColumnName("NEID");
            entity.Property(e => e.UnitOfMesuament).HasMaxLength(50);

            entity.HasOne(d => d.Ne).WithMany(p => p.FoodAndSuppliments)
                .HasForeignKey(d => d.Neid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FoodAndSuppliment_Account");
        });

        modelBuilder.Entity<FoodAndSupplimentInFoodAndSupplimentType>(entity =>
        {
            entity.ToTable("FoodAndSupplimentInFoodAndSupplimentType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FoodAndSupplimentId).HasColumnName("FoodAndSupplimentID");
            entity.Property(e => e.FoodAndSupplimentTypeId).HasColumnName("FoodAndSupplimentTypeID");

            entity.HasOne(d => d.FoodAndSuppliment).WithMany(p => p.FoodAndSupplimentInFoodAndSupplimentTypes)
                .HasForeignKey(d => d.FoodAndSupplimentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FoodAndSupplimentInFoodAndSupplimentType_FoodAndSuppliment");

            entity.HasOne(d => d.FoodAndSupplimentType).WithMany(p => p.FoodAndSupplimentInFoodAndSupplimentTypes)
                .HasForeignKey(d => d.FoodAndSupplimentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FoodAndSupplimentInFoodAndSupplimentType_FoodAndSupplimentType");
        });

        modelBuilder.Entity<FoodAndSupplimentInMeal>(entity =>
        {
            entity.ToTable("FoodAndSupplimentInMeal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FoodAndSupplimentId).HasColumnName("FoodAndSupplimentID");
            entity.Property(e => e.MealId).HasColumnName("MealID");

            entity.HasOne(d => d.FoodAndSuppliment).WithMany(p => p.FoodAndSupplimentInMeals)
                .HasForeignKey(d => d.FoodAndSupplimentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FoodAndSupplimentInMeal_FoodAndSuppliment");

            entity.HasOne(d => d.Meal).WithMany(p => p.FoodAndSupplimentInMeals)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FoodAndSupplimentInMeal_Meal");
        });

        modelBuilder.Entity<FoodAndSupplimentType>(entity =>
        {
            entity.ToTable("FoodAndSupplimentType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Neid).HasColumnName("NEID");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.ToTable("Meal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.NutritionScheduleId).HasColumnName("NutritionScheduleID");

            entity.HasOne(d => d.NutritionSchedule).WithMany(p => p.Meals)
                .HasForeignKey(d => d.NutritionScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meal_NutritionSchedule");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Message1)
                .HasMaxLength(300)
                .HasColumnName("Message");
            entity.Property(e => e.RecieverId).HasColumnName("RecieverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Reciever).WithMany(p => p.MessageRecievers)
                .HasForeignKey(d => d.RecieverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Account");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Account1");
        });

        modelBuilder.Entity<NutritionSchedule>(entity =>
        {
            entity.ToTable("NutritionSchedule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GymerId).HasColumnName("GymerID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Neid).HasColumnName("NEID");
            entity.Property(e => e.PackageGymerId).HasColumnName("PackageGymerID");

            entity.HasOne(d => d.PackageGymer).WithMany(p => p.NutritionSchedules)
                .HasForeignKey(d => d.PackageGymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NutritionSchedule_PackageGymer");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.ToTable("Package");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.HasNe).HasColumnName("HasNE");
            entity.Property(e => e.HasPt).HasColumnName("HasPT");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Necost).HasColumnName("NECost");
            entity.Property(e => e.NumberOfsession).HasColumnName("NumberOFSession");
            entity.Property(e => e.Ptcost).HasColumnName("PTCost");
        });

        modelBuilder.Entity<PackageGymer>(entity =>
        {
            entity.ToTable("PackageGymer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.From).HasColumnType("datetime");
            entity.Property(e => e.GymerId).HasColumnName("GymerID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Neid).HasColumnName("NEID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Ptid).HasColumnName("PTID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.To).HasColumnType("datetime");

            entity.HasOne(d => d.Gymer).WithMany(p => p.PackageGymerGymers)
                .HasForeignKey(d => d.GymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageGymer_Account2");

            entity.HasOne(d => d.Ne).WithMany(p => p.PackageGymerNes)
                .HasForeignKey(d => d.Neid)
                .HasConstraintName("FK_PackageGymer_Account1");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageGymers)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_PackageGymer_Package");

            entity.HasOne(d => d.Pt).WithMany(p => p.PackageGymerPts)
                .HasForeignKey(d => d.Ptid)
                .HasConstraintName("FK_PackageGymer_Account");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.PackageGymerId).HasColumnName("PackageGymerID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.PackageGymer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PackageGymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_PackageGymer");
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.ExpertId);

            entity.ToTable("Qualification");

            entity.Property(e => e.ExpertId)
                .ValueGeneratedNever()
                .HasColumnName("ExpertID");
            entity.Property(e => e.Certificate).IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsCetifide).HasColumnName("isCetifide");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");

            entity.HasOne(d => d.Expert).WithOne(p => p.Qualification)
                .HasForeignKey<Qualification>(d => d.ExpertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Qualification_Account");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GymerId).HasColumnName("GymerID");
            entity.Property(e => e.IsAccepted).HasColumnName("isAccepted");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.IsPt).HasColumnName("isPT");
            entity.Property(e => e.PackageGymerId).HasColumnName("PackageGymerID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

            entity.HasOne(d => d.Gymer).WithMany(p => p.RequestGymers)
                .HasForeignKey(d => d.GymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_Account");

            entity.HasOne(d => d.PackageGymer).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PackageGymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_PackageGymer");

            entity.HasOne(d => d.Receiver).WithMany(p => p.RequestReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_Account1");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.From).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.To).HasColumnType("datetime");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_ExcerciseSchedule");
        });

        modelBuilder.Entity<SessionResult>(entity =>
        {
            entity.ToTable("SessionResult");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionResults)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SessionResult_Session");
        });

        modelBuilder.Entity<Suspend>(entity =>
        {
            entity.ToTable("Suspend");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.From).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.PackageGymerId).HasColumnName("PackageGymerID");
            entity.Property(e => e.Reson).HasMaxLength(100);
            entity.Property(e => e.To).HasColumnType("datetime");

            entity.HasOne(d => d.PackageGymer).WithMany(p => p.Suspends)
                .HasForeignKey(d => d.PackageGymerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suspend_PackageGymer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
