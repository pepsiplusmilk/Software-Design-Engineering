using Microsoft.Extensions.DependencyInjection;

namespace FinanceModule.Facades;
using CategoryProcessing;
using AccountProcessing;

public class AdministratorWorkInterface {
  private ServiceCollection? _dataBaseServicesCollection;
  private ServiceProvider? _dataBaseServiceProvider;

  public void SubscribeToDataBase() {
    _dataBaseServicesCollection = new ServiceCollection();

    _dataBaseServicesCollection.AddSingleton<IRegisterService<Category>, CategoryDataBase>();
    _dataBaseServicesCollection.AddSingleton<IRegisterService<BankAccount>, AccountsDataBase>();
    
    _dataBaseServiceProvider = _dataBaseServicesCollection.BuildServiceProvider();
  }
  
  public void AddClientAccount(BankAccount account) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Administrator interface isn't subscribed to any databases");
    }

    try {
      _dataBaseServiceProvider.GetService<IRegisterService<BankAccount>>()!.Register(account);
    }
    catch (Exception e) {
      throw new Exception("Something went wrong, while working with database", e);
    }
  }

  public void DeleteClientAccount(BankAccount account) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Administrator interface isn't subscribed to any databases");
    }

    try {
      _dataBaseServiceProvider.GetService<IRegisterService<BankAccount>>()!.Unregister(account);
    }
    catch (Exception e) {
      throw new Exception("Something went wrong, while working with database", e);
    }
  }

  public void AddCategory(Category category) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Administrator interface isn't subscribed to any databases");
    }
    
    try {
      _dataBaseServiceProvider.GetService<IRegisterService<Category>>()!.Register(category);
    }
    catch (Exception e) {
      throw new Exception("Something went wrong, while working with database", e);
    }
  }

  public void DeleteCategory(Category category) {
    if (_dataBaseServiceProvider is null) {
      throw new NullReferenceException("Administrator interface isn't subscribed to any databases");
    }

    try {
      _dataBaseServiceProvider.GetService<IRegisterService<Category>>()!.Unregister(category);
    }
    catch (Exception e) {
      throw new Exception("Something went wrong, while working with database", e);
    }
  }
  
  public void ExportOperationsToCSV() {
    
  }

  public void ExportOperationsToJSON() {
    
  }

  public void ExportOperationsToYAML() {
    
  }
}