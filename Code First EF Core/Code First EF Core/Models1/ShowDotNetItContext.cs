using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Code_First_EF_Core.Models1;

public partial class ShowDotNetItContext : DbContext
{
    public ShowDotNetItContext()
    {
    }

    public ShowDotNetItContext(DbContextOptions<ShowDotNetItContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditEmp> AuditEmps { get; set; }

    public virtual DbSet<EmpIndex> EmpIndices { get; set; }

    public virtual DbSet<EmpTrigger> EmpTriggers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCategory> EmployeeCategories { get; set; }

    public virtual DbSet<EmployeeCnd> EmployeeCnds { get; set; }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<EmployeeInd> EmployeeInds { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<VwProduct> VwProducts { get; set; }

    public virtual DbSet<VwTable> VwTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ShowDotNetIT;User id=bansi;Password=!!Forar2021!!;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditEmp>(entity =>
        {
            entity.ToTable("AuditEmp");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NewFirstName).HasMaxLength(50);
            entity.Property(e => e.NewLastName).HasMaxLength(50);
            entity.Property(e => e.OldFirstName).HasMaxLength(50);
            entity.Property(e => e.OldLastName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmpIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmpIndex");

            entity.HasIndex(e => e.Name, "IX_EmpIndex_ID").IsClustered();

            entity.HasIndex(e => e.Id, "IX_EmpIndex_NAME");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<EmpTrigger>(entity =>
        {
            entity.ToTable("EmpTrigger", tb =>
                {
                    tb.HasTrigger("EmpTriggerHistory");
                    tb.HasTrigger("tempEmpInsteadOfTrigger");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.PhoneNumber, "CK_Phone").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Grade).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.EmpCategory).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmpCategoryId)
                .HasConstraintName("FK_Employees_EmployeeCategory");
        });

        modelBuilder.Entity<EmployeeCategory>(entity =>
        {
            entity.HasKey(e => e.EmpCategoryId);

            entity.ToTable("EmployeeCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeCnd>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmployeeCND");

            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.GenderOfPerson).HasMaxLength(50);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.Property(e => e.Department)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.JobType).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeInd>(entity =>
        {
            entity.ToTable("EmployeeIND");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.GenderOfPerson).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User");
        });

        modelBuilder.Entity<VwProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_product");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VwTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_table");

            entity.Property(e => e.Department)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Grade).HasMaxLength(50);
            entity.Property(e => e.JobType).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Salary)
                .HasMaxLength(10)
                .IsFixedLength();
        });
        modelBuilder.HasSequence("EmpSeq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
