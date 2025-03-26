namespace FinanceModule.OperationProcessing;

public class Operation {
  private Int64 _id;
  private decimal amount;
  private DateTime date;
  
  private Int64 bankAccountId;
  private Int64 categoryId;
    
  private string? _description;

  public Int64 Id {
    get => _id;
    
    init {
      if (value < 0) {
        throw new ArgumentOutOfRangeException(nameof(_id), "Operation id cannot be negative");
      }
    }
  }

  public bool IsIncome { get; init; }

  public decimal Amount {
    get => amount;
    init {
      if (value < 0) {
        throw new ArgumentOutOfRangeException(nameof(amount), "Bank cannot process operations with negative amount");
      }
    }
  }
  
  public DateTime Date { get; init; }

  public Int64 BankAccountId {
    get => bankAccountId;
    init {
      if (value < 0) {
        throw new ArgumentOutOfRangeException(nameof(bankAccountId), "Bank accounts with negative id's doesn't exist");
      }
    }
  }

  public Int64 CategoryId {
    get => categoryId;
    init {
      if (value < 0) {
        throw new ArgumentOutOfRangeException(nameof(categoryId), "Categories with negative id's doesn't exist");
      }
    }
  }

  public string? Description {
    get => _description;
    
    set {
      if (value is null) {
        _description = "";
      }
      
      _description = value;
    }
  }
}