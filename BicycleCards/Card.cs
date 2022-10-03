using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Printing;

namespace BlackJack.BicycleCards
{
    public class Card
    {
        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }
        public bool IsFaceUp { get; private set; } = true;

        /* Magic Numbers */
        private readonly string _faceDownReturnString = "¿?";
        private readonly int _aceFullValue = 11;

        /* Card Colors */
        private const ConsoleColor _colorBlack = ConsoleColor.DarkBlue;
        private const ConsoleColor _colorRed = ConsoleColor.DarkRed;
        private const ConsoleColor _colorFaceDown = ConsoleColor.DarkYellow;

        /// <summary>
        /// Sets the card's values, cannot be changed.
        /// </summary>
        /// <param name="suit">The Card's <see cref="CardSuit"/>.</param>
        /// <param name="value">The Card's <see cref="CardValue"/></param>
        public Card(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }


        /// <summary>
        /// Returns the card's value as it's calculated in Black Jack. Aces default to full value.
        /// </summary>
        public int getValue
        {
            get
            {
                if (Value >= CardValue.Ten)
                {
                    return (int)CardValue.Ten;
                }
                else if (Value >= CardValue.Two)
                {
                    return (int)Value;
                }
                // Ace full value.
                return _aceFullValue;
            }
        }

        /// <summary>
        /// <em>Flips</em> the <see cref="Card"/> facedown, making it <em>hidden</em> to the player.<br></br>
        /// Only applies to the first card <see cref="Participant.Dealer"/> gets.
        /// </summary>
        /// <returns><c>this</c> with <see cref="IsFaceUp"/> set to <c>false</c>.</returns>
        public Card SetFaceDown() {
            IsFaceUp = false;
            return this;
        }

        /// <summary>
        /// <em>Flips</em> the <see cref="Card"/> faceup, making it <em>visible</em> to the player.<br></br>
        /// Only applies to the first card <see cref="Participant.Dealer"/> gets.
        /// </summary>
        /// <returns><c>this</c> with <see cref="IsFaceUp"/> set to <c>true</c>.</returns>
        public Card SetFaceUp() {
            IsFaceUp = true;
            return this;
        }

        #region Console Print properties
        public string getSuitAndValue => IsFaceUp ? $"{GetSuitUnicode}{GetValueChar}" : _faceDownReturnString;

        public string getValueAndSuit => IsFaceUp ? $"{GetValueChar}{GetSuitUnicode}" : _faceDownReturnString;

        
        /// <summary>
        /// Gets the unicode character representing the card's <see cref="Suit"/>,
        /// </summary>
        private string GetSuitUnicode
        {
            get
            {
                switch (Suit)
                {
                    case CardSuit.Hearts:
                        return "♥";
                    case CardSuit.Spades:
                        return "♠";
                    case CardSuit.Diamonds:
                        return "♦";
                    case CardSuit.Clubs:
                        return "♣";
                    default:
                        throw new Exception("Card-object does not have a suit, also this will literally never happen.");
                }
            }
        }


        /// <summary>
        /// Returns the <see cref="Value"/> as a single character, I.E. "Ace" -> "A"; "Jack" -> "J", etc..<br></br>
        /// Used in conjunction with <see cref="GetSuitUnicode"/> for fancy printing.
        /// </summary>
        private string GetValueChar
        {
            get
            {
                if (Value > CardValue.Ace && Value < CardValue.Jack)
                {
                    var value = (int)Value;
                    return value.ToString();
                }
                return Value.ToString().Substring(0, 1);
            }
        }


        /// <summary>
        /// Returns the appropriate <see cref="ConsoleColor"/> based on the <see cref="CardSuit"/>, and if the card isn't <see cref="IsFaceUp"/>.
        /// </summary>
        public ConsoleColor getColor {
            get {
                if (!IsFaceUp) {
                    return _colorFaceDown;
                }
                return (int)Suit % 2 == 0 ? _colorBlack : _colorRed;
            }
        }

        #endregion

        //TODO what do you even write about ToString()???
        /// <summary>
        /// For fancy printing.
        /// </summary>
        /// <returns>It's fancy string</returns>
        public override string ToString() {
            return getSuitAndValue;
        }
    }
}
