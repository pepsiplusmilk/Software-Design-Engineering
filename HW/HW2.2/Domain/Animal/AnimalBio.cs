namespace Domain.Animal;

public enum AnimalGender {
  Male = 1,
  Female = 2,
  Unspecified = -1
}

/// <summary>
/// The value object that contains information that personalize every animal
/// </summary>
public class AnimalBio {
  public string Nickname { get; }
  public DateOnly BirthDate { get; }
  public string PreferredFood { get; }
  public AnimalGender Gender { get; }

  public AnimalBio(string nickname, DateOnly birthDate, string preferredFood, AnimalGender gender) {
    if (nickname == string.Empty) {
      nickname = "-";
    }

    if (preferredFood == string.Empty) {
      preferredFood = "-";
    }
    
    Nickname = nickname;
    BirthDate = birthDate;
    PreferredFood = preferredFood;
    Gender = gender;
  }
}