namespace MoscowZooDDD.Entities.FeedSchedule;

public class Schedule : IComparable<Schedule> {
  public int AnimalId { get; }
  public TimeOnly FeedingTime { get; }
  public string FoodDescription { get; }
  public double FoodWeight { get; }

  public Schedule(int animalId, TimeOnly feedingTime, string foodDescription, double foodWeight) {
    if (animalId < 0) {
      throw new ArgumentException("Животных с отрицательным уникальным номером в зоопарке " +
                                  "не существует", nameof(animalId));
    }

    if (string.IsNullOrEmpty(foodDescription)) {
      throw new ArgumentException("Тип корма должен быть обязательно указан", nameof(foodDescription));
    }

    if (foodWeight < 0) {
      throw new ArgumentException("Вес пищи для кормежки должен быть положительным числом", nameof(foodWeight));
    }
    
    AnimalId = animalId;
    FeedingTime = feedingTime;
    FoodDescription = foodDescription;
    FoodWeight = foodWeight;
  }

  public int CompareTo(Schedule? other) {
    return FeedingTime.CompareTo(other?.FeedingTime);
  }

  /// <summary>
  /// Process information to .csv format with ';' separator
  /// </summary>
  /// <returns> String that holds one line of .csv table </returns>
  public override string? ToString() {
    return $"{AnimalId};{FeedingTime};{FoodDescription};{FoodWeight}" + Environment.NewLine;
  }
}