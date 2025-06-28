using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMasterService.Services.Abstractions.Transaction;
using MoneyMasterService.Services.Contracts.Transaction;

namespace MoneyMasterService.WebAPI.Controllers;
/// <summary>
/// Контроллер транзакций
/// </summary>
[ApiController]
[Route("api/v1/transactions")]
[Authorize]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    /// <summary>
    /// Получение объекта транзакции
    /// </summary>
    /// <remarks>Данный метод позволяет получить объект транзакции по её идентификатору</remarks>
    /// <param name="id">Идентификатор транзакции</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <response code="200">Получение объекта транзакции</response>
    /// <response code="404">Не удалось найти транзакцию по указанному идентификатору</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> GetTransaction([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionService.GetByIdAsync(id, cancellationToken);

        return transaction is null ? NotFound() : Ok(transaction);
    }

    /// <summary>
    /// Получение всех транзакций.
    /// </summary>
    /// <remarks>
    /// Данный метод позволяет получить список всех транзакций. 
    /// </remarks>
    /// <response code="200">Получение списка всех транзакций</response>
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<TransactionResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<TransactionResponse>> GetAllTransactions(CancellationToken cancellationToken)
    {
        var transactions = await transactionService.GetAllAsync(cancellationToken);

        return Ok(transactions);
    }

    /// <summary>
    /// Создание транзакции.
    /// </summary>
    /// <remarks>
    /// Данный метод позволяет создать новую транзакцию. 
    /// </remarks>
    /// <response code="201">Транзакция успешно создана</response>
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> CreateTransaction([FromBody] CreatingTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var id = await transactionService.CreateAsync(request, cancellationToken);

        return Created(string.Empty, id);
    }

    /// <summary>
    /// Изменение транкзакции.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> UpdateTransaction(
        [FromRoute] Guid id,
        [FromBody] UpdatingTransactionRequest request,
        CancellationToken cancellationToken)
    {
        var transaction = await transactionService.UpdateAsync(id, request, cancellationToken);

        return Ok(transaction);
    }

    /// <summary>
    /// Удаление транзакции.
    /// </summary>
    /// <param name="id">Идентификатор транзакции</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTransaction([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await transactionService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Восстановление транзакции.
    /// </summary>
    [HttpPost("{id:guid}")]
    [ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> RestoreTransaction([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var restoredTransaction = await transactionService.RestoreAsync(id, cancellationToken);

        return Ok(restoredTransaction);
    }

    /// <summary>
    /// Получить транзакции по id счета
    /// </summary>
    /// <param name="id">Индентификатор счета</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet("account/{id:guid}")]
    [ProducesResponseType<IReadOnlyCollection<TransactionResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionResponse>> GetTransactionByAccountIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var transactions = await transactionService.GetByAccountIdAsync(id, cancellationToken);
        return Ok(transactions);
    }

    /// <summary>
    /// Создать перевод с одного счета на другой
    /// </summary>
    /// <param name="request">Модель для создания перевода</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("transfer")]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> CreateTransactionTransfer([FromBody] CreateTransactionTransferRequest request,
        CancellationToken cancellationToken)
    {
        var transactionId = await transactionService.CreateTransactionTransferAsync(request, cancellationToken);
        return Ok(transactionId);
    }

    /// <summary>
    /// Получение  транзакций за период.
    /// </summary>
    /// <remarks>
    /// Данный метод позволяет получить список всех транзакций за выбранный период. 
    /// </remarks>
    /// <param name="startDate">Начало периода</param>
    /// <param name="endDate">Конец периода</param>
    /// <param name="cancellationToken"></param>
    /// <response code = "200" > Получение списка всех транзакций за период</response>
    /// <returns></returns>
    [HttpGet("GetByDataRange/{id:guid}")]
    [ProducesResponseType<IReadOnlyCollection<TransactionResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<TransactionResponse>> GetByDataRange([FromRoute] Guid id, [FromQuery] DateTime startDate , [FromQuery] DateTime endDate,CancellationToken cancellationToken)
    {
        var transactions = await transactionService.GetByDataRange(id,startDate,endDate, cancellationToken);

        return Ok(transactions);
    }
}
