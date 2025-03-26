namespace FinanceModule.OperationProcessing;

public interface IOperationDescriptionProcessor {
  public string GetDescription(Int64 id);
  public void СhangeDescription(Int64 id, string newDescription);
}