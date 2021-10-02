using NUnit.Framework;
using System;
using System.Collections.Generic;
using NSubstitute;

namespace BudgetQuery
{
    #region MyRegion

    //組別 04

    //1730: finish => repository

    //> test case define

    //> code review

    //查詢預算
    //1. 特定區間 可用預算的功能
    //> 資料欄位
    //table - budgets
    //col
    //YearMonth: char 6, ( 202104
    //Amount	 : int (> 0
    //=> 這一個月有多少預算可以用
    //資料可能有缺、可能有資料卻為 0、不連續月份

    //IBudgetRepo
    //+getAll() :List<Budget>(fake by NSubsutitue) 不做 filter ，拉出所有資訊

    //eg. 5/21 ~6/20 => 按比例切  5/21~5/31 共 11 天 所以是 11/31

    //> 先讓測試案例可以被整除，先不處理小數

    //> 檢查日期區間有效範圍 (確保 end 不會早於 start), 無效 return 0
    //> 查不到資料 return 0

    //[code:c#]

    //[/code]

    //2. Naming
    //Class:  BudgetService
    //Method:
    //double Query(DateTime Start, DateTime End)

    #endregion MyRegion

    #region MyRegion

    //> 考慮閏年 DateTime.IsLeapYear(Int32) https://docs.microsoft.com/zh-tw/dotnet/api/system.datetime.isleapyear?view=net-5.0
    //> 一個月有幾天 DateTime.DaysInMonth
    //> 是不是月底 month.01 + 1 month - 1 day e.g. 2021-03-03 => new DateTime(2021,3,1).addmonth(1).addDay(-1)
    // 算 start end 之間 有幾天 new TimeSpan(date1.Ticks - date2.Ticks).TotalDays

    #endregion MyRegion

    [TestFixture]
    public class UnitTest1
    {
        private BudgetService _budgetService;
        private IBudgetRepo _subBudgets;

        [SetUp]
        public void Init()
        {
            _subBudgets = Substitute.For<IBudgetRepo>();
            _budgetService = new BudgetService(_subBudgets);
        }

        [Test]
        public void Invalid_Period()
        {
            GivenReport(new Budget { YearMonth = "202101", Amount = 310 });
            var actual = _budgetService.Query(new DateTime(2021, 1, 2), new DateTime(2021, 1, 1));
            Assert.AreEqual(0, actual);
        }


        [Test]
        public void Database_NoBudget()
        {
            GivenReport();
            var actual = _budgetService.Query(new DateTime(2021, 3, 1), new DateTime(2021, 3, 31));
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void OneBudgetAmountIsZero()
        {
            GivenReport(new Budget { YearMonth = "202101", Amount = 310 });
            var actual = _budgetService.Query(new DateTime(2021, 3, 1), new DateTime(2021, 3, 31));
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void QueryOneMonth()
        {
            GivenReport(new Budget { YearMonth = "202101", Amount = 310 });
            var actual = _budgetService.Query(new DateTime(2021, 1, 1), new DateTime(2021, 1, 31));
            Assert.AreEqual(310, actual);
        }

        [Test]
        public void QueryTwoDaysInOneMonth()
        {
            GivenReport(new Budget { YearMonth = "202101", Amount = 310 });
            var actual = _budgetService.Query(new DateTime(2021, 1, 1), new DateTime(2021, 1, 2));
            Assert.AreEqual(20, actual);
        }

        private void GivenReport(params Budget[] budgets)
        {
            _subBudgets.GetAll().Returns(new List<Budget>(budgets));
        }
    }
}