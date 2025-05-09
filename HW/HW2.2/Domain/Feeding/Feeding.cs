namespace Domain.Feeding;

public class Feeding {
  public Guid Id { get; }
  
  public Guid AnimalId { get; }
  public string FoodDescription { get; set; }
  public TimeOnly OptimalFeedingTime { get; set; }
  public bool IsCompleted { get; set; }

  public Feeding(Guid animalId, string foodDescription, TimeOnly optimalFeedingTime) {
    Id = Guid.NewGuid();
    
    AnimalId = animalId;
    FoodDescription = foodDescription;
    OptimalFeedingTime = optimalFeedingTime;
    
    IsCompleted = false;
  }

  public override string ToString() {
    // Real feeding time attached in event handler
    return $"{Id};{AnimalId};{FoodDescription};{OptimalFeedingTime};";
  }
}