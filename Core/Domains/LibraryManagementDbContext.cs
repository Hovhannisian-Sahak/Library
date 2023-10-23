using System;
using System.Collections.Generic;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains;

public partial class LibraryManagementDbContext : DbContext
{
    public LibraryManagementDbContext()
    {
    }

    public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePassword> EmployeePasswords { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentPassword> StudentPasswords { get; set; }
    public virtual DbSet<FilteredBooks> FilteredBooksResult { get; set; }
    public virtual DbSet<FilteredEmployees> FilteredEmployeesResult { get; set; }
    public virtual DbSet<StudentsMostReadBooks> StudentsMostReadBooksResult { get; set; }
    public virtual DbSet<StudentsWithLateReturn> StudentsWithLateReturnResult { get; set; }
    public virtual DbSet<EmployeesWithBooks> EmployeesWithBooksResult { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SAHAK\\SQLEXPRESS;Database=LibraryManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3213E83F60EF7BCD");

            entity.ToTable("Book");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PublishDate).HasColumnType("date");
            entity.Property(e => e.UpdatedDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FD783823C");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.HireDate).HasColumnType("date");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasDefaultValueSql("((2))");

            entity.HasOne(d => d.Profession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProfessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Profes__49C3F6B7");
        });

        modelBuilder.Entity<EmployeePassword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FD92AF39C");

            entity.ToTable("EmployeePassword");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePasswords)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__Emplo__07C12930");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faculty__3213E83F0314E04C");

            entity.ToTable("Faculty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3213E83FC3F20DFF");

            entity.ToTable("History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.BorrowDate).HasColumnType("date");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.IsReturned).HasColumnName("isReturned");
            entity.Property(e => e.ReturnDate).HasColumnType("date");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Book).WithMany(p => p.Histories)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__History__BookID__59FA5E80");

            entity.HasOne(d => d.Employee).WithMany(p => p.Histories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__History__Employe__5AEE82B9");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Professi__3213E83FAC7476EC");

            entity.ToTable("Profession");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProfessionName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83F0B6DD72F");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83FAB888923");

            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcademicYear)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__Facluty__4316F928");
        });

        modelBuilder.Entity<StudentPassword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentP__3213E83F741FAE9D");

            entity.ToTable("StudentPassword");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.StudentPasswords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentPa__Stude__03F0984C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
