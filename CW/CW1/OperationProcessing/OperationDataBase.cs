namespace FinanceModule.OperationProcessing;

public class OperationDataBase : IRegisterService<Operation>, IOperationDescriptionProcessor {
  private Dictionary<Int64, Operation> operationsLog = new Dictionary<Int64, Operation>();
  
  public void Register(Operation operation) {
    if (!operationsLog.TryAdd(operation.Id, operation)) {
      throw new ArgumentException($"There is already an operation performed with id: {operation.Id}");
    }
  }

  public bool Unregister(Operation operation) {
    return operationsLog.Remove(operation.Id);
  }

  public string GetDescription(Int64 id) {
    if (!operationsLog.TryGetValue(id, out var operation)) {
      throw new ArgumentException($"There is no operation performed with id: {id}");
    }
    
    return operation.Description;
  }

  public void СhangeDescription(Int64 id, string description) {
    if (!operationsLog.TryGetValue(id, out var operation)) {
      throw new ArgumentException($"There is no operation performed with id: {id}");
    }
    
    operation.Description = description;
  }
}