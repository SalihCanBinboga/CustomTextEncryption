using System;
using System.Diagnostics;

namespace CustomTextEncryption
{
    class Program
    {
        private static char[] alphabet = { 'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'ğ', 'h', 'i', 'ı', 'j', 'k', 'l', 'm', 'n', 'o', 'ö', 'p', 'q', 'r', 's', 'ş', 't', 'u', 'ü', 'v', 'w', 'x', 'y', 'z' };
        static void Main(string[] args)
        {
            String message = "Selam Nasılsın iyi misin ben iyiyim ama bilmiyorum hadi seni biraz zorlayalım ne kadar doğru çalışıyorsun";

            String encodedMessage = encodeMessage(message);
            Console.WriteLine("Şifrelenen Mesaj: " + encodedMessage);

            String decodedMessage = decodeMessage(encodedMessage);
            Console.WriteLine("Çözülen Mesaj: " + decodedMessage);
        }

        private static string encodeMessage(string message)
        {
            String result = "";
            int encodeMessageIndex = 0;
            foreach (Char letter in message.ToLower().ToCharArray())
            {
                int charIndex = findCharIndex(letter);
                if (charIndex == -1)
                {
                    if (encodeMessageIndex != 0) // 0. indexte boşluk varsa + koymayıp boşluğu silmek için
                    {
                        char lastLetter = result[result.Length - 1];
                        if (lastLetter.Equals('-')) // Önceki karakter - ise siler
                        {
                            result = result.Remove(result.Length - 1);
                        }

                        result += "+";
                    }
                }
                else
                {
                    result += charIndex + "-";
                }
                encodeMessageIndex++;
            }

            char lastCharacter = result[result.Length - 1];
            if (lastCharacter.Equals('-') || lastCharacter.Equals('+'))
            {
                result = result.Remove(result.Length - 1); // Eğer son karakter - ya da + ise kaldırır.
            }

            return result;
        }
        private static int findCharIndex(char letter)
        {
            int result = -1;
            int index = 0;

            foreach (char _letter in alphabet)
            {
                Boolean isSearchedLetter = _letter.Equals(letter);
                if (isSearchedLetter)
                {
                    result = index;
                    break;
                }
                index++;
            }

            //Debug.WriteLine("findCharIndex: Gelen Letter: " + letter + "Bulunan Index: " + result);

            return result; // -1 boşluk temsil eder
        }
        private static string decodeMessage(string encodeMessage)
        {
            String result = "";

            string[] messageEncodeWordList = encodeMessage.Split("+");

            foreach (String word in messageEncodeWordList)
            {
                result += decodeWord(word) + " ";
            }

            return result;
        }
        private static string decodeWord(string word)
        {
            string[] messageEncodeCharList = word.Split("-");

            String resultWord = "";

            foreach (string letter in messageEncodeCharList)
            {
                resultWord += decodeLetter(Int32.Parse(letter));
            }
            return resultWord;
        }
        private static char decodeLetter(int letter)
        {
            return alphabet[letter];
        }

    }
}
