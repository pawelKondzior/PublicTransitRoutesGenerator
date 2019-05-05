namespace Magisterka.Data.Access.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<LastPoints> LastPoints { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<SingleResult> SingleResult { get; set; }
        public virtual DbSet<StartStopPoints> StartStopPoints { get; set; }
        public virtual DbSet<TestToBeDone> TestToBeDone { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parameters>()
                .HasMany(e => e.Result)
                .WithRequired(e => e.Parameters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Result>()
                .HasMany(e => e.SingleResult)
                .WithRequired(e => e.Result)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StartStopPoints>()
                .HasMany(e => e.Result)
                .WithRequired(e => e.StartStopPoints)
                .WillCascadeOnDelete(false);
        }
    }
}
