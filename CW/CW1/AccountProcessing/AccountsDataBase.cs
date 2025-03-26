namespace FinanceModule.AccountProcessing;

// In future this class can be replaced with class that works with database solution
public class AccountsDataBase : IRegisterService<BankAccount>, IBankAccountNameProcessor,
IBankAccountBalanceProcessor {
  private Dictionary<Int64, BankAccount> _bankAccounts = new Dictionary<Int64, BankAccount>();
  
  public void Register(BankAccount bankAccount) {
    if (!_bankAccounts.TryAdd(bankAccount.Id, bankAccount)) {
      throw new ArgumentException($"There is already an account with id: {bankAccount.Id}");
    }
  }

  public bool Unregister(BankAccount bankAccount) {
    return _bankAccounts.Remove(bankAccount.Id);
  }

  public string GetName(Int64 id) {
    if (!_bankAccounts.TryGetValue(id,out var bankAccount)) {
      throw new ArgumentException($"There is no account with id: {id}");
    }
    
    return bankAccount.Name;
  }

  public void ChangeName(Int64 id, string newName) {
    if (!_bankAccounts.TryGetValue(id, out var bankAccount)) {
      throw new ArgumentException($"There is no account with id: {id}");
    }

    try {
      bankAccount.Name = newName;
    }
    catch (Exception e) {
      throw new ArgumentException("While changing name of account something went wrong", e);
    }
  }

  public decimal GetBalance(Int64 id) {
    if (!_bankAccounts.TryGetValue(id, out var bankAccount)) {
      throw new ArgumentException($"There is no account with id: {id}");
    }
    
    return bankAccount.Balance;
  }

  public void ChangeBalance(Int64 id, decimal enrollment) {
    if (!_bankAccounts.TryGetValue(id, out var bankAccount)) {
      throw new ArgumentException($"There is no account with id: {id}");
    }

    if (bankAccount.Balance + enrollment < bankAccount.Overdraft) {
      throw new Exception("Operation cannot be performed beyond the bank account overdraft");
    }
    
    bankAccount.Balance += enrollment;
  }
}