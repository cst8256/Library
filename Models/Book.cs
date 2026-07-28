namespace Library.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateOnly PublicationDate { get; set; }
    public bool IsAvailable { get; set; }
    public int BookTypeId { get; set; }
    public BookType? BookType { get; set; }
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}