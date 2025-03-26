namespace FinanceModule.AccountProcessing;

public interface IBankAccountNameProcessor {
  public string GetName(Int64 id);
  public void ChangeName(Int64 id, string newName);
}

public interface IBankAccountBalanceProcessor {
  public decimal GetBalance(Int64 id);
  
  public void ChangeBalance(Int64 id, decimal enrollment);
}