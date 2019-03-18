using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiny;

namespace ExampleModNamespace
{
    internal class PluginConfig2
    {
        [JsonProperty("Example of the dictionary that contains custom classes")]
        public Dictionary<string, CustomClass2> CustomDict;

        [JsonProperty("Example of the list that contains custom classes")]
        public List<CustomClass2> CustomList;

        [JsonProperty("First value of config")]
        public string FirstValue;

        [JsonProperty("Example of the dictionary that contains regular classes")]
        public Dictionary<string, int> RegularDict;

        [JsonProperty("Example of the list that contains regular values")]
        public List<string> RegularList;

        [JsonProperty("Second value of the config")]
        public int SecondValue;

        [JsonProperty("And ofc some doubles")]
        public double SomeDouble;

        [JsonProperty("Some floats to the config. Everybody loves them, right?")]
        public float SomeFloats;

        public static PluginConfig2 DefaultConfig() =>
            new PluginConfig2
            {
                FirstValue = "Hey!",
                SecondValue = 99,
                SomeFloats = 0.003f,
                SomeDouble = 1.2d,
                RegularList = new List<string> {"How", "are", "you", "?"},
                CustomList = new List<CustomClass2>
                {
                    new CustomClass2("First", 120, 0f),
                    new CustomClass2("Second", 119, 0)
                },
                RegularDict = new Dictionary<string, int>
                {
                    ["Entry one"] = 16,
                    ["Another key"] = 1201
                },
                CustomDict = new Dictionary<string, CustomClass2>
                {
                    ["First custom class"] = new CustomClass2("Player 1", 65, 100f),
                    ["Second custom class"] = new CustomClass2("Player 2", 1, 199f)
                }
            };

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"First value: {FirstValue}");
            sb.AppendLine($"Second value: {SecondValue}");
            sb.AppendLine($"SomeFloats: {SomeFloats}");
            sb.AppendLine($"SomeDouble: {SomeDouble}");
            sb.AppendLine($"Regular list values:\n\t{string.Join("\n\t", RegularList.ToArray())}");
            sb.AppendLine(
                $"Custom list values:\n\t{string.Join("\n\t", CustomList.Select(x => x.ToString("\n\t")).ToArray())}");
            sb.AppendLine(
                $"Regular dictionary values: \n\t{string.Join("\n\t", RegularDict.Select(x => $"{x.Key}: {x.Value}").ToArray())}");
            sb.AppendLine(
                $"Custom dictionary values: \n\t{string.Join("\n\t", CustomDict.Select(x => $"{x.Key}:\n\t\t{x.Value.ToString("\n\t\t")}").ToArray())}");
            return sb.ToString();
        }

        //Be aware! Private fields aren't being saved in the config!
        public class CustomClass2
        {
            [JsonProperty("Count of something")]
            public int Count;

            [JsonProperty("Name of this custom class")]
            public string Name;

            [JsonProperty("And timer. Why not?")]
            public float Timer;

            public CustomClass2(string name, int count, float timer)
            {
                Name = name;
                Count = count;
                Timer = timer;
            }
            public CustomClass2()
            {
                //This method SHOULD exists so this class can be read. As the ConfigFile create empty instance, and then populate it with values.
            }

            public string ToString(string Seperator) =>
                $"Name: {Name}{Seperator}Count = {Count}{Seperator}Timer = {Timer}";
        }
    }
}