using ExpenseReport.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ExpenseReport.Context
{
    public class ApplicationDbContext : DbContext //Classe héritant de DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //Constructeur en paramètre
        {

        }

        public DbSet<Salary> Salaries { get; set; }//Création de table dans la base de données
        public DbSet<Expense> Expenses { get; set; }//Création de table dans la base de données
    }
}

