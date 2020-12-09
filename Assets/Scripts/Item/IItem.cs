public interface IItem
{
    string Name { get; }

    float SellPrice { get; }

    void Sell();
}