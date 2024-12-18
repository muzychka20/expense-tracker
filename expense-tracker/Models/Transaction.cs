﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace expense_tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category!")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
		
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0!")]
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
		public string FormattedAmount
		{
			get
			{
				// Format the amount as currency with the dollar sign
				return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
			}
		}
	}
}
