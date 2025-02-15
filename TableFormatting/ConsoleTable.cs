namespace MoscowZooERP.TableFormatting;

public class ConsoleTable {
  private LinkedList<string> _columnStrokes = new LinkedList<string>();
  private LinkedList<string> _columnHeaders = new LinkedList<string>();
  private LinkedList<string> _columnSpaces = new LinkedList<string>();

  private LinkedList<LinkedList<string>> _rows = new LinkedList<LinkedList<string>>();
  
  static string AdjustLeft(string data, int len) {
    if (data.Length <= len) {
      return new string(' ', len - data.Length) + data;
    }
    
    return data.Substring(0, len - 3) + "...";
  }
  
  public void AppendColumn(string header) {
    if (header is null) {
      throw new ArgumentNullException("Загаловок добавляемой колонки не существует");
    }
    
    _columnHeaders.AddLast(header);
    _columnStrokes.AddLast(new string('-', header.Length));
    _columnSpaces.AddLast(new string(' ', header.Length));
  }

  public void AppendRow(IEnumerable<string> row) {
    if (row is null) {
      throw new ArgumentNullException("Добавляемая строка не существует");
    }

    if (row.Count() != _columnStrokes.Count) {
      throw new ArgumentOutOfRangeException(row.ToString(), 
        "Число элементов в переданной строке не соответствует числу ячеек в строке таблицы");
    }
    
    _rows.AddLast(new LinkedList<string>());

    LinkedListNode<string> node = _columnStrokes.First;
    foreach (var data in row) {
      _rows.Last.Value.AddLast(AdjustLeft(data, node.Value.Length));
      node = node.Next;
    }
  }

  private string PrintList(LinkedList<string> list) {
    string result = "|";

    foreach (var data in list) {
      result += data + "|";
    }
    
    return result;
  }

  public string Print() {
    string result = PrintList(_columnStrokes) + "\n" + PrintList(_columnSpaces) + "\n" + PrintList(_columnHeaders) 
                    + "\n" + PrintList(_columnSpaces) + "\n" + PrintList(_columnStrokes) + "\n";

    foreach (var row in _rows) {
      result += PrintList(row) + "\n" + PrintList(_columnStrokes) + "\n";
    }
    
    return result;
  }
}