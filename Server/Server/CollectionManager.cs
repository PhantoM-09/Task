using HTTP_API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Threading;

namespace Server
{
    class CollectionManager
    {
        private static string _fileName = "Card.txt";

        private static List<InformationCard> _cardCollection;

        public static void AddCard(InformationCard addedCard)
        {
            int countCards = _cardCollection.Count;
            int newCardId = countCards > 0 ? _cardCollection[countCards - 1].Id + 1 : 1;
            
            _cardCollection.Add(new InformationCard(newCardId, addedCard.Name, addedCard.Image));
        }

        public static void DeleteCard(int deletedId)
        {
            InformationCard deletedCard = _cardCollection.First(i => i.Id == deletedId);
            _cardCollection.Remove(deletedCard);
        }

        public static void UpdateCard(InformationCard changingCard)
        {
            InformationCard updatedCard = _cardCollection.First(i => i.Id == changingCard.Id);

            updatedCard.Name = changingCard.Name;
            updatedCard.Image = changingCard.Image;
        }

        public static List<InformationCard> GetCards()
        {
            return _cardCollection;
        }

        public static void LoadCards()
        {
            if (!File.Exists(_fileName))
            {
                using(StreamWriter streamWriter = new StreamWriter(_fileName))
                {
                    streamWriter.Write("");
                }
            }

            using(StreamReader streamReader = new StreamReader(_fileName))
            {
                string jsonString = streamReader.ReadToEnd();
                if (string.IsNullOrEmpty(jsonString))
                    _cardCollection = new List<InformationCard>();
                else
                    _cardCollection = JsonSerializer.Deserialize<List<InformationCard>>(jsonString);
            }
        }

        public static void SaveCards()
        {
            using (StreamWriter streamWriter = new StreamWriter(_fileName))
            {
                string jsonString = JsonSerializer.Serialize(_cardCollection);
                streamWriter.WriteLine(jsonString);
            }
        }
    }
}
