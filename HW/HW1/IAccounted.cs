namespace MoscowZooERP;
using ThingsHierarchy;

public interface IAccounted {
  public bool Add(Thing? thing);
  public Thing this[int index] { get;}
}