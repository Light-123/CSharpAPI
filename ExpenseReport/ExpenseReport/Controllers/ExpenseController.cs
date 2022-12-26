using ExpenseReport.Context;
using ExpenseReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpenseReport.DTO;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ExpenseReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;//Context pour se connecter à la base de données
        public ExpenseController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet(Name = "GetExpenses")]
        async public Task<ActionResult<IEnumerable<Expense>?>> GetExpenses()
        {
            return Ok(await _context.Expenses.ToListAsync());
        }

        [HttpGet("{id}", Name = "GetExpense")]
        public async Task<ActionResult> GetExpense([FromRoute] int id)
        {
           // Recherche de la note de frais par id
            Expense? expense = await _context.Expenses.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (expense==null)
            {
                return NotFound();//Retour de valeur non trouvé
            }
            // Retourne la valeur trouvé si la condition if n'est pas true
            else
            {
                return Ok(expense);
            }
        }


        [HttpPost(Name = "CreateExpense")]
        public async Task<ActionResult> Create([FromBody] CreateExpense createExpense)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            Expense expense = new Expense();//Instanciation de l'objet "expense"

            //Attribution de la date du moment présent vers l'attribut du paramètre createExpense 
            createExpense.Expense_Date = dateTime;

            //Affectation des valeurs 

            expense.Amount = createExpense.Amount;
            expense.Category = createExpense.Category;
            expense.Details = createExpense.Details;
            expense.Expense_Date = createExpense.Expense_Date;
                


            // Ajoute l'occurrence dans la base de données
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            // La note de frais retourné
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        [HttpPatch("{id}", Name = "UpdateExpense")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateExpense updateExpense)
        {
            // find food by id
            Expense? expense = await _context.Expenses.FindAsync(id);

            // return Notfound if food not found else return food
            if (expense == null) return NotFound();

            // Affection des valeur
            expense.Amount = updateExpense.Amount;
            expense.Category = updateExpense.Category;
            expense.Details = updateExpense.Details;
            expense.Expense_Date = updateExpense.Expense_Date;

            // Mise à jour de la note de frais dans la collection Expenses
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();

            // Retourne la note de frais modifié
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }



        [HttpPost("{id}", Name = "ExpenseChosen")]

        public async Task<ActionResult> Affectation([FromRoute] int id, [FromBody] UpdateExpense updateExpense)
        {
            // find food by id
            Expense? expense = await _context.Expenses.FindAsync(id);

            // return Notfound if food not found else return food
            if (expense == null) return NotFound();

            // Affection des valeur
            expense.Amount = updateExpense.Amount;
            expense.Category = updateExpense.Category;
            expense.Details = updateExpense.Details;
            expense.Expense_Date = updateExpense.Expense_Date;

            // Mise à jour de la note de frais dans la collection Expenses
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();

            // Retourne la note de frais modifié
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }


        [HttpDelete("{id}", Name = "DeleteExpense")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            // Cherche la note de frais en fonction de l'identifiant
            Expense? expense = await _context.Expenses.FindAsync(id);

            // Retourne un notfound si la valeur de l'objet est nulle
            if (expense == null) return NotFound();

            // Supprime l'objet de la collection Expenses et donc de la colonne 
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            // Retourne un message de confirmation de la requête 
            return Ok("Expenses has been well deleted");
        }
    }
}
