using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleModNamespace
{
    internal class PluginConfig1
    {
        public Dictionary<string, CustomClass1> CustomDictionary = new Dictionary<string, CustomClass1>
        {
            ["first entry of custom dictionary"] = new CustomClass1("First entry of custom dictionary", 6, 45f),
            ["Second key of custom dictionary"] = new CustomClass1("Second entry of custom dictionary", 8, 9.4f)
        };

        public List<CustomClass1> CustomList = new List<CustomClass1>
        {
            new CustomClass1("First custom list value", 12, 67.5f),
            new CustomClass1("Second custom list value", 64, 12.1f)
        };

        public string FirstOption = "First Value";

        public Dictionary<string, float> RegularDictionary = new Dictionary<string, float>
        {
            ["first entry of regular dictionary"] = 1f,
            ["Second key of regular dictionary"] = 2f
        };

        public List<string> RegularList = new List<string>
        {
            "first list value",
            "Second list value"
        };

        public int SecondOption = 42;
        public float ThirdOption = 0.73f;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"First option: {FirstOption}");
            sb.AppendLine($"Second option: {SecondOption}");
            sb.AppendLine($"Second option: {ThirdOption}");
            sb.AppendLine($"Regular list values:\n\t{string.Join("\n\t", RegularList.ToArray())}");
            sb.AppendLine(
                $"Custom list values:\n\t{string.Join("\n\t", CustomList.Select(x => x.ToString("\n\t")).ToArray())}");
            sb.AppendLine(
                $"Regular dictionary values: \n\t{string.Join("\n\t", RegularDictionary.Select(x => $"{x.Key}: {x.Value}").ToArray())}");
            sb.AppendLine(
                $"Custom dictionary values: \n\t{string.Join("\n\t", CustomDictionary.Select(x => $"{x.Key}:\n\t\t{x.Value.ToString("\n\t\t")}").ToArray())}");
            return sb.ToString();
        }
    }

    internal class CustomClass1
    {
        public float FloatValue;
        public string Name;
        public int Value;

        public CustomClass1(string name, int value, float floatValue)
        {
            Name = name;
            Value = value;
            FloatValue = floatValue;
        }

        public CustomClass1()
        {
            //This method SHOULD exists so this class can be read. As the ConfigFile create empty instance, and then populate it with values.
        }

        public string ToString(string Seperator) =>
            $"Name: {Name}{Seperator}Value = {Value}{Seperator}FloatValue = {FloatValue}";
    }
}