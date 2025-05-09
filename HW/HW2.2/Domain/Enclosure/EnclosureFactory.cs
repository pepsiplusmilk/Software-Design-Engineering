using Domain.Animal;

namespace Domain.Enclosure;

public abstract class AbstractEnclosureFactory {
  public abstract AbstractEnclosure? CreateEnclosure(int maxAnimalsCount);
}

public class EnclosureFactory : AbstractEnclosureFactory {
  public override AbstractEnclosure? CreateEnclosure(int maxAnimalsCount) {
    if (maxAnimalsCount < 1) {
      return null;
    }

    EnclosureState newEnclosureState = new EnclosureState(maxAnimalsCount);
    return new Enclosure(newEnclosureState);
  }
}

public class CageFactory : AbstractEnclosureFactory {
  public override AbstractEnclosure? CreateEnclosure(int maxAnimalsCount) {
    if (maxAnimalsCount < 1) {
      return null;
    }

    EnclosureState newEnclosureState = new EnclosureState(maxAnimalsCount);
    return new Cage(newEnclosureState);
  }
}

public class AquariumFactory : AbstractEnclosureFactory {
  public override AbstractEnclosure? CreateEnclosure(int maxAnimalsCount) {
    if (maxAnimalsCount < 1) {
      return null;
    }

    EnclosureState newEnclosureState = new EnclosureState(maxAnimalsCount);
    return new Aquarium(newEnclosureState);
  }
}

public class TerrariumFactory : AbstractEnclosureFactory {
  public override AbstractEnclosure? CreateEnclosure(int maxAnimalsCount) {
    if (maxAnimalsCount < 1) {
      return null;
    }

    EnclosureState newEnclosureState = new EnclosureState(maxAnimalsCount);
    return new Terrarium(newEnclosureState);
  }
}