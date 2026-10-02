using System.Security.Cryptography.X509Certificates;
//practice
public class BankAccount
{
    private readonly string _accountId;
    private string _ownerName;
    private decimal _balance;
    private readonly List<string> _transactionHistory = new();

    public string AccountId => _accountId;
    public string OwnerName => _ownerName;
    public decimal Balance => _balance;

    public BankAccount(string ownerName, decimal initialDeposit)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("名義人名は必須です");
        if (initialDeposit < 0)
            throw new ArgumentOutOfRangeException(nameof(initialDeposit), "初期入金額は0以上にしてください");

        _accountId = Guid.NewGuid().ToString("N")[..8].ToUpper();
        _ownerName = ownerName;
        _balance = initialDeposit;
        RecordTransaction($"口座開設: ¥{initialDeposit:N0}");
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        _balance += amount;
        RecordTransaction($"入金: ¥{amount:N0} → 残高: ¥{_balance:N0}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        _balance -= amount;
        RecordTransaction($"出金: ¥{amount:N0} → 残高: ¥{_balance:N0}");
    }

    private void RecordTransaction(string description)
    {
        _transactionHistory.Add($"[{DateTime.Now:yyyy/MM/dd HH:mm}] {description}");
    }

    public IReadOnlyList<string> GetHistory() => _transactionHistory.AsReadOnly();
}

//タスク２

public abstract class LibraryItem
{
    public string Taitle {  get; set; }
    public int Number { get; set; }
    public bool Take { get; set; }

    public List<string> book = new();

    public LibraryItem(string taitle, int number,bool take)
    {
        Taitle = taitle;
        Number = number;
        Take = take;

    }
   
     public  virtual string Getinfo() => $"{Taitle}:{Number}"; 

    public void ListItem(string Getinfo)
    {
        book.Add(Getinfo);
    }
    public List<string>Library()=>book;  //       入力されたのをLibraryに追加

}
public class Book:LibraryItem
{
    public string Name {  get; set; }
    public int Page { get; set; }
    public Book(string taitle, int number, bool take, string name, int page) : base(taitle, number,take)
    {
        Name = name;
        Page = page;
    }
    public override string Getinfo()=>base.Getinfo();
    
    

}
public class DVD:LibraryItem
{
    public string Name { get; set; }
    public int Page { get; set; }
    public DVD(string taitle, int number, bool take,string name,int page):base( taitle, number,  take)
    {
        Name=name;
        Page = page;
    }
}
public interface IBorrowable  //他の人や組織から一時的に使用するために貸し出し可能な物事
{
    void Borrow(string borrowerName);
    void Return();
}








public class output
{
    public static void  Main()
    {
        BankAccount bank = new BankAccount("gotou",1000);

        bank.Deposit(100);
        bank.Withdraw(50);
       
     
        foreach (string n in bank.GetHistory())
        {
            Console.WriteLine(n);
        }

        Book bo = new Book("a",2,true,"g",4);
        
        

    }
    
}