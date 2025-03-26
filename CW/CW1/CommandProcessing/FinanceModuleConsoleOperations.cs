using System.Text.RegularExpressions;

namespace FinanceModule.CommandProcessing;

// Все возможные действия которые мы можем совершить в консоли
public enum FinanceProcessingOperations {
  GetAccountBalance,
  GetAccountName,
  ChangeAccountName,
  PerformOperation,
  CreateCategory,
  DeleteCategory,
  CreateAccount,
  DeleteAccount,
  Help,
  Exit
}

public class GetAccountBalanceCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-c gab \d+$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.GetAccountBalance;
  }
}

public class GetAccountNameCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-c gnm \d+$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.GetAccountName;
  }
}

public class ChangeAccountNameCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-c chnm \d+ \S+$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.ChangeAccountName;
  }
}

public class PerformOperationCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-c perf \d+ \d+ \w+(| \S+)$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.PerformOperation;
  }
}

public class CreateAccountCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-m mkacc \S+ (0|-\d+)$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.CreateAccount;
  }
}

public class DeleteAccountCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-m dlacc \d+$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.DeleteAccount;
  }
}

public class CreateCategoryCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-m mkcat \S+ (1|0)$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.CreateCategory;
  }
}

public class DeleteCategoryCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-m dlcat \d+$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.DeleteCategory;
  }
}

public class HelpCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-h$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.Help;
  }
}

public class ExitCovering : ICmdCommand<FinanceProcessingOperations> {
  public bool Check(string text) {
    return Regex.IsMatch(text, @"^-q$");
  }

  public FinanceProcessingOperations ReturnCode() {
    return FinanceProcessingOperations.Exit;
  }
}