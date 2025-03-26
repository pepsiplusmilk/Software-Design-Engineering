using FinanceModule.OperationProcessing;

namespace FinanceModule.Facades;
using AccountProcessing;
using Microsoft.Extensions.DependencyInjection;

public class ClientWorkInterface {
  private ServiceCollection? _dataBaseServicesCollection;
  private ServiceProvider? _dataBaseServiceProvider;

  public void SubscribeToDataBase() {
    _dataBaseServicesCollection = new ServiceCollection();
    
    _dataBaseServicesCollection.AddSingleton<IBankAccountBalanceProcessor, AccountsDataBase>();
    _dataBaseServicesCollection.AddSingleton<IBankAccountNameProcessor, AccountsDataBase>();

    _dataBaseServiceProvider = _dataBaseServicesCollection.BuildServiceProvider();
  }
  
  public void PerformNewOperation(Operation operation) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Client interface isn't subscribed to any databases");
    }
    
    try {
      _dataBaseServiceProvider.GetService<IBankAccountBalanceProcessor>()!.
        ChangeBalance(operation.BankAccountId, operation.Amount);
    }
    catch (Exception e) {
      throw new Exception($"Something went wrong, while working with database", e);
    }
  }

  public decimal GetBalance(Int64 id) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Client interface isn't subscribed to any databases");
    }
    
    return _dataBaseServiceProvider.GetService<IBankAccountBalanceProcessor>()!.GetBalance(id);
  }

  public string GetName(Int64 id) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Client interface isn't subscribed to any databases");
    }

    return _dataBaseServiceProvider.GetService<IBankAccountNameProcessor>()!.GetName(id);
  }

  public void ChangeName(Int64 id, string newName) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Client interface isn't subscribed to any databases");
    }

    try {
      _dataBaseServiceProvider.GetService<IBankAccountNameProcessor>()!.ChangeName(id, newName);
    }
    catch (Exception e) {
      throw new Exception($"Something went wrong, while working with database", e);
    }
  }

  public void ExportOperationsToCSV() {
    
  }

  public void ExportOperationsToJSON() {
    
  }

  public void ExportOperationsToYAML() {
    
  }
}