// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");

var deck = new Deck();
Console.WriteLine(deck.ToString());

var (d1, d2) = deck.Split();
Console.WriteLine(d1.ToString());
Console.WriteLine(d2.ToString());

Stopwatch sw = new Stopwatch();
sw.Start();
var counts = new List<int>();
for (var i = 0; i < 100_000; i++)
{
	var game = new Game();
	var count = game.Play();
	counts.Add(count);
}
sw.Stop();

Console.WriteLine($"Average: {counts.Average()}, elapsed: {sw.ElapsedMilliseconds}ms");

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

public class Game
{
	private Deck _d1;
	private Deck _d2;

	public Game()
	{
		var deck = new Deck();
		(_d1, _d2) = deck.Split();
	}

	public int Play()
	{
		var counter = 0;
		while (true)
		{
			counter++;
			this.OneTurn();
			if (_d1.Empty || _d2.Empty)
			{
				break;
			}
		}
		return counter;
	}

	private void OneTurn()
	{
		if (_d1.Cards.Count() == 0 || _d2.Cards.Count() == 0)
		{
			return;
		}

		var engaged = new List<int>();
		while (true)
		{
			var card1 = _d1.Pop();
			var card2 = _d2.Pop();

			engaged.Add(card1);
			engaged.Add(card2);

			if (card1 != card2)
			{
				break;
			}

			if (_d1.Cards.Count > 0 && _d2.Cards.Count > 0)
			{
				engaged.Add(_d1.Pop());
				engaged.Add(_d2.Pop());
			}

			if (_d1.Cards.Count == 0 || _d2.Cards.Count == 0)
			{
				break;
			}
		}

		var target = engaged[engaged.Count - 1] > engaged[engaged.Count - 2] ? _d2 : _d1;

		engaged.Shuffle();

		target.Add(engaged);
	}

	public override string ToString()
	{
		return $"Deck 1: {_d1.ToString()}, Deck 2: {_d2.ToString()}";
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
	public IList<int> Cards
	{
		get
		{
			return this._cards;
		}
	}

	public int Pop()
	{
		if (this._cards.Count == 0)
		{
			throw new IndexOutOfRangeException();
		}
		var card = this._cards[0];
		this._cards.RemoveAt(0);
		return card;
	}

	public void Add(IList<int> cards)
	{
		cards.ToList().ForEach(c => this._cards.Add(c));
	}

	public bool Empty
	{
		get
		{
			return this._cards.Count == 0;
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
