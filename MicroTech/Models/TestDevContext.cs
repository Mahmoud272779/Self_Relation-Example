using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MicroTech.Models;

public partial class TestDevContext : DbContext
{
    public TestDevContext()
    {
    }

    public TestDevContext(DbContextOptions<TestDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=192.168.1.253,63888;Initial Catalog=TestDev;user id=sa;password=XB@&)^@4354tfdsg;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccNumber);

            entity.Property(e => e.AccNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Acc_Number");
            entity.Property(e => e.AccParent)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ACC_Parent");
            entity.Property(e => e.Balance).HasColumnType("decimal(20, 9)");

            entity.HasOne(d => d.AccParentNavigation).WithMany(p => p.childAccounts)
                .HasForeignKey(d => d.AccParent)
                .HasConstraintName("FK_Accounts_Accounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
