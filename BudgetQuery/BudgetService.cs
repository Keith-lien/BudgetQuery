using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetQuery
{
    public class BudgetService
    {
        public double Query(DateTime start, DateTime end)
        {
            IBudgetRepo repo = new BudgetRepo();
            var budgets = repo.GetAll();
            if (false == budgets.Any())
            {
                return 0;
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