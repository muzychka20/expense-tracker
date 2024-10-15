using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expense_tracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName="VARCHAR(50)")]
        public string Title { get; set; }

        [Column(TypeName = "VARCHAR(5)")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "VARCHAR(10)")]
        public string Type { get; set; } = "Expense";
    }
}
