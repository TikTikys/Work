using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseBookApp
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        public decimal SumAdd { get; set; }
        public DateTime DateAdd { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}
