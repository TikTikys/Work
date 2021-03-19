using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseBookApp
{
    public class Expenditure
    {
        [Key]
        public int ExpenditureId { get; set; }
        public char Category { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime Date { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
