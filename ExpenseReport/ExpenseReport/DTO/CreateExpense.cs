using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseReport.DTO
{
    public class CreateExpense
    {
        public DateTime Expense_Date { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Amount { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Category { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Details { get; set; }
    }
}
