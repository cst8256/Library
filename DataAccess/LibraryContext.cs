using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Library.Models;

namespace Library.DataAccess;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<BookType> Types => Set<BookType>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dateOnlyConverter = new DateOnlyToStringConverter();

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.Property(book => book.Title).HasMaxLength(200).IsRequired();
            entity.Property(book => book.Author).HasMaxLength(120).IsRequired();
            entity.Property(book => book.PublicationDate)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("TEXT");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.ToTable("Loans");
            entity.Property(loan => loan.CheckedOutDate)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("TEXT");
            entity.Property(loan => loan.ReturnedDate)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("TEXT");

            entity.HasOne(loan => loan.Book)
                .WithMany(book => book.Loans)
                .HasForeignKey(loan => loan.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(loan => loan.BookId);
        });

        modelBuilder.Entity<BookType>(entity =>
        {
            entity.ToTable("Types");
            entity.Property(type => type.Name).HasMaxLength(50).IsRequired();
            entity.HasIndex(type => type.Name).IsUnique();
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");
            entity.Property(genre => genre.Name).HasMaxLength(60).IsRequired();
            entity.HasIndex(genre => genre.Name).IsUnique();
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.ToTable("BookGenres");
            entity.HasKey(bookGenre => new { bookGenre.BookId, bookGenre.GenreId });

            entity.HasOne(bookGenre => bookGenre.Book)
                .WithMany(book => book.BookGenres)
                .HasForeignKey(bookGenre => bookGenre.BookId);

            entity.HasOne(bookGenre => bookGenre.Genre)
                .WithMany(genre => genre.BookGenres)
                .HasForeignKey(bookGenre => bookGenre.GenreId);
        });
    }
}
