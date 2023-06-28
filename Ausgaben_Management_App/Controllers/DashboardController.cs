using Ausgaben_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ausgaben_Management_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }    
        public async Task<ActionResult> Index()
        {
            //Last 7 day trans

            DateTime startDate = DateTime.Today.AddDays(-6);
            DateTime endDate = DateTime.Today;
            List<TbTransaktion> SelectedTransactions = await _context.TbTransaktionen
                .Include(x=>x.kategorie)
                .Where(y=>y.Datum >= startDate && y.Datum <= endDate)
                .ToListAsync();
            //Total Einkommen
            
            int TotalIncome = SelectedTransactions
                .Where(i => i.kategorie.Type == "Einkommen")
                .Sum(j => j.Betrag);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            // Total Ausgaben
            int TotalExpense = SelectedTransactions
                .Where(i => i.kategorie.Type == "Ausgaben")
                .Sum(j => j.Betrag);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");
            //Balance
            int Balance = TotalIncome - TotalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, /*"{0:C0}"*/"{0:n} €", Balance);

            //Doughnut Chart - Expense By Category
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(i => i.kategorie.Type == "Ausgaben")
                .GroupBy(j => j.kategorie.kategorieId)
                .Select(k => new
                {
                    KategorieTitelMitIcon = k.First().kategorie.Icon + " " + k.First().kategorie.Titel,
                    betrag = k.Sum(j => j.Betrag),
                    FormattedBetrag = k.Sum(j => j.Betrag).ToString("C0"),
                })
                .OrderByDescending(l => l.betrag)
                .ToList();

            //Spline Chart - Income vs Expense

            //Income
            List<SplineChartData> IncomeSummary = SelectedTransactions
                .Where(i => i.kategorie.Type == "Einkommen")
                .GroupBy(j => j.Datum)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Datum.ToString("dd-MMM"),
                    income = k.Sum(l => l.Betrag)
                })
                .ToList();

            //Expense
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(i => i.kategorie.Type == "Ausgaben")
                .GroupBy(j => j.Datum)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Datum.ToString("dd-MMM"),
                    expense = k.Sum(l => l.Betrag)
                })
                .ToList();

            //Combine Income & Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };
            //Recent Transactions
            ViewBag.RecentTransactions = await _context.TbTransaktionen
                .Include(i => i.kategorie)
                .OrderByDescending(j => j.Datum)
                .Take(5)
                .ToListAsync();
            return View();
        }

    }
    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;

    }
}
