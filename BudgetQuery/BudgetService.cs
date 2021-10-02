using System;
using System.CodeDom;
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

            if (budgets.Any())
            {
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