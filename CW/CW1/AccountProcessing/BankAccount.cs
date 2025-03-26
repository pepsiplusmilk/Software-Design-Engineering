namespace FinanceModule.AccountProcessing;

public class BankAccount {
  private Int64 id;
  private string name;

  public Int64 Id {
    get => id;
    
    set {
      if (value < 0) {
        throw new ArgumentOutOfRangeException(nameof(id), "Customer account id cannot be negative");
      }
      
      id = value;
    }
  }

  public string Name {
    get => name;

    set {
      if (value is null) {
        throw new ArgumentNullException(nameof(value), "Customer account name need to exist");
      }
      
      if (value.Length > 32) {
        throw new ArgumentOutOfRangeException(nameof(name),"Customer account name cannot exceed 32 characters");
      }
      
      name = value;
    }
  }

  public decimal Balance { get; set; }
  public decimal Overdraft { get; set; }
}