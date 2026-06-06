using Titan.AniTec.Platform.Financial.Domain.Model;
using Titan.AniTec.Platform.Financial.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Financial.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Financial.Application.Internal.CommandServices;

public class FinancialCommandService(
    ITransactionRepository transactionRepository,
    IBudgetRepository budgetRepository,
    IFinancialCategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IFinancialCommandService
{
    public async Task<Result<Transaction>> RegisterTransactionAsync(RegisterTransactionCommand command)
    {
        try
        {
            var transaction = new Transaction(command.UserId, command.TransactionDate, command.Type,
                command.Category, command.Description, command.Amount, command.AnimalId,
                command.PaymentMethod, command.Reference, command.Notes);
            await transactionRepository.AddAsync(transaction);
            await unitOfWork.CompleteAsync();
            return Result<Transaction>.Success(transaction);
        }
        catch (Exception)
        {
            return Result<Transaction>.Failure(FinancialError.InvalidTransactionData);
        }
    }

    public async Task<Result<Transaction>> UpdateTransactionAsync(UpdateTransactionCommand command)
    {
        try
        {
            var transaction = await transactionRepository.FindByIdAsync(command.TransactionId);
            if (transaction == null)
                return Result<Transaction>.Failure(FinancialError.TransactionNotFound);

            transaction.UpdateDetails(command.TransactionDate, command.Type, command.Category,
                command.Description, command.Amount, command.AnimalId, command.PaymentMethod,
                command.Reference, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Transaction>.Success(transaction);
        }
        catch (Exception)
        {
            return Result<Transaction>.Failure(FinancialError.InvalidTransactionData);
        }
    }

    public async Task<Result> DeleteTransactionAsync(DeleteTransactionCommand command)
    {
        try
        {
            var transaction = await transactionRepository.FindByIdAsync(command.TransactionId);
            if (transaction == null)
                return Result.Failure(FinancialError.TransactionNotFound);

            transactionRepository.Remove(transaction);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(FinancialError.TransactionNotFound);
        }
    }

    public async Task<Result<Budget>> RegisterBudgetAsync(RegisterBudgetCommand command)
    {
        try
        {
            var existing = await budgetRepository.FindByYearMonthCategoryAsync(
                command.UserId, command.Year, command.Month, command.Category, command.BudgetType);
            if (existing != null)
                return Result<Budget>.Failure(FinancialError.BudgetAlreadyExists);

            var budget = new Budget(command.UserId, command.Year, command.Month, command.Category,
                command.BudgetType, command.PlannedAmount, command.Notes);
            await budgetRepository.AddAsync(budget);
            await unitOfWork.CompleteAsync();
            return Result<Budget>.Success(budget);
        }
        catch (Exception)
        {
            return Result<Budget>.Failure(FinancialError.InvalidBudgetData);
        }
    }

    public async Task<Result<Budget>> UpdateBudgetAsync(UpdateBudgetCommand command)
    {
        try
        {
            var budget = await budgetRepository.FindByIdAsync(command.BudgetId);
            if (budget == null)
                return Result<Budget>.Failure(FinancialError.BudgetNotFound);

            budget.UpdateDetails(command.Year, command.Month, command.Category,
                command.BudgetType, command.PlannedAmount, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Budget>.Success(budget);
        }
        catch (Exception)
        {
            return Result<Budget>.Failure(FinancialError.InvalidBudgetData);
        }
    }

    public async Task<Result> DeleteBudgetAsync(DeleteBudgetCommand command)
    {
        try
        {
            var budget = await budgetRepository.FindByIdAsync(command.BudgetId);
            if (budget == null)
                return Result.Failure(FinancialError.BudgetNotFound);

            budgetRepository.Remove(budget);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(FinancialError.BudgetNotFound);
        }
    }

    // Categories
    public async Task<Result<FinancialCategory>> CreateCategoryAsync(CreateCategoryCommand command)
    {
        try
        {
            var category = new FinancialCategory(command.UserId, command.Name, command.Type, command.Description);
            await categoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
            return Result<FinancialCategory>.Success(category);
        }
        catch (Exception)
        {
            return Result<FinancialCategory>.Failure(FinancialError.InvalidCategoryData);
        }
    }

    public async Task<Result<FinancialCategory>> UpdateCategoryAsync(UpdateCategoryCommand command)
    {
        try
        {
            var category = await categoryRepository.FindByIdAsync(command.CategoryId);
            if (category == null)
                return Result<FinancialCategory>.Failure(FinancialError.CategoryNotFound);

            category.UpdateDetails(command.Name, command.Type, command.Description);
            await unitOfWork.CompleteAsync();
            return Result<FinancialCategory>.Success(category);
        }
        catch (Exception)
        {
            return Result<FinancialCategory>.Failure(FinancialError.InvalidCategoryData);
        }
    }

    public async Task<Result> DeleteCategoryAsync(DeleteCategoryCommand command)
    {
        try
        {
            var category = await categoryRepository.FindByIdAsync(command.CategoryId);
            if (category == null)
                return Result.Failure(FinancialError.CategoryNotFound);

            categoryRepository.Remove(category);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(FinancialError.CategoryNotFound);
        }
    }

    public async Task<Result> BatchCreateTransactionsAsync(BatchCreateTransactionsCommand command)
    {
        try
        {
            foreach (var item in command.Transactions)
            {
                var transaction = new Transaction(command.UserId, item.TransactionDate, item.Type,
                    item.Category, item.Description, item.Amount, item.AnimalId,
                    item.PaymentMethod, item.Reference, item.Notes);
                await transactionRepository.AddAsync(transaction);
            }

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(FinancialError.InvalidTransactionData);
        }
    }
}
