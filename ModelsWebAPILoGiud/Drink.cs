namespace WebAPILoGiud.Data
{
    public class Drink
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Size { get; set; }
        public required bool Fizz { get; set; }
    }
}
