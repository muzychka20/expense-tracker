using expense_tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
			// Set up culture for USD formatting
			CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
			culture.NumberFormat.CurrencyNegativePattern = 1;

			// Define date range for the last 7 days
			DateTime StartDate = DateTime.Today.AddDays(-6);
			DateTime EndDate = DateTime.Today;

			// Retrieve and filter transactions
			var SelectedTransactions = await _context.Transactions
				.Include(x => x.Category)
				.Where(y => y.Date >= StartDate && y.Date <= EndDate)
				.ToListAsync();

			// Total Income
			int TotalIncome = SelectedTransactions
				.Where(i => i.Category.Type == "Income")
				.Sum(j => j.Amount);
			ViewBag.TotalIncome = String.Format(culture, "{0:C0}", TotalIncome);

			// Total Expense
			int TotalExpense = SelectedTransactions
				.Where(i => i.Category.Type == "Expense")
				.Sum(j => j.Amount);
			ViewBag.TotalExpense = String.Format(culture, "{0:C0}", TotalExpense);

			// Calculate balance and format as currency
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
				.OrderByDescending(l => l.amount)
				.ToList();

			// Spline Chart - Income vs Expense
			// Income summary by day
			var IncomeSummary = SelectedTransactions
				.Where(i => i.Category.Type == "Income")
				.GroupBy(j => j.Date)
				.Select(k => new SplineChartData
				{
					Day = k.First().Date.ToString("dd-MMM"),
					Income = k.Sum(l => l.Amount),
					Expense = 0
				})
				.ToList();

			// Expense summary by day
			var ExpenseSummary = SelectedTransactions
				.Where(i => i.Category.Type == "Expense")
				.GroupBy(j => j.Date)
				.Select(k => new SplineChartData
				{
					Day = k.First().Date.ToString("dd-MMM"),
					Income = 0,
					Expense = k.Sum(l => l.Amount)
				})
				.ToList();

			// Days for the last 7 days in "dd-MMM" format
			string[] Last7Days = Enumerable.Range(0, 7)
				.Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
				.ToArray();

			// Combine Income & Expense data for Spline Chart
			ViewBag.SplineChartData = from day in Last7Days
									  join income in IncomeSummary on day equals income.Day into incomeJoined
									  from income in incomeJoined.DefaultIfEmpty(new SplineChartData { Day = day, Income = 0, Expense = 0 })
									  join expense in ExpenseSummary on day equals expense.Day into expenseJoined
									  from expense in expenseJoined.DefaultIfEmpty(new SplineChartData { Day = day, Income = 0, Expense = 0 })
									  select new SplineChartData
									  {
										  Day = day,
										  Income = income.Income,
										  Expense = expense.Expense
									  };

			// Recent Transactions
			var recentTransactions = await _context.Transactions
				.Include(j => j.Category)
				.OrderByDescending(i => i.Date)
				.Take(5)
				.ToListAsync();

			// Format each transaction's amount with the USD format
			ViewBag.RecentTransactions = recentTransactions
				.Select(t => new
				{
					Date = t.Date.ToString("MMM-dd-yy", culture), // Format the date as "MMM-dd-yy"
					CategoryTitleWithIcon = t.Category.Icon + " " + t.Category.Title,
					FormattedAmount = String.Format(culture, "{0:C0}", t.Amount) // Format the amount as currency
				})
				.ToList();


			return View();
		}
	}

	// Define SplineChartData class properly
	public class SplineChartData
	{
		public string Day { get; set; }
		public int Income { get; set; }
		public int Expense { get; set; }
	}
}
