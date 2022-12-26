using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseReport.Entities
{
    public class Salary : DbContext
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
        [Required]
        public string? Business_Function { get; set; }

        //Création de relation entre tables
        //Une occurrence de salarié peut correspondre à plusieurs occurrences de notes de frais
        ICollection<Expense> Expenses { get; set; }
    }
}
