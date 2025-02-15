using System.Reflection;

namespace MoscowZooERP.CommandProcessing;

/// <summary>
/// Класс который реализует проверку поступившей из консоли строки на то что она является командой
/// </summary>
/// <typeparam name="T"> Параметризованный тип перечисления </typeparam>
public class Matcher<T> where T : System.Enum {
  // Команды задаются 1 раз при создании, подразумевается что консоль стабильна
  private readonly List<ICommand<T>> _commands;

  public T Parse(string? text) {
    if (text is null) {
      throw new ArgumentNullException(text, "Null-строка не может являться корректной коммандой");
    }

    List<ICommand<T>> matches = new List<ICommand<T>>();

    foreach (var command in _commands) {
      if (command.Check(text)) {
        matches.Add(command);
      }
    }

    if (matches.Count == 1) {
      return matches[0].ReturnCode();
    }

    if (matches.Count == 0) {
      throw new ArgumentException("Данная строка не является командой исполнения");
    }

    throw new AmbiguousMatchException("Нашлось более 1 совпадения для введенной команды.");
  }

  public List<ICommand<T>> Commands {
    init {
      if (value is null) {
        _commands = new List<ICommand<T>>();
      } else {
        _commands = new List<ICommand<T>>(value);
      }

      if (_commands.Count == 0) {
        Console.WriteLine("Консоль построена без проверяемых команд. Это означает что при проверке любой строки" +
                          " вы получите ошибку, так команд для совпадения нет.");
      }
    }
  }
}