using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ExpenseReport.Entities
{
    public class Expense : DbContext
    {
        [Key]
        public int? Id { get; set; }
        [Required]
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
        //Création de relation entre tables
        //Plusieurs occurrences de notes de frais ne peuvent dépendre d'une seule occurence de Salariés
        Salary Salary { get; set; } 

        
    }
}
