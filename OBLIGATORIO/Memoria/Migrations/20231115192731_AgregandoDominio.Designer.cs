﻿// <auto-generated />
using System;
using Memoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Memoria.Migrations
{
    [DbContext(typeof(SQLContexto))]
    [Migration("20231115192731_AgregandoDominio")]
    partial class AgregandoDominio
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriaObjetivoDeGasto", b =>
                {
                    b.Property<int>("CategoriasId")
                        .HasColumnType("int");

                    b.Property<int>("ObjetivosDeGastoId")
                        .HasColumnType("int");

                    b.HasKey("CategoriasId", "ObjetivosDeGastoId");

                    b.HasIndex("ObjetivosDeGastoId");

                    b.ToTable("CategoriaObjetivoDeGasto");
                });

            modelBuilder.Entity("Dominio.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EspacioId")
                        .HasColumnType("int");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EspacioId");

                    b.HasIndex("UsuarioCreadorId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Dominio.Cuenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EspacioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaDeCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MonedaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropietarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EspacioId");

                    b.HasIndex("MonedaId");

                    b.HasIndex("PropietarioId");

                    b.ToTable("Cuenta");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Cuenta");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Dominio.Espacio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdministradorId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdministradorId");

                    b.ToTable("Espacios");
                });

            modelBuilder.Entity("Dominio.Moneda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SimboloMonetario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Monedas");
                });

            modelBuilder.Entity("Dominio.ObjetivoDeGasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EspacioId")
                        .HasColumnType("int");

                    b.Property<float>("MontoMaximo")
                        .HasColumnType("real");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EspacioId");

                    b.HasIndex("UsuarioCreadorId");

                    b.ToTable("ObjetivoDeGastos");
                });

            modelBuilder.Entity("Dominio.TipoDeCambio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EspacioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MonedaId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<float?>("ValorDeLaMoneda")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("EspacioId");

                    b.HasIndex("MonedaId");

                    b.HasIndex("UsuarioCreadorId");

                    b.ToTable("TiposDeCambios");
                });

            modelBuilder.Entity("Dominio.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int?>("CuentaId")
                        .HasColumnType("int");

                    b.Property<int>("EspacioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MonedaId")
                        .HasColumnType("int");

                    b.Property<float?>("Monto")
                        .HasColumnType("real");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CuentaId");

                    b.HasIndex("EspacioId");

                    b.HasIndex("MonedaId");

                    b.ToTable("Transacciones");
                });

            modelBuilder.Entity("Dominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("EspacioUsuario", b =>
                {
                    b.Property<int>("EspaciosId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipantesId")
                        .HasColumnType("int");

                    b.HasKey("EspaciosId", "ParticipantesId");

                    b.HasIndex("ParticipantesId");

                    b.ToTable("EspacioUsuario");
                });

            modelBuilder.Entity("Dominio.Monetaria", b =>
                {
                    b.HasBaseType("Dominio.Cuenta");

                    b.Property<float?>("Monto")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("Monetaria");
                });

            modelBuilder.Entity("Dominio.TarjetaDeCredito", b =>
                {
                    b.HasBaseType("Dominio.Cuenta");

                    b.Property<string>("BancoEmisor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("CreditoDisponible")
                        .HasColumnType("real");

                    b.Property<DateTime>("FechaDeCierre")
                        .HasColumnType("datetime2");

                    b.Property<string>("UltimosCuatroDigitos")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TarjetaDeCredito");
                });

            modelBuilder.Entity("CategoriaObjetivoDeGasto", b =>
                {
                    b.HasOne("Dominio.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.ObjetivoDeGasto", null)
                        .WithMany()
                        .HasForeignKey("ObjetivosDeGastoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Categoria", b =>
                {
                    b.HasOne("Dominio.Espacio", "Espacio")
                        .WithMany()
                        .HasForeignKey("EspacioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Usuario", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Espacio");

                    b.Navigation("UsuarioCreador");
                });

            modelBuilder.Entity("Dominio.Cuenta", b =>
                {
                    b.HasOne("Dominio.Espacio", "Espacio")
                        .WithMany()
                        .HasForeignKey("EspacioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Moneda", "Moneda")
                        .WithMany()
                        .HasForeignKey("MonedaId");

                    b.HasOne("Dominio.Usuario", "Propietario")
                        .WithMany()
                        .HasForeignKey("PropietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Espacio");

                    b.Navigation("Moneda");

                    b.Navigation("Propietario");
                });

            modelBuilder.Entity("Dominio.Espacio", b =>
                {
                    b.HasOne("Dominio.Usuario", "Administrador")
                        .WithMany("EspaciosQueAdministra")
                        .HasForeignKey("AdministradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrador");
                });

            modelBuilder.Entity("Dominio.ObjetivoDeGasto", b =>
                {
                    b.HasOne("Dominio.Espacio", "Espacio")
                        .WithMany()
                        .HasForeignKey("EspacioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Usuario", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Espacio");

                    b.Navigation("UsuarioCreador");
                });

            modelBuilder.Entity("Dominio.TipoDeCambio", b =>
                {
                    b.HasOne("Dominio.Espacio", "Espacio")
                        .WithMany()
                        .HasForeignKey("EspacioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Moneda", "Moneda")
                        .WithMany()
                        .HasForeignKey("MonedaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Usuario", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Espacio");

                    b.Navigation("Moneda");

                    b.Navigation("UsuarioCreador");
                });

            modelBuilder.Entity("Dominio.Transaccion", b =>
                {
                    b.HasOne("Dominio.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.HasOne("Dominio.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("CuentaId");

                    b.HasOne("Dominio.Espacio", "Espacio")
                        .WithMany()
                        .HasForeignKey("EspacioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Moneda", "Moneda")
                        .WithMany()
                        .HasForeignKey("MonedaId");

                    b.Navigation("Categoria");

                    b.Navigation("Cuenta");

                    b.Navigation("Espacio");

                    b.Navigation("Moneda");
                });

            modelBuilder.Entity("EspacioUsuario", b =>
                {
                    b.HasOne("Dominio.Espacio", null)
                        .WithMany()
                        .HasForeignKey("EspaciosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Usuario", null)
                        .WithMany()
                        .HasForeignKey("ParticipantesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Usuario", b =>
                {
                    b.Navigation("EspaciosQueAdministra");
                });
#pragma warning restore 612, 618
        }
    }
}
