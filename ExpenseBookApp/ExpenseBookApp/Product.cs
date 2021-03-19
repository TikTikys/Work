using System.ComponentModel.DataAnnotations;

namespace ExpenseBookApp
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductCount { get; set; }

        public int ExpenditureId { get; set; }
        public Expenditure Expenses { get; set; }
    }
}
