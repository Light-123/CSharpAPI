using System.ComponentModel.DataAnnotations;

namespace ExpenseReport.DTO
{
    public class UpdateSalary
    {
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
        [Required]
        public string? Business_Function { get; set; }
    }
}
