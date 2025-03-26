namespace FinanceModule.PublicInterface.ClientPanel;
using OperationProcessing;

public class Invoke {
  private ClientCommand? _command;

  public decimal GetBalance(Int64 id) {
    _command = new GetBalanceCommand() {Id = id};

    try {
      _command.Exec();
    }
    catch (Exception e) {
      throw new AggregateException($"Can't execute command {nameof(GetBalance)}, errors happened", e);
    }
    
    return (_command as GetBalanceCommand)!.Result;
  }

  public string GetName(Int64 id) {
    _command = new GetNameCommand() {Id = id};

    try {
      _command.Exec();
    }
    catch (Exception e) {
      throw new AggregateException($"Can't execute command {nameof(GetName)}, errors happened", e);
    }
    
    return (_command as GetNameCommand)!.Result;
  }

  public void ChangeName(Int64 id, string newName) {
    _command = new ChangeNameCommand() {Id = id, NewName = newName};
    
    try {
      _command.Exec();
    }
    catch (Exception e) {
      throw new AggregateException($"Can't execute command {nameof(ChangeName)}, errors happened", e);
    }
  }

  public void PerformNewOperation(Operation operation) {
    _command = new PerformNewOperationCommand() {Operation = operation};
    
    try {
      _command.Exec();
    }
    catch (Exception e) {
      throw new AggregateException($"Can't execute command " +
                                   $"{nameof(PerformNewOperation)}, errors happened", e);
    }
  }
}