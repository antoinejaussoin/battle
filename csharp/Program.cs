// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var deck = new Deck();
Console.WriteLine(deck.ToString());

var (d1, d2) = deck.Split();
Console.WriteLine(d1.ToString());
Console.WriteLine(d2.ToString());



static class Extensions
{
	private static Random rng = new Random();

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

}


public class Deck
{
	private List<int> _cards;

	public Deck()
	{
		this._cards = new List<int>();
		this.Initialise();
	}
	public IEnumerable<int> Cards
	{
		get
		{
			return this._cards;
		}
	}

	public (Deck, Deck) Split()
	{
		var deck1 = new Deck();
		var deck2 = new Deck();

		deck1._cards = new List<int>(this._cards.Take(this._cards.Count / 2));
		deck2._cards = new List<int>(this._cards.Skip(this._cards.Count / 2));

		return (deck1, deck2);
	}

	private void Initialise()
	{
		AddNCards(7, 6);
		AddNCards(6, 6);
		AddNCards(5, 6);
		AddNCards(4, 6);
		AddNCards(3, 6);
		AddNCards(2, 6);
		AddNCards(1, 6);
		this._cards.Shuffle();
	}

	private void AddNCards(int value, int number)
	{
		for (var i = 0; i < number; i++)
		{
			this._cards.Add(value);
		}
	}

	public override string ToString()
	{
		return string.Join(", ", this._cards);
	}
}
