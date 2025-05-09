namespace Domain.Enclosure;

public class EnclosureState {
  public int MaxAnimalsCount { get; }

  public EnclosureState(int maxAnimalsCount) {
    MaxAnimalsCount = maxAnimalsCount;
  }
}