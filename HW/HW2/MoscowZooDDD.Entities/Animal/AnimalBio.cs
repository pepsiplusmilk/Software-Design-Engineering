using System.Data;

namespace MoscowZooDDD.Entities.Animal;

public enum AnimalGender {
  Female = 0,
  Male = 1,
  Unspecified = -1
}

public sealed record AnimalBio {
  public string Nickname { get; }
  public DateOnly BirthDate { get; }
  public AnimalGender Gender { get; }

  public AnimalBio(string nickname, DateOnly birthDate, AnimalGender gender) {
    if (string.IsNullOrEmpty(nickname)) {
      throw new ArgumentException("Кличка животного не может быть пустой", nameof(nickname));
    }

    if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow.Date)) {
      throw new ArgumentException("Дата рождения животного еще не наступила", nameof(birthDate));
    }

    if (!Enum.IsDefined(typeof(AnimalGender), gender)) {
      throw new ArgumentException("Обработка данного гендера не поддерживается базой данных", nameof(gender));
    }
    
    Nickname = nickname;
    BirthDate = birthDate;
    Gender = gender;
  }

  public override string ToString() {
    return $"{Nickname};{BirthDate};{Enum.GetName(typeof(AnimalGender), Gender)}";
  } 
}