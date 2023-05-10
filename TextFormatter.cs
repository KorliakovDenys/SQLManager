namespace SQLManager; 

public static class TextFormatter {
    public static string FormatToTable(object data) {
        var table = string.Empty;

        if (data is not IEnumerable<object[]> objects) return null;

        var objectsEnumerable = objects.ToList();
            
        var longestWordLength = (from obj in objectsEnumerable from el in obj select el.ToString().Length).Prepend(0).Max();

        foreach (var obj in objectsEnumerable) {
            table = obj.Aggregate(table, (current, el) => current + el.ToString()?.PadRight(longestWordLength + 4, ' '));
            table += '\n';
        }
            
        return table;
    }
}