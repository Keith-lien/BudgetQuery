using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetQuery
{
    public class BudgetService
    {
        private readonly IBudgetRepo _repo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _repo = budgetRepo;
        }

        public double Query(DateTime start, DateTime end)
        {
            if (start > end)
            {
                return 0;
            }
            var budgets = _repo.GetAll();
            var startMonth = start.ToString("yyyyMM");

            var daysInMonth = DateTime.DaysInMonth(end.Year, end.Month);
            if (start.Day != 1 || end.Day != daysInMonth)
            {
                if (budgets != null)
                {
                    var amount = budgets.FirstOrDefault(t => t.YearMonth == startMonth)?.Amount ?? 0;
                    var days = end.Day - start.Day + 1;
                    return (amount / daysInMonth) * days;
                }
            }

            if (budgets.Any())
            {
                var startDate = start.ToString("yyyyMM");
                var endDate = end.ToString("yyyyMM");

                var budgetList = budgets.Where(x =>
                {
                    return string.Compare(startDate, x.YearMonth, StringComparison.Ordinal) <= 0
                           && string.Compare(endDate, x.YearMonth, StringComparison.Ordinal) >= 0;
                }).ToList();

                return budgetList.Sum(x => x.Amount);
                return budgets.FirstOrDefault(t => t.YearMonth == startMonth)?.Amount ?? 0;
            }

            return 0;
        }
    }

    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }
    }

    public class BudgetRepo : IBudgetRepo
    {
        public List<Budget> GetAll()
        {
            return new List<Budget>();
        }
    }

    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}