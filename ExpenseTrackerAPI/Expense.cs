using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
    }
}
