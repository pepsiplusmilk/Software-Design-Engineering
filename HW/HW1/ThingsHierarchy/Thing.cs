namespace MoscowZooERP.ThingsHierarchy;

public class Thing : IInventorizedObject {
   private int id;
   private string label;
  
   public int Id {
     get => id;
  
     set {
       if (value < 0) {
         throw new ArgumentException("Инвентарный номер не может принимать отрицательные значения.");
       }
        
       id = value;
     }
   }

   public string? Label {
     get => label;

     set {
       if (value is null) {
         label = string.Empty;
       }
       
       label = value;
     }
   }
}

public abstract class ThingsBuilder {
  public virtual Thing Build() {
    return new Thing();
  }
}