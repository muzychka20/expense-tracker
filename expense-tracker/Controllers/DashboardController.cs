using expense_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Globalization;

namespace expense_tracker.Controllers
{
	public class DashboardController : Controller
	{
		private readonly ApplicationDbContext _context;


		public DashboardController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ActionResult> Index()
		{
			// For displaying $
			CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
			culture.NumberFormat.CurrencyNegativePattern = 1;

			// Last 7 days
			DateTime StartDate = DateTime.Today.AddDays(-6);
			DateTime EndDate = DateTime.Today;

			List<Transaction> SelectedTransactions = await _context.Transactions
				.Include(x => x.Category)
				.Where(y => y.Date >= StartDate && y.Date <= EndDate)
				.ToListAsync();

			// Total Income
			int TotalIncome = SelectedTransactions
				.Where(i => i.Category.Type == "Income")
				.Sum(j => j.Amount);
			ViewBag.TotalIncome = String.Format(culture, "{0:C0}", TotalIncome);
			// ViewBag.TotalIncome = TotalIncome.ToString("C0");

			// Total Expense
			int TotalExpense = SelectedTransactions
				.Where(i => i.Category.Type == "Expense")
				.Sum(j => j.Amount);
			ViewBag.TotalExpense = String.Format(culture, "{0:C0}", TotalExpense);
			//ViewBag.TotalExpense = TotalExpense.ToString("C0");

			int Balance = TotalIncome - TotalExpense;
			ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

			// Doughnut Chart - Expense By Category
			ViewBag.DoughnutChartData = SelectedTransactions
				.Where(i => i.Category.Type == "Expense")
				.GroupBy(j => j.Category.CategoryId)
				.Select(k => new
				{
					categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
					amount = k.Sum(j => j.Amount),
					formattedAmount = k.Sum(j => j.Amount).ToString("C0", culture),
				})
				.ToList();

			return View();
		}
	}
}
