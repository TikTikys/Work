using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseBookApp
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<Expenditure> Exceptions { get; set; } = new List<Expenditure>();

        public List<Income> Incomes { get; set; } = new List<Income>();
    }
}
