using ExpenseReport.Context;
using ExpenseReport.DTO;
using ExpenseReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ExpenseReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;//Context pour se connecter à la base de données
        public SalaryController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet(Name = "GetSalaries")]
        async public Task<ActionResult<IEnumerable<Salary>?>> GetSalaries()
        {
            return Ok(await _context.Expenses.ToListAsync());
        }

        [HttpGet("{id}", Name = "GetSalary")]
        public async Task<ActionResult> GetSalary([FromRoute] int id)
        {
            // Recherche du salarié par id
            Salary? salary = await _context.Salaries.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (salary == null)
            {
                return NotFound();//Retour de valeur non trouvé
            }
            // Retourne la valeur trouvé si la condition if n'est pas true
            else
            {
                return Ok(salary);
            }
        }


        [HttpPost(Name = "WelcomeSalary")]
        public async Task<ActionResult> Create([FromBody] WelcomeSalary welcomeSalary)
        {

            Salary salary = new Salary();//Instanciation de l'objet "expense"

            //Affectation des valeurs 

            salary.Last_Name = welcomeSalary.Last_Name;
            salary.First_Name = welcomeSalary.First_Name;
            salary.Business_Function = welcomeSalary.Business_Function;

            // Ajoute l'occurrence dans la base de données
            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();

            // La note de frais retourné
            return CreatedAtAction(nameof(GetSalary), new { id = salary.Id }, salary);
        }

        [HttpPatch("{id}", Name = "UpdateSalary")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSalary updateSalary)
        {
            // find food by id
            Salary? salary = await _context.Salaries.FindAsync(id);

            // return Notfound if food not found else return food
            if (salary == null) return NotFound();

            // Affection des valeur
            salary.Last_Name = updateSalary.Last_Name;
            salary.First_Name = updateSalary.First_Name;
            salary.Business_Function = updateSalary.Business_Function;

            // Mise à jour de la note de frais dans la collection Expenses
            _context.Salaries.Update(salary);
            await _context.SaveChangesAsync();

            // Retourne la note de frais modifié
            return CreatedAtAction(nameof(GetSalary), new { id = salary.Id }, salary);
        }

        [HttpDelete("{id}", Name = "DeleteSalary")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            // Cherche la note de frais en fonction de l'identifiant
            Salary? salary = await _context.Salaries.FindAsync(id);

            // Retourne un notfound si la valeur de l'objet est nulle
            if (salary == null) return NotFound();

            // Supprime l'objet de la collection Expenses et donc de la colonne 
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();

            // Retourne un message de confirmation de la requête 
            return Ok("Salary has been well deleted");
        }


    }
}