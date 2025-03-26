namespace FinanceModule.PublicInterface.AdministratorPanel;

using FinanceModule;
using Facades; 

public abstract class AdminCommand {
  protected AdministratorWorkInterface? _workInterface;
  public abstract void Exec();
}
