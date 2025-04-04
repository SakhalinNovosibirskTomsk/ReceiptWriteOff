using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BookInstance> BookInstances { get; set; }
    public DbSet<ReceiptFact> ReceiptFacts { get; set; }
    public DbSet<WriteOffFact> WriteOffFacts { get; set; }
    public DbSet<WriteOffReason> WriteOffReasons { get; set; }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("Books");
        
        modelBuilder.Entity<Book>().Property(bi => bi.Title).HasMaxLength(1000);
        modelBuilder.Entity<Book>().Property(bi => bi.Author).HasMaxLength(500);
        
        modelBuilder.Entity<BookInstance>().Property(bi => bi.InventoryNumber).HasMaxLength(100);
        modelBuilder.Entity<BookInstance>()
            .HasOne(bi => bi.Book)
            .WithMany(b => b.BookInstances)
            .HasForeignKey(bi => bi.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<BookInstance>()
            .HasOne(bi => bi.ReceiptFact)
            .WithOne(rf => rf.BookInstance)
            .HasForeignKey<BookInstance>(bi => bi.ReceipdFactId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceiptFact>()
            .HasOne(rf => rf.BookInstance)
            .WithOne(bi => bi.ReceiptFact)
            .HasForeignKey<ReceiptFact>(receiptFact => receiptFact.BookInstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WriteOffFact>()
            .HasOne(wf => wf.BookInstance)
            .WithOne(bi => bi.WriteOffFact)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<WriteOffFact>()
            .HasOne(wf => wf.WriteOffReason)
            .WithMany(wr => wr.WriteOffFacts)
            .HasForeignKey(wf => wf.WriteOffReasonId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<WriteOffReason>().Property(bi => bi.Description).HasMaxLength(500);
    }

    public IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class => 
        new DdSetWrapper<TEntity>(base.Set<TEntity>());

    public IDbSet<TEntity> GetDbSet<TEntity>(string name) where TEntity : class =>
        new DdSetWrapper<TEntity>(base.Set<TEntity>(name));

    public IEntityEntry<TEntity> GetEntry<TEntity>(TEntity entity) where TEntity : class =>
        new EntityEntryWrapper<TEntity>(base.Entry(entity));
}