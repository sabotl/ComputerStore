namespace ComputerStore.Domain.ValueObject
{
    public class Dimensions
    {
        public double Height { get; }
        public double Width { get; }
        public double Depth { get; }
        public Dimensions(double height, double width, double depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
        }
    }
}
