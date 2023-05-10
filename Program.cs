namespace SQLManager; 

static class Program {
    private const string ConnectionString = @"***";

    private static readonly SqlManager SqlManager = new (ConnectionString);
    
    static void Main(string[] args) {
        SqlManager.MessageReceived += SqlManagerOnMessageReceived;
        
        HandleSqlCommand(@"SELECT * FROM Suppliers");

        HandleSqlCommand(@"INSERT INTO Suppliers (name) VALUES ('Chinese brand')");
        
        HandleSqlCommand(@"SELECT * FROM Suppliers");
        
        HandleSqlCommand(@"UPDATE Suppliers 
                           SET name = 'unknown brand'
                           WHERE id = (SELECT max(id) FROM Suppliers)");
        
        HandleSqlCommand(@"SELECT * FROM Suppliers");
        
        HandleSqlCommand(@"DELETE FROM Suppliers 
                           WHERE id = (SELECT max(id) 
                           FROM Suppliers)");
        
        HandleSqlCommand(@"SELECT * FROM Suppliers");
    }
    

    private static void HandleSqlCommand(string command) {
        var response = SqlManager.Fetch(command);

        if (response == null) return;
        
        var result = TextFormatter.FormatToTable(response);

        var line = string.Concat(Enumerable.Repeat("-", result.IndexOf('\n')));
        
        Console.WriteLine(line);
        Console.WriteLine(result);
        Console.WriteLine(line);
    }

    private static void SqlManagerOnMessageReceived(object sender, MessageHandlerArgs args) {
        Console.WriteLine($"{args.Time}|{args.Message}");
    }
}