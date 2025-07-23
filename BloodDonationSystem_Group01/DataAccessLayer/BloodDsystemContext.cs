using System;
using System.Collections.Generic;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public partial class BloodDsystemContext : DbContext
{
    public BloodDsystemContext()
    {
    }

    public BloodDsystemContext(DbContextOptions<BloodDsystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BloodComponent> BloodComponents { get; set; }

    public virtual DbSet<BloodType> BloodTypes { get; set; }

    public virtual DbSet<BloodUnit> BloodUnits { get; set; }

    public virtual DbSet<DonationHistory> DonationHistories { get; set; }

    public virtual DbSet<DonationRequest> DonationRequests { get; set; }

    public virtual DbSet<EmergencyNotification> EmergencyNotifications { get; set; }

    public virtual DbSet<EmergencyRequest> EmergencyRequests { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OtpCode> OtpCodes { get; set; }

    public virtual DbSet<ReminderLog> ReminderLogs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KHOI;Database=bloodDSystem;User Id=sa;Password=12345;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BloodComponent>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("PK__BloodCom__AEB1DA59F799027A");

            entity.HasIndex(e => e.ComponentName, "UQ__BloodCom__2E7CCD4B95F902C2").IsUnique();

            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.ComponentName)
                .HasMaxLength(255)
                .HasColumnName("component_name");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<BloodType>(entity =>
        {
            entity.HasKey(e => e.BloodTypeId).HasName("PK__BloodTyp__56FFB8C812255B67");

            entity.HasIndex(e => e.TypeName, "UQ__BloodTyp__543C4FD9684FD373").IsUnique();

            entity.Property(e => e.BloodTypeId).HasColumnName("blood_type_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TypeName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<BloodUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__BloodUni__D3AF5BD74F556B5E");

            entity.Property(e => e.UnitId)
                .HasMaxLength(36)
                .HasColumnName("unit_id");
            entity.Property(e => e.BloodTypeId).HasColumnName("blood_type_id");
            entity.Property(e => e.CollectionDate).HasColumnName("collection_date");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.DiscardReason).HasColumnName("discard_reason");
            entity.Property(e => e.DonationId)
                .HasMaxLength(36)
                .HasColumnName("donation_id");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Available")
                .HasColumnName("status");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(100)
                .HasColumnName("storage_location");
            entity.Property(e => e.TestResults).HasColumnName("test_results");
            entity.Property(e => e.VolumeMl).HasColumnName("volume_ml");

            entity.HasOne(d => d.Donation).WithMany(p => p.BloodUnits)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__BloodUnit__donat__7F2BE32F");
        });

        modelBuilder.Entity<DonationHistory>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__Donation__296B91DC7E0BDF30");

            entity.ToTable("DonationHistory");

            entity.Property(e => e.DonationId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("donation_id");
            entity.Property(e => e.BloodTypeId).HasColumnName("blood_type_id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.DonationDate).HasColumnName("donation_date");
            entity.Property(e => e.DonationRequestId)
                .HasMaxLength(36)
                .HasColumnName("donation_request_id");
            entity.Property(e => e.DonorUserId)
                .HasMaxLength(36)
                .HasColumnName("donor_user_id");
            entity.Property(e => e.EligibilityStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("eligibility_status");
            entity.Property(e => e.EmergencyId)
                .HasMaxLength(36)
                .HasColumnName("EmergencyID");
            entity.Property(e => e.QuantityMl).HasColumnName("quantity_ml");
            entity.Property(e => e.ReasonIneligible).HasColumnName("reason_ineligible");
            entity.Property(e => e.StaffUserId)
                .HasMaxLength(36)
                .HasColumnName("staff_user_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.TestingResults).HasColumnName("testing_results");

            entity.HasOne(d => d.BloodType).WithMany(p => p.DonationHistories)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationH__blood__00200768");

            entity.HasOne(d => d.DonationRequest).WithMany(p => p.DonationHistories)
                .HasForeignKey(d => d.DonationRequestId)
                .HasConstraintName("FK_DonationHistory_DonationRequests");

            entity.HasOne(d => d.DonorUser).WithMany(p => p.DonationHistoryDonorUsers)
                .HasForeignKey(d => d.DonorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationH__donor__01142BA1");

            entity.HasOne(d => d.Emergency).WithMany(p => p.DonationHistories)
                .HasForeignKey(d => d.EmergencyId)
                .HasConstraintName("FK_DonationHistory_EmergencyRequests");

            entity.HasOne(d => d.StaffUser).WithMany(p => p.DonationHistoryStaffUsers)
                .HasForeignKey(d => d.StaffUserId)
                .HasConstraintName("FK__DonationH__staff__02084FDA");
        });

        modelBuilder.Entity<DonationRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Donation__18D3B90F858114CD");

            entity.Property(e => e.RequestId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("request_id");
            entity.Property(e => e.BloodTypeId).HasColumnName("blood_type_id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.DonorUserId)
                .HasMaxLength(36)
                .HasColumnName("donor_user_id");
            entity.Property(e => e.PreferredDate).HasColumnName("preferred_date");
            entity.Property(e => e.PreferredTimeSlot)
                .HasMaxLength(50)
                .HasColumnName("preferred_time_slot");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("request_date");
            entity.Property(e => e.StaffNotes).HasColumnName("staff_notes");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");

            entity.HasOne(d => d.BloodType).WithMany(p => p.DonationRequests)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationR__blood__04E4BC85");

            entity.HasOne(d => d.DonorUser).WithMany(p => p.DonationRequests)
                .HasForeignKey(d => d.DonorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonationR__donor__05D8E0BE");
        });

        modelBuilder.Entity<EmergencyNotification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Emergenc__E059842FED452BE3");

            entity.Property(e => e.NotificationId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("notification_id");
            entity.Property(e => e.DeliveryMethod)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("delivery_method");
            entity.Property(e => e.EmergencyId)
                .HasMaxLength(36)
                .HasColumnName("emergency_id");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.RecipientUserId)
                .HasMaxLength(36)
                .HasColumnName("recipient_user_id");
            entity.Property(e => e.ResponseStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("No Response")
                .HasColumnName("response_status");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("sent_date");

            entity.HasOne(d => d.Emergency).WithMany(p => p.EmergencyNotifications)
                .HasForeignKey(d => d.EmergencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__emerg__06CD04F7");

            entity.HasOne(d => d.RecipientUser).WithMany(p => p.EmergencyNotifications)
                .HasForeignKey(d => d.RecipientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__recip__07C12930");
        });

        modelBuilder.Entity<EmergencyRequest>(entity =>
        {
            entity.HasKey(e => e.EmergencyId).HasName("PK__Emergenc__F0E90B915E043F21");

            entity.Property(e => e.EmergencyId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("emergency_id");
            entity.Property(e => e.BloodTypeId).HasColumnName("blood_type_id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.FulfillmentDate).HasColumnName("fulfillment_date");
            entity.Property(e => e.Priority)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Medium")
                .HasColumnName("priority");
            entity.Property(e => e.QuantityNeededMl).HasColumnName("quantity_needed_ml");
            entity.Property(e => e.RequesterUserId)
                .HasMaxLength(36)
                .HasColumnName("requester_user_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("status");

            entity.HasOne(d => d.BloodType).WithMany(p => p.EmergencyRequests)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__blood__08B54D69");

            entity.HasOne(d => d.Component).WithMany(p => p.EmergencyRequests)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__compo__09A971A2");

            entity.HasOne(d => d.RequesterUser).WithMany(p => p.EmergencyRequests)
                .HasForeignKey(d => d.RequesterUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Emergency__reque__0A9D95DB");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FFAF3A9F8");

            entity.Property(e => e.NotificationId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("notification_id");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.RecipientUserId)
                .HasMaxLength(36)
                .HasColumnName("recipient_user_id");
            entity.Property(e => e.SentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("sent_date");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.RecipientUser).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.RecipientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__recip__0B91BA14");
        });

        modelBuilder.Entity<OtpCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OtpCodes__3214EC0775BEA73E");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
        });

        modelBuilder.Entity<ReminderLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reminder__3214EC0710AD03BB");

            entity.Property(e => e.ReminderType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(36);
            entity.Property(e => e.Via)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.ReminderLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReminderLogs_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC0C87E84E");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__783254B1E38B9FF2").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FB3CDA6E3");

            entity.HasIndex(e => e.Email, "UC_Email").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164EFCADC8B").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572AB4405DB").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastLoginDate).HasColumnName("last_login_date");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("registration_date");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_id__0F624AF8");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__UserProf__AEBB701F1042E83C");

            entity.HasIndex(e => e.PhoneNumber, "UQ__UserProf__A1936A6BE06727A1").IsUnique();

            entity.HasIndex(e => e.Cccd, "UQ__UserProf__A955A0AA091EEEDF").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__UserProf__B9BE370E3121465D").IsUnique();

            entity.Property(e => e.ProfileId)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("profile_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BloodTypeId).HasColumnName("blood_type_id");
            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastBloodDonationDate).HasColumnName("last_blood_donation_date");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitude");
            entity.Property(e => e.MedicalHistory).HasColumnName("medical_history");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RhFactor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("rh_factor");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");

            entity.HasOne(d => d.BloodType).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.BloodTypeId)
                .HasConstraintName("FK__UserProfi__blood__0D7A0286");

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserProfi__user___0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
