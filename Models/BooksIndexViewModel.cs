namespace Library.Models;

public class BooksIndexViewModel
{
    public List<Book> Books { get; set; } = new();
    public List<GenreBookCountViewModel> GenreBookCounts { get; set; } = new();
    public int AvailableBooksCount { get; set; }
    public double AverageLoanLengthDays { get; set; }
}

public class GenreBookCountViewModel
{
    public string GenreName { get; set; } = string.Empty;
    public int BookCount { get; set; }
}