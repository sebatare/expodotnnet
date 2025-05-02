using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proyectodotnet.Core.Models;

namespace proyectodotnet.Data
{
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
            builder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // Configuración de la entidad intermedia para Usuario - Equipo
            // Relación entre Usuario y Equipo (tabla intermedia UserTeam)
            builder.Entity<UserTeam>()
                .HasKey(ue => new { ue.UserId, ue.TeamId }); // Clave compuesta

            builder.Entity<UserTeam>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ue => ue.UserId);

            builder.Entity<UserTeam>()
                .HasOne(ue => ue.Team)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(ue => ue.TeamId); // Asegúrate de que el nombre coincida con la propiedad en UserTeam

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

            // Relación muchos a muchos: Usuario - Amistad
            //OJO CON LAS ELIMINACIONES EN CASCADA
            builder.Entity<Amistad>()
                .HasOne(a => a.Usuario1)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId1)
                .OnDelete(DeleteBehavior.Cascade); // Solo una relación con cascada

            builder.Entity<Amistad>()
                .HasOne(a => a.Usuario2)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId2)
                .OnDelete(DeleteBehavior.Restrict); // Restringir la otra relación    

            builder.Entity<Team>()
                .HasOne(e => e.Capitan)
                .WithMany()
                .HasForeignKey(e => e.CapitanId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar cascadas al eliminar un capitán


        }


        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Cancha> Cancha { get; set; }

        public DbSet<Sede> Sede { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

    }
}
