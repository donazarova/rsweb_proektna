using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCTurizam.Areas.Identity.Data;
using MVCTurizam.Models;

namespace MVCTurizam.Data
{
    public class MVCTurizamContext : IdentityDbContext<MVCTurizamUser>
    {
        public MVCTurizamContext (DbContextOptions<MVCTurizamContext> options)
            : base(options)
        {
        }

        public DbSet<MVCTurizam.Models.Destinacija> Destinacija { get; set; } = default!;

        public DbSet<MVCTurizam.Models.Klient> Klient { get; set; }

        public DbSet<MVCTurizam.Models.Patuvanje> Patuvanje { get; set; }

        public DbSet<MVCTurizam.Models.Vodic> Vodic { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
       /* {
            builder.Entity<Patuvanje>()
            .HasOne<Klient>(p => p.Klient)
            .WithMany(p => p.Patuvanje)
            .HasForeignKey(p => p.KlientId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Patuvanje>()
            .HasOne<Destinacija>(p => p.Destinacija)
            .WithMany(p => p.Patuvanje)
            .HasForeignKey(p => p.DestinacijaId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Destinacija>()
            .HasOne<Vodic>(p => p.Vodic)
            .WithMany(p => p.Destinacijas)
            .HasForeignKey(p => p.VodicId);
            //.HasPrincipalKey(p => p.Id);
        } */

    }
}
