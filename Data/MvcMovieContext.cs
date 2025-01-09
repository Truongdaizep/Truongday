using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETCORE.Models;

public class MvcMovieContext : DbContext
{
    public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
        : base(options)
    {
    }

    public DbSet<NETCORE.Models.Hoaqua> Hoaqua { get; set; } = default!;
    public DbSet<NETCORE.Models.CartItem> CartItem { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hoaqua>().ToTable("Hoaqua");
        modelBuilder.Entity<CartItem>().ToTable("CartItem");
    }
}
