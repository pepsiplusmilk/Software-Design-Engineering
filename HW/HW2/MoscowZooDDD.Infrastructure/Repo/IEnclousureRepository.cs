namespace MoscowZooDDD.Infrastructure.Repo;
using MoscowZooDDD.Entities.Enclosure;
using MoscowZooDDD.Entities.Animal;

public interface IEnclosureRepository {
  public Enclosure GetEnclosure(int enclosureId);
  public void AddEnclosure(Enclosure enclosure);
  public void RemoveEnclosure(int enclosureId);
  public int GetNextFreeEnclosureId();
  public void UpdateStatus(int enclosureId, InhabitantsType newType);
}