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

        builder.Entity<Address>()
       .HasOne(a => a.User)
       .WithMany(u => u.Addresses)
       .HasForeignKey(a => a.UserId);

        builder.Entity<Sede>()
        .HasOne(s => s.Address)
        .WithOne(a => a.Sede)
        .HasForeignKey<Sede>(s => s.AddressId)
        .IsRequired(false); // Permite que sea opcional

        builder.Entity<Cancha>()
        .HasOne(p => p.Sede)
        .WithMany(c => c.Canchas)
        .HasForeignKey(p => p.SedeId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);//Esto me permite eliminar sedes sin eliminar las canchas relacionadas, dejandonlas sin SedeId.


    }


    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Cancha> Cancha { get; set; }

    public DbSet<Sede> Sede { get; set; }

}

