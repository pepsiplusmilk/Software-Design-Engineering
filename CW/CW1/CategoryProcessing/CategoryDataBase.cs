namespace FinanceModule.CategoryProcessing;

public class CategoryDataBase : IRegisterService<Category> {
  private Dictionary<Int64, Category> _categories = new Dictionary<Int64, Category>();
  
  public void Register(Category category) {
    if (!_categories.TryAdd(category.Id, category)) {
      throw new ArgumentException($"There is already a category with id {category.Id}");
    }
  }

  public bool Unregister(Category category) {
    return _categories.Remove(category.Id);
  }
}