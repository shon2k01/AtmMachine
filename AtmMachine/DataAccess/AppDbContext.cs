using Microsoft.EntityFrameworkCore;

namespace AtmMachine;

public class AppDbContext : DbContext
{
    //Accounts table 
    public DbSet<Account> Accounts { get; set; }

    //Bills table
    public DbSet<Bill> Bills{get; set; }
    //constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    //configure database
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure the database connection
        string databaseFilePath = "AtmDb.db"; 
        optionsBuilder.UseSqlite($"Data Source={databaseFilePath};");
    }

    //create models
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Accounts table entity "Account"
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.AccountNumber);
            entity.Property(a => a.Balance).IsRequired().HasColumnType("decimal(18, 2)");
        });

        //Bills table entity "Bill"
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(b => b.Value);
            entity.Property(b => b.Amount).IsRequired();
        });
        
        
        base.OnModelCreating(modelBuilder);
    }


}