namespace Domain.Animal;


public class AnimalState {
  public string Specie { get; }
  public AnimalState(string specie) {
    Specie = specie;
  }
}