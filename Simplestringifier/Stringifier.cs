using System.Reflection;
using System.Text;

namespace SimpleStringifier;

public class Stringifier
{
    private readonly StringBuilder _stringBuilder = new();

    public string Stringify(object source)
    {
        try
        {
            StringifyObject(source, 0);

            return _stringBuilder.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void StringifyObject(object? source, int indent)
    {
        if (source is null)
        {
            _stringBuilder.AppendLine("----------------------------------------");
            return;
        }

        var sourceType = source.GetType();
        _stringBuilder.AppendLine($"{GetIndent(indent)}Object of Class \"{sourceType.Name}\"");
        _stringBuilder.AppendLine("----------------------------------------");

        var props = new List<PropertyInfo>(sourceType.GetProperties());

        foreach (var prop in props)
        {
            var value = sourceType.GetProperty(prop.Name)?.GetValue(source, null);

            if (IsPrimitiveOrString(value))
            {
                _stringBuilder.AppendLine($"{GetIndent(indent + 1)}{prop.Name} = {value?.ToString()}");
            }
            else
            {
                _stringBuilder.AppendLine($"{GetIndent(indent + 1)}{prop.Name}");
                StringifyObject(value, indent + 1);
            }
        }
    }

    private string GetIndent(int indentIndex) 
        => string.Concat(Enumerable.Repeat(" ", indentIndex * 4));

    private bool IsPrimitiveOrString(object? value)
        => value == null || value.GetType().IsPrimitive || value is string;
}