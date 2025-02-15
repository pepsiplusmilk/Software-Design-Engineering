namespace MoscowZooERP.CommandProcessing;

public interface ICommand<T> where T : System.Enum {
  public bool Check(string? text);
  public T ReturnCode();
}