namespace Domain.Enclosure;
using Animal;

public class AbstractEnclosure {
  public Guid Id { get; set; }
  public AnimalTypes CurrentAnimalType { get; set; }
  public int CurrentAnimalsInside { get; set; }
  
  public EnclosureState State { get; set; }

  public AbstractEnclosure(EnclosureState state) {
    Id = Guid.NewGuid();
    CurrentAnimalsInside = 0;
    
    CurrentAnimalType = AnimalTypes.Neutral;
    State = state;
  }

  public override string ToString() {
    return $"{Id};{CurrentAnimalsInside};{State.MaxAnimalsCount};{Enum.GetName(CurrentAnimalType)};";
  }
}

public sealed class Cage : AbstractEnclosure {
  public Cage(EnclosureState state) :
  base( state) {
  }

  public override string ToString() {
    return base.ToString() + "Клетка" + Environment.NewLine;
  }
}

public sealed class Enclosure : AbstractEnclosure {
  public Enclosure( EnclosureState state) :
    base( state) {
  }

  public override string ToString() {
    return base.ToString() + "Вольер" + Environment.NewLine;
  }
}

public sealed class Aquarium : AbstractEnclosure {
  public Aquarium(EnclosureState state) :
    base(state) {
  }

  public override string ToString() {
    return base.ToString() + "Аквариум" + Environment.NewLine;
  }
}

public sealed class Terrarium : AbstractEnclosure {
  public Terrarium (EnclosureState state) :
    base(state) {
  }

  public override string ToString() {
    return base.ToString() + "Террариум" + Environment.NewLine;
  }
}