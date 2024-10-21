﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expense_tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        // CategoryId
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "NVARCHAR(75)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;    
            } 
        }

		[NotMapped]
		public string? FormattedAmount
		{
			get
			{
				return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
			}
		}
	}
}
