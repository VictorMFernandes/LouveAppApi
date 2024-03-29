﻿// <auto-generated />
using System;
using LouveApp.Dal.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LouveApp.Dal.Migrations
{
    [DbContext(typeof(BancoContexto))]
    partial class BancoContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Dispositivo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<string>("UsuarioId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("tb_dispositivo");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Escala", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<string>("MinisterioId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MinisterioId");

                    b.ToTable("tb_escala");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Instrumento", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("tb_instrumento");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.EscalaMusica", b =>
                {
                    b.Property<string>("EscalaId");

                    b.Property<string>("MusicaId");

                    b.HasKey("EscalaId", "MusicaId");

                    b.HasIndex("MusicaId");

                    b.ToTable("tb_escala_musica");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioEscala", b =>
                {
                    b.Property<string>("UsuarioId");

                    b.Property<string>("EscalaId");

                    b.HasKey("UsuarioId", "EscalaId");

                    b.HasIndex("EscalaId");

                    b.ToTable("tb_usuario_escala");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioEscalaInstrumento", b =>
                {
                    b.Property<string>("UsuarioEscalaId");

                    b.Property<string>("InstrumentoId");

                    b.HasKey("UsuarioEscalaId", "InstrumentoId");

                    b.ToTable("tb_usuario_escala_instrumento");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioInstrumento", b =>
                {
                    b.Property<string>("UsuarioId");

                    b.Property<string>("InstrumentoId");

                    b.HasKey("UsuarioId", "InstrumentoId");

                    b.HasIndex("InstrumentoId");

                    b.ToTable("tb_usuario_instrumento");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioMinisterio", b =>
                {
                    b.Property<string>("UsuarioId");

                    b.Property<string>("MinisterioId");

                    b.Property<bool>("Administrador");

                    b.Property<DateTime>("DtIngresso");

                    b.HasKey("UsuarioId", "MinisterioId");

                    b.HasIndex("MinisterioId");

                    b.ToTable("tb_usuario_ministerio");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Ministerio", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LinkConvite");

                    b.Property<bool>("LinkConviteAtivado");

                    b.HasKey("Id");

                    b.HasIndex("LinkConvite");

                    b.ToTable("tb_ministerio");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Musica", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Bpm")
                        .HasMaxLength(3);

                    b.Property<string>("Classificacao")
                        .HasMaxLength(60);

                    b.Property<string>("MinisterioId")
                        .IsRequired();

                    b.Property<string>("Tom")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("MinisterioId");

                    b.ToTable("tb_musica");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DtCriacao");

                    b.Property<DateTime>("DtUltimaAtividade");

                    b.HasKey("Id");

                    b.ToTable("tb_usuario");
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Dispositivo", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Usuario", "Usuario")
                        .WithMany("Dispositivos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<string>("DispositivoId");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnName("Nome")
                                .HasMaxLength(60);

                            b1.HasKey("DispositivoId");

                            b1.ToTable("tb_dispositivo");

                            b1.HasOne("LouveApp.Dominio.Entidades.Dispositivo")
                                .WithOne("Nome")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Nome", "DispositivoId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Escala", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Ministerio", "Ministerio")
                        .WithMany("Escalas")
                        .HasForeignKey("MinisterioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Instrumento", b =>
                {
                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<string>("InstrumentoId");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnName("Nome")
                                .HasMaxLength(60);

                            b1.HasKey("InstrumentoId");

                            b1.HasIndex("Texto")
                                .IsUnique();

                            b1.ToTable("tb_instrumento");

                            b1.HasOne("LouveApp.Dominio.Entidades.Instrumento")
                                .WithOne("Nome")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Nome", "InstrumentoId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.EscalaMusica", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Escala", "Escala")
                        .WithMany("Musicas")
                        .HasForeignKey("EscalaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LouveApp.Dominio.Entidades.Musica", "Musica")
                        .WithMany("Escalas")
                        .HasForeignKey("MusicaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioEscala", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Escala", "Escala")
                        .WithMany("Usuarios")
                        .HasForeignKey("EscalaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LouveApp.Dominio.Entidades.Usuario", "Usuario")
                        .WithMany("Escalas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioInstrumento", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Instrumento", "Instrumento")
                        .WithMany("Usuarios")
                        .HasForeignKey("InstrumentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LouveApp.Dominio.Entidades.Usuario", "Usuario")
                        .WithMany("Instrumentos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Juncao.UsuarioMinisterio", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Ministerio", "Ministerio")
                        .WithMany("Usuarios")
                        .HasForeignKey("MinisterioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LouveApp.Dominio.Entidades.Usuario", "Usuario")
                        .WithMany("Ministerios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Ministerio", b =>
                {
                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Foto", "Foto", b1 =>
                        {
                            b1.Property<string>("MinisterioId");

                            b1.Property<string>("IdPublico")
                                .HasColumnName("FotoIdPublico")
                                .HasMaxLength(25);

                            b1.Property<string>("Url")
                                .HasColumnName("FotoUrl")
                                .HasMaxLength(160);

                            b1.HasKey("MinisterioId");

                            b1.ToTable("tb_ministerio");

                            b1.HasOne("LouveApp.Dominio.Entidades.Ministerio")
                                .WithOne("Foto")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Foto", "MinisterioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<string>("MinisterioId");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnName("Nome")
                                .HasMaxLength(60);

                            b1.HasKey("MinisterioId");

                            b1.ToTable("tb_ministerio");

                            b1.HasOne("LouveApp.Dominio.Entidades.Ministerio")
                                .WithOne("Nome")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Nome", "MinisterioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Musica", b =>
                {
                    b.HasOne("LouveApp.Dominio.Entidades.Ministerio", "Ministerio")
                        .WithMany("Musicas")
                        .HasForeignKey("MinisterioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Link", "Cifra", b1 =>
                        {
                            b1.Property<string>("MusicaId");

                            b1.Property<string>("Url")
                                .HasColumnName("Cifra")
                                .HasMaxLength(200);

                            b1.HasKey("MusicaId");

                            b1.ToTable("tb_musica");

                            b1.HasOne("LouveApp.Dominio.Entidades.Musica")
                                .WithOne("Cifra")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Link", "MusicaId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Link", "Letra", b1 =>
                        {
                            b1.Property<string>("MusicaId");

                            b1.Property<string>("Url")
                                .HasColumnName("Letra")
                                .HasMaxLength(200);

                            b1.HasKey("MusicaId");

                            b1.ToTable("tb_musica");

                            b1.HasOne("LouveApp.Dominio.Entidades.Musica")
                                .WithOne("Letra")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Link", "MusicaId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Link", "Video", b1 =>
                        {
                            b1.Property<string>("MusicaId");

                            b1.Property<string>("Url")
                                .HasColumnName("Video")
                                .HasMaxLength(200);

                            b1.HasKey("MusicaId");

                            b1.ToTable("tb_musica");

                            b1.HasOne("LouveApp.Dominio.Entidades.Musica")
                                .WithOne("Video")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Link", "MusicaId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Nome", "Artista", b1 =>
                        {
                            b1.Property<string>("MusicaId");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnName("Artista")
                                .HasMaxLength(60);

                            b1.HasKey("MusicaId");

                            b1.ToTable("tb_musica");

                            b1.HasOne("LouveApp.Dominio.Entidades.Musica")
                                .WithOne("Artista")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Nome", "MusicaId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<string>("MusicaId");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnName("Nome")
                                .HasMaxLength(60);

                            b1.HasKey("MusicaId");

                            b1.ToTable("tb_musica");

                            b1.HasOne("LouveApp.Dominio.Entidades.Musica")
                                .WithOne("Nome")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Nome", "MusicaId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("LouveApp.Dominio.Entidades.Usuario", b =>
                {
                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Autenticacao", "Autenticacao", b1 =>
                        {
                            b1.Property<string>("UsuarioId");

                            b1.Property<bool>("Ativo")
                                .HasColumnName("Ativo");

                            b1.Property<string>("Login")
                                .IsRequired()
                                .HasColumnName("Login")
                                .HasMaxLength(100);

                            b1.Property<string>("Senha")
                                .IsRequired()
                                .HasColumnName("Senha")
                                .IsFixedLength(true)
                                .HasMaxLength(32);

                            b1.HasKey("UsuarioId");

                            b1.HasIndex("Login")
                                .IsUnique();

                            b1.ToTable("tb_usuario");

                            b1.HasOne("LouveApp.Dominio.Entidades.Usuario")
                                .WithOne("Autenticacao")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Autenticacao", "UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<string>("UsuarioId");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasMaxLength(160);

                            b1.HasKey("UsuarioId");

                            b1.ToTable("tb_usuario");

                            b1.HasOne("LouveApp.Dominio.Entidades.Usuario")
                                .WithOne("Email")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Email", "UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Foto", "Foto", b1 =>
                        {
                            b1.Property<string>("UsuarioId");

                            b1.Property<string>("IdPublico")
                                .HasColumnName("FotoIdPublico")
                                .HasMaxLength(25);

                            b1.Property<string>("Url")
                                .HasColumnName("FotoUrl")
                                .HasMaxLength(160);

                            b1.HasKey("UsuarioId");

                            b1.ToTable("tb_usuario");

                            b1.HasOne("LouveApp.Dominio.Entidades.Usuario")
                                .WithOne("Foto")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Foto", "UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("LouveApp.Dominio.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<string>("UsuarioId");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnName("Nome")
                                .HasMaxLength(60);

                            b1.HasKey("UsuarioId");

                            b1.ToTable("tb_usuario");

                            b1.HasOne("LouveApp.Dominio.Entidades.Usuario")
                                .WithOne("Nome")
                                .HasForeignKey("LouveApp.Dominio.ValueObjects.Nome", "UsuarioId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
