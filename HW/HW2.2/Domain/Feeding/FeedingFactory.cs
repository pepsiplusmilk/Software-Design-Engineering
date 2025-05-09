namespace Domain.Feeding;

public class FeedingFactory {
  public Feeding? Create(Guid animalId, string foodDescription, TimeOnly optimalTime) {
    if (foodDescription == String.Empty) {
      foodDescription = "-";
    }
    
    return new Feeding(animalId, foodDescription, optimalTime);
  }
}