namespace MoscowZooERP.AnimalHierarchy;

/// <summary>
/// Класс предоставляющий функционал живого существа
/// </summary>
public class Animal : ILiveCreature {
  protected int FoodRequierement;
  protected string? nickname;
  protected bool illness;
  
  public int RequiersFoodAmount {
    set {
      if (value <= 0) {
        string? subsString = Nickname;
        
        throw new ArgumentException($"Животное {subsString} не может есть неположительное количество килограмм корма.");
      }
      
      FoodRequierement = value;
    }

    get {
      return FoodRequierement;
    }
  }

  public string? Nickname {
    // Если передана пустая строка или null строка то сохраняем как null
    set {
      if (value is null || value.Length == 0) {
        nickname = null;
      }
      
      nickname = value;
    }

    // Далее проверяем что выражение null и если да возвращаем ""
    get => (nickname is null) ? "" : nickname;
  }

  public bool Illness {
    set;
    get;
  }

  public virtual string Behave() {
    throw new NotImplementedException("Поведение недетерменированно, укажите более конкретный тип животного.");
  }
}