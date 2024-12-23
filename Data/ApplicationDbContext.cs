using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
         
    }
    protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    // Índice único para el campo 'Email'
    builder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

    // Configuración del modelo ChatMessage
    builder.Entity<ChatMessage>()
        .HasOne(cm => cm.FromUser) // Relación con el usuario que envía
        .WithMany()
        .HasForeignKey(cm => cm.FromUserId)
        .OnDelete(DeleteBehavior.Restrict); // Evitar cascadas

    builder.Entity<ChatMessage>()
        .HasOne(cm => cm.ToUser) // Relación con el usuario que recibe
        .WithMany()
        .HasForeignKey(cm => cm.ToUserId)
        .OnDelete(DeleteBehavior.Restrict); // Evitar cascadas
}


    public DbSet<ChatMessage> ChatMessages { get; set; }

}

