namespace FinanceModule.CategoryProcessing;

public class Category {
  private Int64 id;
  private string type;

  public Int64 Id {
    get => id;

    set {
      if (value < 0) {
        throw new ArgumentOutOfRangeException(nameof(id), "Category id cannot be negative");
      }
    }
  }

  public string Type {
    get => type;

    set {
      if (value is null) {
        throw new ArgumentNullException(nameof(value), "Type of category need to exist");
      }
    }
  }

  public bool IsIncome { get; init; }
}