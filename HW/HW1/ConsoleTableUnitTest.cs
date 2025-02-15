using MoscowZooERP.TableFormatting;
using Moq;

namespace MoscowZooERP.Tests;

public class ConsoleTableUnitTest {
  [Fact]
  public void AdjustTest() {
    string str = "adahsjsjdfhsdajfhjfgsdjd";
    int len = 5;
    
    Assert.True(ConsoleTable.AdjustLeft(str, 5) == "ad...");
    Assert.False(ConsoleTable.AdjustLeft(str, 5) == "adahs");
  }
  
  [Fact]
  public void AdjustTest2() {
    string str = "x";
    int len = 100;
    
    Assert.False(ConsoleTable.AdjustLeft(str, len) == "x");
    Assert.True(ConsoleTable.AdjustLeft(str, len).Length == len);
  }

  [Fact]
  public void AppendColumnNullTest() {
    ConsoleTable table = new ConsoleTable();
    
    Assert.Throws<ArgumentNullException>(
      () => table.AppendColumn((string)null));
  }
  
  [Fact]
  public void AppendRowNullTest() {
    ConsoleTable table = new ConsoleTable();
    
    Assert.Throws<ArgumentNullException>(
      () => table.AppendRow((IEnumerable<string>)null));
  }

  [Fact]
  public void AppendRowMismatchTest() {
    List<string> row = ["a", "b"];
    ConsoleTable table = new ConsoleTable();
    
    table.AppendColumn("xzxxx");
    table.AppendColumn("jkjkosafdio");
    table.AppendColumn("jkdfjkffjk");

    Assert.Throws<ArgumentOutOfRangeException>(
      () => table.AppendRow(row));
  }

  [Fact]
  public void PrintNullityTest() {
    ConsoleTable table = new ConsoleTable();
    
    Assert.Equal("", table.Print());
  }
}