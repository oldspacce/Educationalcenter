using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Educationalcenter.Models;

namespace Educationalcenter;

public partial class EducationalcenterContext : DbContext
{
    public EducationalcenterContext()
    {
    }

    public EducationalcenterContext(DbContextOptions<EducationalcenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Dateschedule> Dateschedules { get; set; }

    public virtual DbSet<Grouplesson> Grouplessons { get; set; }

    public virtual DbSet<Grouplessonclient> Grouplessonclients { get; set; }

    public virtual DbSet<Groupschedule> Groupschedules { get; set; }

    public virtual DbSet<Individualeschedule> Individualeschedules { get; set; }

    public virtual DbSet<Individuallesson> Individuallessons { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Scheduledate> Scheduledates { get; set; }

    public virtual DbSet<Schedulelesson> Schedulelessons { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Teachersubject> Teachersubjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=educationalcenter;Username=postgres;Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("pk_client");

            entity.ToTable("client", "education");

            entity.Property(e => e.Clientid)
                .ValueGeneratedNever()
                .HasColumnName("clientid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.Surname).HasColumnName("surname");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Dateschedule>(entity =>
        {
            entity.HasKey(e => e.Datescheduleid).HasName("pk_dateschedule");

            entity.ToTable("dateschedule", "education");

            entity.Property(e => e.Datescheduleid)
                .ValueGeneratedNever()
                .HasColumnName("datescheduleid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Dateschedules)
                .HasForeignKey(d => d.Teacherid)
                .HasConstraintName("fk_teacherid");
        });

        modelBuilder.Entity<Grouplesson>(entity =>
        {
            entity.HasKey(e => e.Grouplessonid).HasName("pk_gropulesson");

            entity.ToTable("grouplesson", "education");

            entity.Property(e => e.Grouplessonid)
                .ValueGeneratedNever()
                .HasColumnName("grouplessonid");
            entity.Property(e => e.Clientamount).HasColumnName("clientamount");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.Subjectid).HasColumnName("subjectid");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grouplessons)
                .HasForeignKey(d => d.Subjectid)
                .HasConstraintName("fk_subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grouplessons)
                .HasForeignKey(d => d.Teacherid)
                .HasConstraintName("fk_teacherid");
        });

        modelBuilder.Entity<Grouplessonclient>(entity =>
        {
            entity.HasKey(e => e.Grouplessonclientid).HasName("pk_grouplessonclient");

            entity.ToTable("grouplessonclient", "education");

            entity.Property(e => e.Grouplessonclientid)
                .ValueGeneratedNever()
                .HasColumnName("grouplessonclientid");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Grouplessonid).HasColumnName("grouplessonid");

            entity.HasOne(d => d.Client).WithMany(p => p.Grouplessonclients)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clientid");

            entity.HasOne(d => d.Grouplesson).WithMany(p => p.Grouplessonclients)
                .HasForeignKey(d => d.Grouplessonid)
                .HasConstraintName("fk_grouplessonid");
        });

        modelBuilder.Entity<Groupschedule>(entity =>
        {
            entity.HasKey(e => e.Groupscheduleid).HasName("pk_groupschedule");

            entity.ToTable("groupschedule", "education");

            entity.Property(e => e.Groupscheduleid)
                .ValueGeneratedNever()
                .HasColumnName("groupscheduleid");
            entity.Property(e => e.Grouplessonid).HasColumnName("grouplessonid");
            entity.Property(e => e.Schedulelessonid).HasColumnName("schedulelessonid");

            entity.HasOne(d => d.Grouplesson).WithMany(p => p.Groupschedules)
                .HasForeignKey(d => d.Grouplessonid)
                .HasConstraintName("fk_grouplesson");

            entity.HasOne(d => d.Schedulelesson).WithMany(p => p.Groupschedules)
                .HasForeignKey(d => d.Schedulelessonid)
                .HasConstraintName("fk_schedulelesson");
        });

        modelBuilder.Entity<Individualeschedule>(entity =>
        {
            entity.HasKey(e => e.Individualschedule).HasName("pk_individualschedule");

            entity.ToTable("individualeschedule", "education");

            entity.Property(e => e.Individualschedule)
                .ValueGeneratedNever()
                .HasColumnName("individualschedule");
            entity.Property(e => e.Individuallessonid).HasColumnName("individuallessonid");
            entity.Property(e => e.Schedulelessonid).HasColumnName("schedulelessonid");

            entity.HasOne(d => d.Individuallesson).WithMany(p => p.Individualeschedules)
                .HasForeignKey(d => d.Individuallessonid)
                .HasConstraintName("fk_individuallesson");

            entity.HasOne(d => d.Schedulelesson).WithMany(p => p.Individualeschedules)
                .HasForeignKey(d => d.Schedulelessonid)
                .HasConstraintName("fk_schedulelesson");
        });

        modelBuilder.Entity<Individuallesson>(entity =>
        {
            entity.HasKey(e => e.Individuallessonid).HasName("pk_individuallesson");

            entity.ToTable("individuallesson", "education");

            entity.Property(e => e.Individuallessonid)
                .ValueGeneratedNever()
                .HasColumnName("individuallessonid");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");

            entity.HasOne(d => d.Client).WithMany(p => p.Individuallessons)
                .HasForeignKey(d => d.Clientid)
                .HasConstraintName("fk_clientid");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Individuallessons)
                .HasForeignKey(d => d.Teacherid)
                .HasConstraintName("fk_teacherid");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Scheduleid).HasName("pk_schedule");

            entity.ToTable("schedule", "education");

            entity.Property(e => e.Scheduleid)
                .ValueGeneratedNever()
                .HasColumnName("scheduleid");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<Scheduledate>(entity =>
        {
            entity.HasKey(e => e.Scheduledateid).HasName("pk_scheduledate");

            entity.ToTable("scheduledate", "education");

            entity.Property(e => e.Scheduledateid)
                .ValueGeneratedNever()
                .HasColumnName("scheduledateid");
            entity.Property(e => e.Busy)
            .HasDefaultValueSql("false")
            .HasColumnName("busy");
            entity.Property(e => e.Datescheduleid).HasColumnName("datescheduleid");
            entity.Property(e => e.Scheduleid).HasColumnName("scheduleid");

            entity.HasOne(d => d.Dateschedule).WithMany(p => p.Scheduledates)
                .HasForeignKey(d => d.Datescheduleid)
                .HasConstraintName("fk_dateschedule");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Scheduledates)
                .HasForeignKey(d => d.Scheduleid)
                .HasConstraintName("fk_schedule");
        });

        modelBuilder.Entity<Schedulelesson>(entity =>
        {
            entity.HasKey(e => e.Schedulelessonid).HasName("pk_schedulelesson");

            entity.ToTable("schedulelesson", "education");

            entity.Property(e => e.Schedulelessonid)
                .ValueGeneratedNever()
                .HasColumnName("schedulelessonid");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Subjectid).HasName("pk_subject");

            entity.ToTable("subject", "education");

            entity.Property(e => e.Subjectid)
                .ValueGeneratedNever()
                .HasColumnName("subjectid");
            entity.Property(e => e.Subjectname).HasColumnName("subjectname");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Teacherid).HasName("pk_teacher");

            entity.ToTable("teacher", "education");

            entity.Property(e => e.Teacherid)
                .ValueGeneratedNever()
                .HasColumnName("teacherid");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.Surname).HasColumnName("surname");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Teachersubject>(entity =>
        {
            entity.HasKey(e => e.Teachersubjectid).HasName("pk_teachersubject");

            entity.ToTable("teachersubject", "education");

            entity.Property(e => e.Teachersubjectid)
                .ValueGeneratedNever()
                .HasColumnName("teachersubjectid");
            entity.Property(e => e.Subjectid).HasColumnName("subjectid");
            entity.Property(e => e.Teacherid).HasColumnName("teacherid");

            entity.HasOne(d => d.Subject).WithMany(p => p.Teachersubjects)
                .HasForeignKey(d => d.Subjectid)
                .HasConstraintName("fk_subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Teachersubjects)
                .HasForeignKey(d => d.Teacherid)
                .HasConstraintName("fk_teacher");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("pk_user");

            entity.ToTable("user", "education");

            entity.Property(e => e.Userid)
                .ValueGeneratedNever()
                .HasColumnName("userid");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
