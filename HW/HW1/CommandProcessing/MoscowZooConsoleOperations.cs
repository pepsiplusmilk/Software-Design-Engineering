using System.Text.RegularExpressions;
using MoscowZooERP.AnimalHierarchy;
using MoscowZooERP.ThingsHierarchy;

namespace MoscowZooERP.CommandProcessing;

// Все возможные действия которые мы можем совершить в консоли
public enum MoscowZooConsoleOperations {
  Help,
  Add,
  Buy,
  AnimalInfo,
  AnimalSummary,
  ObjectsInfo,
  TouchedList,
  Exit, 
}

public class HelpCovering : ICommand<MoscowZooConsoleOperations> {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo help ?$");
    
  }

  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.Help;
  }
}

public class AddCovering : ICommand<MoscowZooConsoleOperations>, IOperatedTypesOfCreatures {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo add " + IOperatedTypesOfCreatures.TypesGroup + 
                               @" (-|\S+) -?\d+ (ill|healthy) (-|-?\d+) ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.Add;
  }
}

public class BuyCovering : ICommand<MoscowZooConsoleOperations>, IOperatedTypesOfThings {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo buy .+ " + IOperatedTypesOfThings.ThingsGroup + 
                               @" -?\d+ ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.Buy;
  }
}

public class AnimalInfoCovering : ICommand<MoscowZooConsoleOperations> {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo animal_table ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.AnimalInfo;
  }
}

public class AnimalSummaryCovering : ICommand<MoscowZooConsoleOperations> {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo animal_totals ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.AnimalSummary;
  }
}

public class ObjectsInfoCovering : ICommand<MoscowZooConsoleOperations> {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo object_table ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.ObjectsInfo;
  }
}

public class TouchedListCovering : ICommand<MoscowZooConsoleOperations> {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo touchable ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.TouchedList;
  }
}

public class ExitCovering : ICommand<MoscowZooConsoleOperations> {
  public bool Check(string text) {
    text = Regex.Replace(text, @"\s+", " ");
    return Regex.IsMatch(text, @"^ ?zoo exit ?$");
  }
  
  public MoscowZooConsoleOperations ReturnCode() {
    return MoscowZooConsoleOperations.Exit;
  }
}