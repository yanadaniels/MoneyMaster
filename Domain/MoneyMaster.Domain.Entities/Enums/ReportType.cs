namespace MoneyMaster.Domain.Entities.Enums
{
    /// <summary>Тип отчета</summary>
    public enum ReportType
    {
        /// <summary>Финансовый отчет </summary>
        FinancialSummary,
        /// <summary>Анализ категории</summary>
        CategoryAnalysis,
        /// <summary>Сравнение бюджета</summary>
        BudgetComparison,
        /// <summary>Инвестиционный отчет</summary>
        InvestmentReport,
        /// <summary>История транзакций</summary>
        TransactionHistory
    }
}
