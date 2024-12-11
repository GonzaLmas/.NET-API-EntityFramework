using BalearesChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace BalearesChallenge.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ContactoModel> Contactos { get; set; }
        public DbSet<TransporteModel> Transportes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioModel>(table =>
            {
                table.HasKey(col => col.IdUsuario);
                table.Property(col => col.IdUsuario).UseIdentityColumn().ValueGeneratedOnAdd();

                table.Property(col => col.Correo).IsRequired().HasMaxLength(50);
                table.Property(col => col.Password).IsRequired().HasMaxLength(100);
                table.Property(col => col.Nombre).IsRequired().HasMaxLength(25);
                table.Property(col => col.Apellido).IsRequired().HasMaxLength(25);
                table.Property(col => col.Salt).HasMaxLength(100);
            });

            modelBuilder.Entity<UsuarioModel>().ToTable("Usuario");


            modelBuilder.Entity<ContactoModel>(table =>
            {
                table.HasKey(col => col.IdContacto);
                table.Property(col => col.IdContacto).UseIdentityColumn().ValueGeneratedOnAdd();

                table.Property(col => col.Nombre).HasMaxLength(50);
                table.Property(col => col.Empresa).HasMaxLength(50);
                table.Property(col => col.Email).HasMaxLength(50);
                table.Property(col => col.FechaNacimiento).HasColumnType("datetime");
                table.Property(col => col.Telefono).HasColumnType("int");
                table.Property(col => col.Direccion).HasMaxLength(50);
                table.HasOne(col => col.Transporte).WithOne().HasForeignKey<ContactoModel>(col => col.IdTransporte)
                                                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ContactoModel>().ToTable("Contacto");


            modelBuilder.Entity<TransporteModel>(table =>
            {
                table.HasKey(col => col.IdTransporte);
                table.Property(col => col.IdTransporte).UseIdentityColumn().ValueGeneratedOnAdd();

                table.Property(col => col.TipoTransporte).HasMaxLength(25);
            });

            modelBuilder.Entity<TransporteModel>().ToTable("Transporte");
        }
    }
}
