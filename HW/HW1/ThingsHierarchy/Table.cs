namespace MoscowZooERP.ThingsHierarchy;

public class Table : Thing {

}

public sealed class TableBuilder : ThingsBuilder {
  public override Table Build() {
    return new Table();
  }
}