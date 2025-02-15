using System.Runtime;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace MoscowZooERP;
using AnimalHierarchy;
using CommandProcessing;
using ThingsHierarchy;
using TableFormatting;

class ClientHandler {
  private static MoscowZoo zoo;
  
  private static IServiceProvider serviceProvider;
  
  private static Matcher<MoscowZooConsoleOperations> defaultConsoleOperations;
  
  private static Dictionary<string, AbstractEgg> create;
  private static Dictionary<string, ThingsBuilder> buy;

  private static ConsoleTable animalTable, touchAbleTable, thingsTable;
  static MoscowZooConsoleOperations? Parse(string? input) {
    MoscowZooConsoleOperations? operation = null;
    
    try {
      operation = defaultConsoleOperations.Parse(input);
    }
    catch (ArgumentNullException exp) {
      Console.WriteLine(exp.Message);
      return null;
    }
    catch (AmbiguousImplementationException exp) {
      Console.WriteLine(exp.Message);
      return null;
    }
    catch (ArgumentException exp) {
      Console.WriteLine(exp.Message);
      return null;
    }

    return operation;
  }
  
  static void PrintAnimals() {
    Console.WriteLine(animalTable.Print());
  }

  static void PrintSummary() {
    Console.WriteLine($"Всего животных на данный момент в зоопарке: {zoo.GetAnimalCount()}");
    Console.WriteLine($"Дневное потребление корма(в кг) составляет: {zoo.GetDailyFeedAmount()}");
  }

  static void PrintTouchable() {
    Console.WriteLine(touchAbleTable.Print());
  }

  static void Add(string input) {
    var args = input.Split(' ');
    // [0] => zoo
    // [1] => add
    // [2] => Тип животного
    // [3] => Кличка животного
    // [4] => Кол-во корма
    // [5] => Статус по болезни
    // [6] => Агрессивность

    Animal animal = create[args[2]].CreateAnimal();

    if (args[3] == "-") {
      animal.Nickname = null;
    } else {
      animal.Nickname = args[3];
    }

    try {
      animal.RequiersFoodAmount = int.Parse(args[4]);
    }
    catch (ArgumentException exp) {
      Console.WriteLine(exp.Message);
      return;
    }
    
    animal.Illness = (args[5] == "ill");

    bool isKind = false;
    int relateScaleValue = 0;
    
    try {
      if (animal is Herbivore herbivore) {
        relateScaleValue = herbivore.Kindness = (args[6] == "-") ? 5 : Int32.Parse(args[6]);
        
        if (herbivore.Kindness > 5) {
          isKind = true;
        }
      } else if (animal is Predator predator) {
        relateScaleValue = predator.Agression = (args[6] == "-") ? 5 : Int32.Parse(args[6]);
      }
    }
    catch (ArgumentException exp) {
      Console.WriteLine(exp.Message);
      return;
    }

    try {
      var result = zoo.AdoptAnimal(animal);

      if (result) {
        Console.WriteLine($"Животное {animal.Nickname} добавлено в зоопарк!");
        
        animalTable.AppendRow([animal.GetType().Name, animal.Nickname, animal.RequiersFoodAmount.ToString(), 
          relateScaleValue.ToString(), animal.Illness.ToString()]);

        if (isKind) {
          touchAbleTable.AppendRow([animal.GetType().Name, animal.Nickname, animal.RequiersFoodAmount.ToString(), 
            relateScaleValue.ToString(), animal.Illness.ToString()]);
        }
      } else {
        Console.WriteLine($"Животное {animal.Nickname} помещено в карантинную зону ветеринарной клиники, по " +
                          $"состоянию здоровья");
      }
    }
    catch (ArgumentNullException exp) {
      Console.WriteLine(exp.Message);
    }
  }

  static void Buy(string input) {
    var args = input.Split(' ');
    // [0] => zoo
    // [1] => buy
    // [2] => Название
    // [3] => Тип
    // [4] => Инвентарный номер

    Thing thing = buy[args[3]].Build();

    thing.Label = args[2];
    
    try {
      thing.Id = int.Parse(args[4]);
    }
    catch (ArgumentException exp) {
      Console.WriteLine(exp.Message);
      return;
    }

    try {
      var result = zoo.Add(thing);

      if (result) {
        Console.WriteLine($"Купленная вещь с артикулом {thing.Label} успешно поставлена на учет.");
        thingsTable.AppendRow([thing.Label, thing.GetType().Name, thing.Id.ToString(),]);
      }
      else {
        Console.WriteLine($"Возникли сложности с постановкой вещи. Возможно она могла быть неисправна.");
      }
    }
    catch (ArgumentNullException exp){
      Console.WriteLine(exp.Message);
    }
  }

  static void PrintObjects() {
    Console.WriteLine(thingsTable.Print());
  }
  static bool Decide(string? input) {
    var type = Parse(input);
    
    if (type is null) {
      return true;
    }

    if (type == MoscowZooConsoleOperations.Exit) {
      return false;
    }
    
    if (type == MoscowZooConsoleOperations.Help) {
      Console.WriteLine("Все воспринимаемые команды начинаются со слова \'zoo\' и содержат помимо него в себе " +
                        "еще одно слово, разделенное пробелом. Ниже приведен список допустимых ключевых слов.");
      Console.WriteLine("\'help\' - вызывает справку, которую вы сейчас читаете");
      Console.WriteLine("\'exit\' - выходит из программы");
      Console.WriteLine("\'add\' - добавляет нового животного в зоопарк. Команда имеет несколько аргументов, " +
                        "которые нужно указать в следующем порядке, через пробельные разделители: \n" +
                        "- Вид животного, обязательный аргумент, на данный момент Московский зоопрак " +
                        "принимает: {monkey, rabbit, tiger, wolf}\n" +
                        "- Кличка животного, можно опустить(для этого оставьте знак \'-\')\n" +
                        "- Количество килограмм корма, потребляемых в день. Обязательный параметр. Ожидается " +
                        "целое число, строго большее 0\n" +
                        "- Болеет ли животное. Обязательный аргумент. Принимаются ответы в виде: " +
                        "[ill/healthy]\n" +
                        "- Агрессивность(у хищников)/Доброта(у травоядных) живнотного. " +
                        "Опциональный аргумент(пропуск это \'-\'). Ожидаются " +
                        "целые числа от 0 до 10 в качестве значений. Значение по умолчанию - 5." +
                        "\nАргументы чувствительны к регистру. Клички могут повторяться.");
      Console.WriteLine("\'animal_table\' - выводит на экран список всех животных, состоящих на " +
                        "учете в зоопарке, и их базовые свойства.");
      Console.WriteLine("\'animal_totals\' - выводит количество животных в зоопарке, и общее" +
                        " количетство потребляемого корма(в кг)");
      Console.WriteLine("\'object_table\' - выводит на экран список всех объектов, подлежащих инвентаризации в" +
                        " зоопарке.");
      Console.WriteLine("\'touchable\' - выводит список животных, которые могут быть отправлены в контактную" +
                        " секцию зоопарка.");
      Console.WriteLine("\'buy\' - закупить предмет для зоопарка. Команда должна сопровождаться следующими " +
                        "атрибутами, в указанном порядке:\n" +
                        "- Название товара, обязательный аргумент\n" +
                        "- Тип товара, на данный момент Московский зоопарк закупает следующие товары: " +
                        "{table, computer}\n" +
                        "- Инвентарный номер, целое неотрицательное число");
    } else if (type == MoscowZooConsoleOperations.Add) {
      input = Regex.Replace(input, @"\s+", " ");
      Add(input);
    } else if (type == MoscowZooConsoleOperations.Buy) {
      input = Regex.Replace(input, @"\s+", " ");
      Buy(input);
    } else if (type == MoscowZooConsoleOperations.AnimalInfo) {
      PrintAnimals();
    } else if (type == MoscowZooConsoleOperations.AnimalSummary) {
      PrintSummary();
    } else if (type == MoscowZooConsoleOperations.ObjectsInfo) {
      PrintObjects();
    } else if (type == MoscowZooConsoleOperations.TouchedList) {
      PrintTouchable();
    }

    return true;
  }
  
  static void Main(string[] args) {
    Console.WriteLine("Добро пожаловать в программу управления Московским Зоопарком!");
    Console.WriteLine("Для того что бы узнать полный список доступных действий, введите: zoo help");

    defaultConsoleOperations = new Matcher<MoscowZooConsoleOperations> {
      Commands = [
        new HelpCovering(), new AddCovering(), new BuyCovering(), new AnimalInfoCovering(), new AnimalSummaryCovering(),
        new ObjectsInfoCovering(), new TouchedListCovering(), new ExitCovering()
      ]
    };
    
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddSingleton<MoscowZoo>();
    serviceCollection.AddSingleton<VeterinaryClinic>();
    
    serviceCollection.AddSingleton<TigerEgg>();
    serviceCollection.AddSingleton<WolfEgg>();
    serviceCollection.AddSingleton<MonkeyEgg>();
    serviceCollection.AddSingleton<RabbitEgg>();

    serviceCollection.AddSingleton<TableBuilder>();
    serviceCollection.AddSingleton<ComputersBuilder>();
    
    serviceProvider = serviceCollection.BuildServiceProvider();

    zoo = serviceProvider.GetService<MoscowZoo>();
    
    create = new Dictionary<string, AbstractEgg> {
      ["monkey"] = serviceProvider.GetService<MonkeyEgg>(),
      ["rabbit"] = serviceProvider.GetService<RabbitEgg>(),
      ["tiger"] = serviceProvider.GetService<TigerEgg>(),
      ["wolf"] = serviceProvider.GetService<WolfEgg>()
    };

    buy = new Dictionary<string, ThingsBuilder> {
      ["table"] = serviceProvider.GetService<TableBuilder>(),
      ["computer"] = serviceProvider.GetService<ComputersBuilder>()
    };

    animalTable = new ConsoleTable();
    touchAbleTable = new ConsoleTable();
    thingsTable = new ConsoleTable();
    
    animalTable.AppendColumn(" Тип животного ");
    animalTable.AppendColumn(" Кличка Животного ");
    animalTable.AppendColumn(" Количество еды (кг) ");
    animalTable.AppendColumn(" Доброта/Агрессивность ");
    animalTable.AppendColumn(" Болеет? ");
    
    touchAbleTable.AppendColumn(" Тип животного ");
    touchAbleTable.AppendColumn(" Кличка Животного ");
    touchAbleTable.AppendColumn(" Количество еды (кг) ");
    touchAbleTable.AppendColumn(" Доброта/Агрессивность ");
    touchAbleTable.AppendColumn(" Болеет? ");
    
    thingsTable.AppendColumn(" Название товара ");
    thingsTable.AppendColumn(" Тип товара ");
    thingsTable.AppendColumn(" Инвентарный номер ");

    string? input;

    do {
      input = Console.ReadLine();
    } while (Decide(input));
  }
}