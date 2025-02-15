namespace MoscowZooERP.AnimalHierarchy;

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
    set {
      if (value is null || value.Length == 0) {
        nickname = null;
      }
      
      nickname = value;
    }

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