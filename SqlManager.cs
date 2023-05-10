using System.Data.SqlClient;

namespace SQLManager; 

public class SqlManager {
    readonly string _connectionString;

    public delegate void MessageHandler(object sender, MessageHandlerArgs args);
        
    public event MessageHandler? MessageReceived;
        
    public SqlManager(string connectionString) {
        _connectionString = connectionString;
    }
        
    public IEnumerable<object[]>? Fetch(string sqlCommand) {
        var values = new List<object[]>();

        try {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            MessageReceived?.Invoke(this, new MessageHandlerArgs("Connected."));
                
            var command = new SqlCommand(sqlCommand, connection);
            MessageReceived?.Invoke(this, new MessageHandlerArgs("Trying to execute command."));
            var reader = command.ExecuteReader();
            MessageReceived?.Invoke(this, new MessageHandlerArgs("Executed."));

            MessageReceived?.Invoke(this, new MessageHandlerArgs($"Records affected: {reader.RecordsAffected.ToString()}"));
                
            if(!reader.HasRows) throw new Exception("Response empty.");
                
            while (reader.Read()) {
                var value = new object[reader.FieldCount];
                reader.GetValues(value);
                values.Add(value);
            }
                
            MessageReceived?.Invoke(this, new MessageHandlerArgs("Information received."));
        }
        catch (Exception e) {
            MessageReceived?.Invoke(this, new MessageHandlerArgs($"{e.Message}"));
            values = null;
        }
        finally {
            MessageReceived?.Invoke(this, new MessageHandlerArgs($"Disconnected."));
        }

        return values;
    }
}