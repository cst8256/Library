namespace Library.Models;

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public DateOnly CheckedOutDate { get; set; }
    public DateOnly ReturnedDate { get; set; }
}