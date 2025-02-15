namespace MoscowZooERP.ThingsHierarchy;

public class Computer : Thing {
 
}

public sealed class ComputersBuilder : ThingsBuilder {
  public override Computer Build() {
    return new Computer();
  }
}