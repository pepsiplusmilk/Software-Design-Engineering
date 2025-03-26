namespace FinanceModule;

public interface IRegisterService<T> {
  public void Register(T item);
  public bool Unregister(T item);
}