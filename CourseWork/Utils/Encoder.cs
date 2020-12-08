using System.Collections.Generic;
using System;

namespace CourseWork.Utils
{
    public class Encoder
    {
        private int _sign = 1;
        private string _key;
        private string _alphabet;
        private static readonly Dictionary<EncodingLanguage, string> _alphabets = new Dictionary<EncodingLanguage, string>()
        {
            { EncodingLanguage.Russian, "абвгдеёжзийклмнопрстуфхцчшщъыьэюя" },
            { EncodingLanguage.English, "abcdefghijklmnopqrstuvwxyz" },
        };

        private Dictionary<char, int> _alphabetIndexes = new Dictionary<char, int>();
        private Dictionary<char, int> _keyShifts = new Dictionary<char, int>();

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                _keyShifts.Clear();


                foreach (char i in _key)
                {
                    if (!_keyShifts.ContainsKey(i))
                    {
                        _keyShifts.Add(i, _alphabetIndexes[i]);
                    }
                }
            }
        }

        public string Alphabet
        {
            get
            {
                return _alphabet;
            }
            set
            {
                _alphabet = value;
                _alphabetIndexes.Clear();

                int index = 0;
                foreach (char i in _alphabet)
                {
                    _alphabetIndexes.Add(i, index);
                    index++;
                }
            }
        }

        public Encoder(string key = "скорпион", EncodingLanguage language = EncodingLanguage.Russian)
        {
            Alphabet = _alphabets[language];

            key = key.Trim();
            if (IsValidKey(key))
            {
                Key = key.ToLower();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool IsValidKey(string key)
        {
            if (key == "" || key == null)
            {
                return false;
            }
            key = key.ToLower();
            foreach (var symbol in key)
            {
                if (!char.IsLetter(symbol) || !Alphabet.Contains(symbol.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        public string Encode(string text)
        {
            _sign = 1;
            return Transform(text);
        }

        public string Decode(string text)
        {
            _sign = -1;
            return Transform(text);
        }

        private string Transform(string text)
        {
            string result = "";
            int index = 0;

            foreach (char symbol in text)
            {
                if (!_alphabetIndexes.ContainsKey(char.ToLower(symbol)))
                {
                    result += symbol;
                    continue;
                }

                int newIndex = GetNewIndex(symbol, index);
                char newSymbol = Alphabet[newIndex];

                if (char.IsUpper(symbol))
                {
                    newSymbol = char.ToUpper(newSymbol);
                }

                result += newSymbol;

                index++;
            }

            return result;
        }

        private int GetNewIndex(char symbol, int index)
        {
            int shift = _keyShifts[Key[index % Key.Length]];
            var tmp = _alphabetIndexes[char.ToLower(symbol)] + _sign * shift;
            int newIndex;

            if (tmp < 0)
            {
                newIndex = (tmp + Alphabet.Length) % Alphabet.Length;
            }
            else
            {
                newIndex = tmp % Alphabet.Length;
            }

            return newIndex;
        }
    }
}