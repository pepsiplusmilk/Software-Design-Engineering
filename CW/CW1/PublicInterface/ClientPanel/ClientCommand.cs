using FinanceModule.OperationProcessing;

namespace FinanceModule.PublicInterface.ClientPanel;

using FinanceModule;
using Facades;

public abstract class ClientCommand {
  protected ClientWorkInterface? _workInterface;
  public abstract void Exec();

  public ClientWorkInterface? WorkInterface {
    set =>
      _workInterface = value ?? throw new ArgumentNullException(nameof(_workInterface),
        "Client interface module need to exist before appointed to the command");
  }
}

public class GetBalanceCommand : ClientCommand {
  private readonly Int64 _id;
  private decimal _result;

  public override void Exec() {
    _result = _workInterface!.GetBalance(_id);
  }

  public Int64 Id {
    init => _id = value;
  }

  public decimal Result => _result;
}

public class PerformNewOperationCommand : ClientCommand {
  private readonly Operation _operation;

  public override void Exec() {
    _workInterface!.PerformNewOperation(_operation);
  }

  public Operation Operation {
    init => _operation = value;
  }
}

public class GetNameCommand : ClientCommand {
  private readonly Int64 _id;
  private string _result;

  public override void Exec() {
    _result = _workInterface!.GetName(_id);
  }

  public Int64 Id {
    init => _id = value;
  }
  
  public string Result => _result;
}

public class ChangeNameCommand : ClientCommand {
  private readonly Int64 _id;
  private readonly string _newName;

  public override void Exec() {
    _workInterface!.ChangeName(_id, _newName);
  }

  public Int64 Id {
    init => _id = value;
  }

  public string NewName {
    init => _newName = value;
  }
}