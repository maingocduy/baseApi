using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskMonitor.Databases.TM;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Account { get; set; }

    public virtual DbSet<ActionLogs> ActionLogs { get; set; }

    public virtual DbSet<Activity> Activity { get; set; }

    public virtual DbSet<ActivityReport> ActivityReport { get; set; }

    public virtual DbSet<Branches> Branches { get; set; }

    public virtual DbSet<Contractor> Contractor { get; set; }

    public virtual DbSet<ContractorCat> ContractorCat { get; set; }

    public virtual DbSet<DevvnQuanhuyen> DevvnQuanhuyen { get; set; }

    public virtual DbSet<DevvnTinhthanhpho> DevvnTinhthanhpho { get; set; }

    public virtual DbSet<DevvnXaphuongthitran> DevvnXaphuongthitran { get; set; }

    public virtual DbSet<Guarantee> Guarantee { get; set; }

    public virtual DbSet<Notify> Notify { get; set; }

    public virtual DbSet<NotifyAcc> NotifyAcc { get; set; }

    public virtual DbSet<Otp> Otp { get; set; }

    public virtual DbSet<OverviewReport> OverviewReport { get; set; }

    public virtual DbSet<Project> Project { get; set; }

    public virtual DbSet<ProjectAnnual> ProjectAnnual { get; set; }

    public virtual DbSet<ProjectContractor> ProjectContractor { get; set; }

    public virtual DbSet<ProjectFund> ProjectFund { get; set; }

    public virtual DbSet<Reports> Reports { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Sessions> Sessions { get; set; }

    public virtual DbSet<Task> Task { get; set; }

    public virtual DbSet<TaskCat> TaskCat { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserProjects> UserProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_520_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("account");

            entity.HasIndex(e => e.RoleUuid, "fk_role_account_uuid");

            entity.HasIndex(e => e.UserUuid, "fk_user_account_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.RoleUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("role_uuid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng , 2-khóa")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.RoleUu).WithMany(p => p.Account)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.RoleUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_account_uuid");

            entity.HasOne(d => d.UserUu).WithMany(p => p.Account)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_account_uuid");
        });

        modelBuilder.Entity<ActionLogs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("action_logs");

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.ActUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("act_uuid");
            entity.Property(e => e.Action)
                .HasComment("0 - Duyệt báo cáo ; 1 - Từ chối báo cáo ; 2 - Gửi báo cáo ; 3 - Duyệt báo cáo giải ngân ; 4 - Từ chối báo cáo giải ngân ; 5 - Gửi báo cáo giải ngân")
                .HasColumnType("tinyint(4)")
                .HasColumnName("action");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.NewValue)
                .HasColumnType("double(15,4)")
                .HasColumnName("new_value");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.OldValue)
                .HasColumnType("double(15,4)")
                .HasColumnName("old_value");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("activity");

            entity.HasIndex(e => e.ParentTaskUuid, "fk_additional_task_activity_uuid");

            entity.HasIndex(e => e.ProjectUuid, "fk_project_activity_uuid");

            entity.HasIndex(e => e.TaskUuid, "fk_task_activity_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.ParentTaskUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasComment("để biết công việc phát sinh thuộc công việc nào")
                .HasColumnName("parent_task_uuid");
            entity.Property(e => e.Priority)
                .HasColumnType("tinyint(4)")
                .HasColumnName("priority");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("project_uuid");
            entity.Property(e => e.State)
                .HasComment("0 - chưa xử lý , 1 - đang xử lý , 2 - hoàn thành")
                .HasColumnType("tinyint(5)")
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.TaskUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("task_uuid");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.ParentTaskUu).WithMany(p => p.ActivityParentTaskUu)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ParentTaskUuid)
                .HasConstraintName("fk_additional_task_activity_uuid");

            entity.HasOne(d => d.ProjectUu).WithMany(p => p.Activity)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ProjectUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_activity_uuid");

            entity.HasOne(d => d.TaskUu).WithMany(p => p.ActivityTaskUu)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.TaskUuid)
                .HasConstraintName("fk_task_activity_uuid");
        });

        modelBuilder.Entity<ActivityReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("activity_report");

            entity.HasIndex(e => e.ActivityUuid, "fk_activity_report_uuid");

            entity.HasIndex(e => e.ReportUuid, "fk_report_ar_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.ActivityUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("activity_uuid");
            entity.Property(e => e.Completed)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("completed");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.ExpectEnd)
                .HasColumnType("timestamp")
                .HasColumnName("expect_end");
            entity.Property(e => e.ExpectStart)
                .HasColumnType("timestamp")
                .HasColumnName("expect_start");
            entity.Property(e => e.Issue)
                .HasColumnType("text")
                .HasColumnName("issue");
            entity.Property(e => e.Progress)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("progress");
            entity.Property(e => e.RealEnd)
                .HasColumnType("timestamp")
                .HasColumnName("real_end");
            entity.Property(e => e.RealStart)
                .HasColumnType("timestamp")
                .HasColumnName("real_start");
            entity.Property(e => e.ReportUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("report_uuid");
            entity.Property(e => e.State)
                .HasComment("0 - chưa xử lý , 1 - đang xử lý , 2 - hoàn thành")
                .HasColumnType("tinyint(4)")
                .HasColumnName("state");
            entity.Property(e => e.StateNote)
                .HasComment("Trạng thái số hóa: 0 - Chưa số hóa, 1 - Đã số hóa")
                .HasColumnType("tinyint(4)")
                .HasColumnName("state_note");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.ActivityUu).WithMany(p => p.ActivityReport)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ActivityUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_activity_ar_uuid");

            entity.HasOne(d => d.ReportUu).WithMany(p => p.ActivityReport)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ReportUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_report_ar_uuid");
        });

        modelBuilder.Entity<Branches>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("branches");

            entity.HasIndex(e => e.Maqh, "fk_maqh_branches");

            entity.HasIndex(e => e.Matp, "fk_matp_branches");

            entity.HasIndex(e => e.Xaid, "fk_xaid_branches");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Xaid)
                .HasMaxLength(5)
                .HasColumnName("xaid");

            entity.HasOne(d => d.MaqhNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.Maqh)
                .HasConstraintName("fk_maqh_branches");

            entity.HasOne(d => d.MatpNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.Matp)
                .HasConstraintName("fk_matp_branches");

            entity.HasOne(d => d.Xa).WithMany(p => p.Branches)
                .HasForeignKey(d => d.Xaid)
                .HasConstraintName("fk_xaid_branches");
        });

        modelBuilder.Entity<Contractor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contractor");

            entity.HasIndex(e => e.Maqh, "fk_maqh_contractor");

            entity.HasIndex(e => e.Matp, "fk_matp_contractor");

            entity.HasIndex(e => e.Type, "fk_type_cc");

            entity.HasIndex(e => e.Xaid, "fk_xaid_contractor");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasColumnType("int(5)")
                .HasColumnName("type");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Xaid)
                .HasMaxLength(5)
                .HasColumnName("xaid");

            entity.HasOne(d => d.MaqhNavigation).WithMany(p => p.Contractor)
                .HasForeignKey(d => d.Maqh)
                .HasConstraintName("fk_maqh_contractor");

            entity.HasOne(d => d.MatpNavigation).WithMany(p => p.Contractor)
                .HasForeignKey(d => d.Matp)
                .HasConstraintName("fk_matp_contractor");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Contractor)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_type_cc");

            entity.HasOne(d => d.Xa).WithMany(p => p.Contractor)
                .HasForeignKey(d => d.Xaid)
                .HasConstraintName("fk_xaid_contractor");
        });

        modelBuilder.Entity<ContractorCat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contractor_cat");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.IsDefault)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("isDefault");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<DevvnQuanhuyen>(entity =>
        {
            entity.HasKey(e => e.Maqh).HasName("PRIMARY");

            entity
                .ToTable("devvn_quanhuyen")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<DevvnTinhthanhpho>(entity =>
        {
            entity.HasKey(e => e.Matp).HasName("PRIMARY");

            entity
                .ToTable("devvn_tinhthanhpho")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Slug)
                .HasMaxLength(30)
                .HasColumnName("slug");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<DevvnXaphuongthitran>(entity =>
        {
            entity.HasKey(e => e.Xaid).HasName("PRIMARY");

            entity
                .ToTable("devvn_xaphuongthitran")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Xaid)
                .HasMaxLength(5)
                .HasColumnName("xaid")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Guarantee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("guarantee");

            entity.HasIndex(e => e.PcUuid, "fk_pc_guarantee");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("double(15,4)")
                .HasColumnName("amount");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp")
                .HasColumnName("end_date");
            entity.Property(e => e.PcUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("pc_uuid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("'1'")
                .HasComment("1 - Bảo lãnh dự án, 2 - Bảo lãnh giải ngân")
                .HasColumnType("tinyint(4)")
                .HasColumnName("type");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.PcUu).WithMany(p => p.Guarantee)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.PcUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pc_guarantee");
        });

        modelBuilder.Entity<Notify>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notify");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(8)")
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasColumnType("tinyint(4)")
                .HasColumnName("type");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<NotifyAcc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notify_acc");

            entity.HasIndex(e => e.AccUuid, "fk_acc_uuid");

            entity.HasIndex(e => e.NotifyUuid, "fk_notify_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AccUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("acc_uuid");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Data)
                .HasColumnName("data")
                .UseCollation("utf8mb4_bin");
            entity.Property(e => e.NotifyUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("notify_uuid");
            entity.Property(e => e.State)
                .HasComment("0 - chưa xem /  1- đã xem")
                .HasColumnType("int(4)")
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("1:Đang hoạt động/0:bị khóa")
                .HasColumnType("int(4)")
                .HasColumnName("status");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.AccUu).WithMany(p => p.NotifyAcc)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.AccUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notify_acc_ibfk_1");

            entity.HasOne(d => d.NotifyUu).WithMany(p => p.NotifyAcc)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.NotifyUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notify_acc_ibfk_2");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("otp");

            entity.HasIndex(e => e.AccUuid, "fk_otp_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.AccUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("acc_uuid");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Expired)
                .HasColumnType("timestamp")
                .HasColumnName("expired");
            entity.Property(e => e.Otp1)
                .HasMaxLength(30)
                .HasColumnName("otp");
            entity.Property(e => e.State)
                .HasColumnType("tinyint(4)")
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng ")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.AccUu).WithMany(p => p.Otp)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.AccUuid)
                .HasConstraintName("fk_otp_uuid");
        });

        modelBuilder.Entity<OverviewReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("overview_report");

            entity.HasIndex(e => e.FundReportUuid, "fk_fund_report_uuid");

            entity.HasIndex(e => e.ReportUuid, "fk_report_uuid");

            entity.HasIndex(e => e.ReporterUuid, "fk_reporter_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.FundReportUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("fund_report_uuid");
            entity.Property(e => e.Month)
                .HasColumnType("int(3)")
                .HasColumnName("month");
            entity.Property(e => e.ReportUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("report_uuid");
            entity.Property(e => e.ReporterUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("reporter_uuid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng , 2-khóa")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Year)
                .HasColumnType("int(4)")
                .HasColumnName("year");

            entity.HasOne(d => d.FundReportUu).WithMany(p => p.OverviewReport)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.FundReportUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fund_report_uuid");

            entity.HasOne(d => d.ReportUu).WithMany(p => p.OverviewReport)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ReportUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_report_uuid");

            entity.HasOne(d => d.ReporterUu).WithMany(p => p.OverviewReport)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ReporterUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reporter_uuid");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project");

            entity.HasIndex(e => e.BrachUuid, "fk_brach_project_uuid");

            entity.HasIndex(e => e.Maqh, "fk_maqh_project");

            entity.HasIndex(e => e.Matp, "fk_matp_project");

            entity.HasIndex(e => e.Xaid, "fk_xaid_project");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.AccumAmount)
                .HasDefaultValueSql("'0.0000'")
                .HasColumnType("double(15,4)")
                .HasColumnName("accum_amount");
            entity.Property(e => e.AccumReserve)
                .HasColumnType("double(15,4)")
                .HasColumnName("accum_reserve");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BrachUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("brach_uuid");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ExpectBudget)
                .HasComment("Kế hoạch vốn đầu tư (PlanBudget)")
                .HasColumnType("double(15,4)")
                .HasColumnName("expect_budget");
            entity.Property(e => e.ExpectEnd)
                .HasColumnType("timestamp")
                .HasColumnName("expect_end");
            entity.Property(e => e.ExpectStart)
                .HasColumnType("timestamp")
                .HasColumnName("expect_start");
            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Priority)
                .HasColumnType("tinyint(4)")
                .HasColumnName("priority");
            entity.Property(e => e.RealBudget)
                .HasComment("Tổng dự toán")
                .HasColumnType("double(15,4)")
                .HasColumnName("real_budget");
            entity.Property(e => e.RealEnd)
                .HasColumnType("timestamp")
                .HasColumnName("real_end");
            entity.Property(e => e.RealStart)
                .HasColumnType("timestamp")
                .HasColumnName("real_start");
            entity.Property(e => e.ReserveBudget)
                .HasColumnType("double(15,4)")
                .HasColumnName("reserve_budget");
            entity.Property(e => e.State)
                .HasComment("1 - chuẩn bị , 2 - thực hiện , 3 kết thúc ")
                .HasColumnType("tinyint(4)")
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.TotalInvest)
                .HasComment("Tổng mức đầu tư dự án (ProjectBudget)")
                .HasColumnType("double(15,4)")
                .HasColumnName("total_invest");
            entity.Property(e => e.Type)
                .HasColumnType("tinyint(4)")
                .HasColumnName("type");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Xaid)
                .HasMaxLength(5)
                .HasColumnName("xaid");

            entity.HasOne(d => d.BrachUu).WithMany(p => p.Project)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.BrachUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_brach_project_uuid");

            entity.HasOne(d => d.MaqhNavigation).WithMany(p => p.Project)
                .HasForeignKey(d => d.Maqh)
                .HasConstraintName("fk_maqh_project");

            entity.HasOne(d => d.MatpNavigation).WithMany(p => p.Project)
                .HasForeignKey(d => d.Matp)
                .HasConstraintName("fk_matp_project");

            entity.HasOne(d => d.Xa).WithMany(p => p.Project)
                .HasForeignKey(d => d.Xaid)
                .HasConstraintName("fk_xaid_project");
        });

        modelBuilder.Entity<ProjectAnnual>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project_annual");

            entity.HasIndex(e => e.ProjectUuid, "fk_project_pa_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.AccumAmount)
                .HasColumnType("double(15,4)")
                .HasColumnName("accum_amount");
            entity.Property(e => e.Budget)
                .HasColumnType("double(15,4)")
                .HasColumnName("budget");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("project_uuid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Year)
                .HasColumnType("int(4)")
                .HasColumnName("year");

            entity.HasOne(d => d.ProjectUu).WithMany(p => p.ProjectAnnual)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ProjectUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_pa_uuid");
        });

        modelBuilder.Entity<ProjectContractor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project_contractor");

            entity.HasIndex(e => e.ContractorUuid, "fk_contractor_uuid");

            entity.HasIndex(e => e.ProjectUuid, "fk_project_contractor_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.ContractAmount)
                .HasDefaultValueSql("'0.0000'")
                .HasComment("Giá trị hợp đồng")
                .HasColumnType("double(15,4)")
                .HasColumnName("contract_amount");
            entity.Property(e => e.ContractEndDate)
                .HasComment("Thời gian hết hạn hợp đồng")
                .HasColumnType("timestamp")
                .HasColumnName("contract_end_date");
            entity.Property(e => e.ContractorUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("contractor_uuid");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("project_uuid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.ContractorUu).WithMany(p => p.ProjectContractor)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ContractorUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contractor_uuid");

            entity.HasOne(d => d.ProjectUu).WithMany(p => p.ProjectContractor)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ProjectUuid)
                .HasConstraintName("fk_project_contractor_uuid");
        });

        modelBuilder.Entity<ProjectFund>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project_fund");

            entity.HasIndex(e => e.UserUuid, "fk_approval_uuid");

            entity.HasIndex(e => e.ProjectUuid, "fk_project_fund_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Approved)
                .HasDefaultValueSql("'0'")
                .HasComment("Trạng thái duyệt: 0 - chưa duyệt (đã báo cáo), 1 - đã duyệt, 2 - Bị từ chối")
                .HasColumnType("tinyint(4)")
                .HasColumnName("approved");
            entity.Property(e => e.Budget)
                .HasColumnType("double(15,4)")
                .HasColumnName("budget");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Feedback)
                .HasColumnType("text")
                .HasColumnName("feedback");
            entity.Property(e => e.Month)
                .HasColumnType("int(3)")
                .HasColumnName("month");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("project_uuid");
            entity.Property(e => e.RealRelease)
                .HasColumnType("timestamp")
                .HasColumnName("real_release");
            entity.Property(e => e.ReverseBudget)
                .HasDefaultValueSql("'0.0000'")
                .HasColumnType("double(15,4)")
                .HasColumnName("reverse_budget");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Year)
                .HasColumnType("int(4)")
                .HasColumnName("year");

            entity.HasOne(d => d.ProjectUu).WithMany(p => p.ProjectFund)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ProjectUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_fund_uuid");

            entity.HasOne(d => d.UserUu).WithMany(p => p.ProjectFund)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.UserUuid)
                .HasConstraintName("fk_user_uuid");
        });

        modelBuilder.Entity<Reports>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reports");

            entity.HasIndex(e => e.UpUuid, "fk_up_report_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Completed)
                .HasComment("Ngày gửi báo báo")
                .HasColumnType("timestamp")
                .HasColumnName("completed");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Month)
                .HasColumnType("int(3)")
                .HasColumnName("month");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.RejectedReason)
                .HasColumnType("text")
                .HasColumnName("rejected_reason");
            entity.Property(e => e.State)
                .HasDefaultValueSql("'2'")
                .HasComment("0 -Từ chối , 1 - Đã duyệt , 2 - Lên kế hoạch , 3 - Chờ duyệt , 4 - Đang thực hiện\r\n")
                .HasColumnType("tinyint(4)")
                .HasColumnName("state");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("up_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Year)
                .HasColumnType("int(4)")
                .HasColumnName("year");

            entity.HasOne(d => d.UpUu).WithMany(p => p.Reports)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.UpUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_up_report_uuid");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Sessions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.AccountUuid, "fk_ss_acc_uid_ref");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(12)")
                .HasColumnName("id");
            entity.Property(e => e.AccountUuid)
                .HasMaxLength(50)
                .HasColumnName("account_uuid");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("ip");
            entity.Property(e => e.Status)
                .HasComment("0: LogIn - 1: LogOut")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.TimeLogin)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("time_login");
            entity.Property(e => e.TimeLogout)
                .HasColumnType("timestamp")
                .HasColumnName("time_logout");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.AccountUu).WithMany(p => p.Sessions)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.AccountUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ss_acc_uid_ref");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("task");

            entity.HasIndex(e => e.ParentUuid, "fk_parent_task_uuid");

            entity.HasIndex(e => e.Type, "fk_taskcat_id");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Order)
                .HasColumnType("int(4)")
                .HasColumnName("order");
            entity.Property(e => e.ParentUuid)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("parent_uuid");
            entity.Property(e => e.Priority)
                .HasColumnType("tinyint(4)")
                .HasColumnName("priority");
            entity.Property(e => e.Stage)
                .HasDefaultValueSql("'1'")
                .HasComment("1-Chuẩn bị  , 2- Thực hiện , 3- kết thúc")
                .HasColumnType("tinyint(4)")
                .HasColumnName("stage");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(5)")
                .HasColumnName("type");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.ParentUu).WithMany(p => p.InverseParentUu)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ParentUuid)
                .HasConstraintName("fk_parent_task_uuid");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Task)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_taskcat_id");
        });

        modelBuilder.Entity<TaskCat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("task_cat");

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng , 2-khóa")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Maqh, "fk_maqh_user");

            entity.HasIndex(e => e.Matp, "fk_matp_user");

            entity.HasIndex(e => e.Xaid, "fk_xaid_user");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasColumnType("text")
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasComment("0-Nam , 1-Nữ , 2 - khác")
                .HasColumnType("tinyint(4)")
                .HasColumnName("gender");
            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0 - đang khóa, 1 - hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("timestamp")
                .HasColumnName("updated");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");
            entity.Property(e => e.Xaid)
                .HasMaxLength(5)
                .HasColumnName("xaid");

            entity.HasOne(d => d.MaqhNavigation).WithMany(p => p.User)
                .HasForeignKey(d => d.Maqh)
                .HasConstraintName("fk_maqh_user");

            entity.HasOne(d => d.MatpNavigation).WithMany(p => p.User)
                .HasForeignKey(d => d.Matp)
                .HasConstraintName("fk_matp_user");

            entity.HasOne(d => d.Xa).WithMany(p => p.User)
                .HasForeignKey(d => d.Xaid)
                .HasConstraintName("fk_xaid_user");
        });

        modelBuilder.Entity<UserProjects>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_projects");

            entity.HasIndex(e => e.ProjectUuid, "fk_project_up_uuid");

            entity.HasIndex(e => e.UserUuid, "fk_user_up_uuid");

            entity.HasIndex(e => e.Uuid, "uuid_unq").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(5)")
                .HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.ProjectUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("project_uuid");
            entity.Property(e => e.Role)
                .HasComment("1 - Nhân viên, 2 - PM")
                .HasColumnType("tinyint(4)")
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-không sử dụng , 1- sử dụng")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.ProjectUu).WithMany(p => p.UserProjects)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ProjectUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_project_up_uuid");

            entity.HasOne(d => d.UserUu).WithMany(p => p.UserProjects)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_up_uuid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
