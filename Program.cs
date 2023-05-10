namespace ADO.NET_HW_2; 

static class Program {
    private const string ConnectionString =
        @"***";

    private static readonly SqlManager SqlManager = new (ConnectionString);
    
    static void Main(string[] args) {
        SqlManager.MessageReceived += SqlManagerOnMessageReceived;
        
        HandleSqlCommand("SELECT * FROM Goods");
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