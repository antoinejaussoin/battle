const shuffle = require('lodash/shuffle');
const sum = require('lodash/sum');

console.log('Hello world')

const start = performance.now();
    const  counts = [];

		for (let i = 0; i< 100_000; i++) {
			const count = game();
			counts.push(count);
		}

    let duration = performance.now() - start;
		//console.log('')
//    println!("{}", counts.iter().map(|x| x.to_string()).collect::<Vec<String>>().join(","));
    console.log("Time elapsed: ", duration, "ms");
		// console.log(counts);
		console.log("Average count: ", sum(counts)/counts.length);



function game() {
	let deck = produceDeck();

//	console.log('Deck: ', deck)

	const [d1, d2] = splitDeck(deck);

//	console.log('Split: ', d1, d2)
 
	let counter = 0;
	do {
			counter += 1;
			play(d1, d2);
			if (d1.cards.length == 0 || d2.cards.length == 0) {
					break;
			}
			if (counter >= 10000) {
					printDeck(d1);
					printDeck(d2);
					break;
			}
	} while(true)

	return counter
}

function play(deck1, deck2) {
	const engaged = [];

	do {
			let card1 = deck1.cards.shift();
			let card2 = deck2.cards.shift();
			engaged.push(card1);
			engaged.push(card2);

			if (card1 != card2) {
					break;
			}

			if (deck1.cards.length > 0 && deck2.cards.length > 0) {
					engaged.push(deck1.cards.shift());
					engaged.push(deck2.cards.shift()); 
			}
	
			if (deck1.cards.length == 0 || deck2.cards.length == 0) {
					break;
			}
	} while(true)

			let target = engaged[engaged.length - 1] > engaged[engaged.length - 2] ? deck2 : deck1;
					

	shuffle(engaged).forEach(card => {
			target.cards.push(card);
	});
}


function produceDeck() {
	const deck = { cards: [] };
	
	pushCards(deck, 7, 6);
	pushCards(deck, 6, 6);
	pushCards(deck, 5, 6);
	pushCards(deck, 4, 6);
	pushCards(deck, 3, 6);
	pushCards(deck, 2, 6);
	pushCards(deck, 1, 6);

	shuffleDeck(deck);

return deck;
}

function splitDeck(deck) {
	// let (c1, c2) = deck.cards.split_at(deck.cards.len() / 2);

	
	return [{ cards: deck.cards.slice(0, deck.cards.length / 2)}, { cards: deck.cards.slice(deck.cards.length / 2)}]
}

function shuffleDeck(deck)  {
	deck.cards = shuffle(deck.cards);
}

function pushCards(deck, value, times) {
	for (let i = 0; i < times; i++) {
			deck.cards.push(value);
	}
}

function printDeck(deck) {
	console.log("Deck: ");
	deck.cards.forEach(card => {
		console.log(card);
	})
}