namespace ComputerStore.Domain.ValueObject
{
    public class Color
    {
        public string Name { get; }
        public string HexCode { get; }

        public Color(string name, string hexCode)
        {
            Name = name;
            HexCode = hexCode;
        }
    }
}
