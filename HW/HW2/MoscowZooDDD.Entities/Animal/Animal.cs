using System.Text;
using MoscowZooDDD.Entities.FeedSchedule;

namespace MoscowZooDDD.Entities.Animal;

public enum AnimalHealthState {
  Healthy = 0,
  Weak,
  Ill,
  Treated
}

public class Animal {
  public int Id { get; }
  public int EnclosureId { get; private set; }
  public string PreferredFoodType { get; }
  public AnimalBio Bio { get; }
  public AnimalBiologicalDescription Description { get; }
  public SortedSet<Schedule> FeedingTimeTable { get; }
  private SortedSet<Schedule> AccomplishedFeeds { get; }

  public Animal(int id, int enclosureId, string preferredFoodType, 
    AnimalBio bio, AnimalBiologicalDescription description, ICollection<Schedule> feedingTimeTable) {
    if (id < 0) {
      throw new ArgumentException("Уникальный номер животного должен быть положительным целым числом", nameof(id));
    }
    
    if (feedingTimeTable.Count == 0) {
      throw new ArgumentException("Животное нужно кормить хотя бы 1 раз в день", nameof(feedingTimeTable));
    }

    if (string.IsNullOrWhiteSpace(preferredFoodType)) {
      preferredFoodType = "-";
    }
    
    Id = id;
    EnclosureId = enclosureId;
    PreferredFoodType = preferredFoodType;
    
    Bio = bio;
    Description = description;
    
    FeedingTimeTable = new SortedSet<Schedule>(feedingTimeTable);
    AccomplishedFeeds = (SortedSet<Schedule>) [];
    
    HealthState = AnimalHealthState.Healthy;
  }
  public AnimalHealthState HealthState { get; private set; }

  public void ChangeHealthState(AnimalHealthState newState) {
    if (!Enum.IsDefined(typeof(AnimalHealthState), newState)) {
      throw new ArgumentException($"Данный код состояния здоровья [{newState}] не специфицирован и не поддерживается",
        nameof(newState));
    }
    
    HealthState = newState;
  }
  public void MoveToAnotherEnclosure(int newEnclosureId) {
    EnclosureId = newEnclosureId;
  }

  /// <summary>
  /// This method is used to change feeding schedule for animal. As is Schedules is VO, so its doesn't have any id
  /// and can't be changed or remove properly it only can be reworked fully. 
  /// Be aware of after it the animal have NO feeding schedule at all
  /// </summary>
  public void ClearFeedingTimeTable() {
    FeedingTimeTable.Clear();
    AccomplishedFeeds.Clear();
  }
  public void AddNewFeedingEvent(Schedule newFeedingEvent) {
    FeedingTimeTable.Add(newFeedingEvent);
  }
  
  public void PerformFirstPossibleFeeding() {
    if (FeedingTimeTable.Count == 0) {
      throw new Exception("Животное получило весь положенный корм");
    }
    
    AccomplishedFeeds.Add(FeedingTimeTable.Min()!);
    FeedingTimeTable.Remove(FeedingTimeTable.Min()!);
  }

  public void RebuildTimeTableForAnotherDay() {
    foreach (var completedFeeding in AccomplishedFeeds) {
      FeedingTimeTable.Add(completedFeeding);
    }
    
    AccomplishedFeeds.Clear();
  }

  public string GetDailyFeedingReport() {
    StringBuilder reportBuilder = new StringBuilder();
    
    foreach (var completedFeeding in AccomplishedFeeds) {
      reportBuilder.Append(completedFeeding);  
    }
    
    return reportBuilder.ToString();
  }

  public override string ToString() {
    return $"{Id};" + Description + $";{Bio};{Enum.GetName(typeof(AnimalHealthState), HealthState)}";
  }
}

// classes that divide species to predators and herbivores

public sealed class Predator : Animal {
  public Predator(int id, int enclosureId, string preferredFoodType, AnimalBio bio,
    AnimalBiologicalDescription description, ICollection<Schedule> feedingTimeTable) : 
    base(id, enclosureId, preferredFoodType, bio, description, feedingTimeTable) {}

  public override string ToString() {
    return base.ToString() + ";Хищник" + Environment.NewLine;
  }
}

public sealed class Herbivore : Animal {
  public Herbivore(int id, int enclosureId, string preferredFoodType, AnimalBio bio,
    AnimalBiologicalDescription description, ICollection<Schedule> feedingTimeTable) :
    base(id, enclosureId, preferredFoodType, bio, description, feedingTimeTable) {}

  public override string ToString() {
    return base.ToString() + ";Травоядное" + Environment.NewLine;
  }
}