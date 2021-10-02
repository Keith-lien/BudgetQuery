using NUnit.Framework;
using System;

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
    //> 是不是月底 month.01 + 1 month - 1 day
    // 算 start end 之間 有幾天 new TimeSpan(date1.Ticks - date2.Ticks).TotalDays

    #endregion MyRegion

    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Invalid_Period()
        {
            var budgetService = new BudgetService();
            var actual = budgetService.Query(new DateTime(2021, 1, 2), new DateTime(2021, 1, 1));
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Database_NoData()
        {
            var budgetService = new BudgetService();
            var actual = budgetService.Query(new DateTime(2021, 3, 1), new DateTime(2021, 3, 31));
            Assert.AreEqual(0, actual);
        }
    }
}