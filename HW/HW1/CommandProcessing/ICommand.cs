namespace MoscowZooERP.CommandProcessing;

public interface ICommand<T> where T : System.Enum {
  public bool Check(string? text); // Проверить подходит ли строка хоть под какой-то паттерн
  public T ReturnCode(); // Получить код из перечисления
}