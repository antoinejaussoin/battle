use rand::thread_rng;
use rand::seq::SliceRandom;
use std::time::{Instant};


fn main() {
    println!("Hello, world!");

    let start = Instant::now();
    let mut counts = Vec::new();

    for _ in 0..100_000 {
        let count = game();
        counts.push(count);
    }

    let duration = start.elapsed();
    // println!("{}", counts.iter().map(|x| x.to_string()).collect::<Vec<String>>().join(","));
    println!("Time elapsed: {}ms", duration.as_millis());
    let average = (counts.iter().sum::<i32>()) ;
    println!("Average: {}", average / (counts.len() as i32));
}

fn game() -> i32 {
    let deck = produce_deck();

    let (mut d1, mut d2) = split_deck(deck);
   
    let mut counter = 0;
    loop {
        counter += 1;
        play(&mut d1, &mut d2);
        if d1.cards.len() == 0 || d2.cards.len() == 0 {
            break;
        }
        if counter >= 10000 {
            print_deck(& d1);
            print_deck(& d2);
            break;
        }
    }

    counter
}

struct Deck {
    cards: Vec<i8>
}

fn play(deck1: &mut Deck, deck2: &mut Deck) {
    let mut engaged = Vec::new();

    loop {
        let card1 = deck1.cards.remove(0);
        let card2 = deck2.cards.remove(0);
        engaged.push(card1);
        engaged.push(card2);

        if card1 != card2 {
            break
        }

        if deck1.cards.len() > 0 && deck2.cards.len() > 0 {
            engaged.push(deck1.cards.remove(0));
            engaged.push(deck2.cards.remove(0)); 
        }
    
        if deck1.cards.len() == 0 || deck2.cards.len() == 0 {
            break;
        }
    }

        let target = match engaged[engaged.len() - 1] > engaged[engaged.len() - 2] {
            true => deck2,
            false => deck1
        };

        engaged.shuffle(&mut thread_rng());

    for card in engaged {
        target.cards.push(card);
    }
}

fn produce_deck() -> Deck {
    let mut deck = Deck {
        cards: Vec::new()
    };

    push_cards(&mut deck, 7, 6);
    push_cards(&mut deck, 6, 6);
    push_cards(&mut deck, 5, 6);
    push_cards(&mut deck, 4, 6);
    push_cards(&mut deck, 3, 6);
    push_cards(&mut deck, 2, 6);
    push_cards(&mut deck, 1, 6);

    shuffle_deck(&mut deck);

    deck
}

fn split_deck(deck: Deck) -> (Deck, Deck) {
    let (c1, c2) = deck.cards.split_at(deck.cards.len() / 2);

    let deck1 = Deck { cards: c1.to_vec() };
    let deck2 = Deck { cards: c2.to_vec() };

    (deck1, deck2)
}

fn shuffle_deck(deck: &mut Deck)  {
    deck.cards.shuffle(&mut thread_rng());
}

fn push_cards(deck: &mut Deck, value: i8, times: i8) {
    for _ in 0..times {
        deck.cards.push(value);
    }
}

fn print_deck(deck: &Deck) {
    print!("Deck: ");
    for card in deck.cards.as_slice() {
        
        print!("{}, ", card)
    }
    println!()
}