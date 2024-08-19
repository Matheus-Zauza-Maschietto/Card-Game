using System.Collections.Immutable;
using app.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace app.Context;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardColor> CardColors { get; set; }
    public DbSet<CardPrinting> CardPrintings { get; set; }
    public DbSet<CardSubType> CardSubTypes { get; set; }
    public DbSet<CardType> CardTypes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<ForeignName> ForeignNames { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Legality> Legalities { get; set; }
    public DbSet<Printing> Printings { get; set; }
    public DbSet<SubType> SubTypes { get; set; }
    public DbSet<Models.Type> Types { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<DeckCard> DeckCards { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Deck>()
        .HasOne(e => e.User)
        .WithMany(e => e.Decks);

        builder.Entity<Deck>()
        .HasMany(e => e.Cards)
        .WithMany(e => e.Decks)
        .UsingEntity<DeckCard>();

        // builder.Entity<Deck>()
        // .HasOne(e => e.CommanderCard)
        // .WithOne();
        
        builder.Entity<Card>()
        .HasMany(e => e.Colors)
        .WithMany(e => e.Cards)
        .UsingEntity<CardColor>();
        
        builder.Entity<Card>()
        .HasMany(e => e.Types)
        .WithMany(e => e.Cards)
        .UsingEntity<CardType>();

        builder.Entity<Card>()
        .HasMany(e => e.Subtypes)
        .WithMany(e => e.Cards)
        .UsingEntity<CardSubType>();

        builder.Entity<Card>()
        .HasMany(e => e.Printings)
        .WithMany(e => e.Cards)
        .UsingEntity<CardPrinting>();

        builder.Entity<Card>()
        .HasMany(e => e.Legalities)
        .WithOne(e => e.Card);

        builder.Entity<Card>()
        .HasMany(e => e.ForeignNames)
        .WithOne(e => e.Card);
        
        builder.Entity<Color>()
        .HasMany(e => e.Cards)
        .WithMany(e => e.Colors)
        .UsingEntity<CardColor>();

        builder.Entity<Models.Type>()
        .HasMany(e => e.Cards)
        .WithMany(e => e.Types)
        .UsingEntity<CardType>();

        builder.Entity<SubType>()
        .HasMany(e => e.Cards)
        .WithMany(e => e.Subtypes)
        .UsingEntity<CardSubType>();

        builder.Entity<Printing>()
        .HasMany(e => e.Cards)
        .WithMany(e => e.Printings)
        .UsingEntity<CardPrinting>();

        builder.Entity<ForeignName>()
        .HasOne(e => e.Card)
        .WithMany(e => e.ForeignNames);

        builder.Entity<Legality>()
        .HasOne(e => e.Card)
        .WithMany(e => e.Legalities);

        builder.Entity<Language>()
        .HasMany(e => e.Users)
        .WithOne(e => e.Language);

        builder.Entity<User>()
        .HasOne(e => e.Language)
        .WithMany(e => e.Users);

        builder.Entity<User>()
        .HasMany(e => e.Decks)
        .WithOne(e => e.User);


        base.OnModelCreating(builder);
    }
}
