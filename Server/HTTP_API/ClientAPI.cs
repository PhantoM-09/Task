using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace HTTP_API
{
    public class ClientAPI
    {
        static string uri = "http://localhost:8888/";
        static HttpClient httpClient = new HttpClient();

        public static List<InformationCard> GetCards()
        {
            return JsonSerializer.Deserialize<List<InformationCard>>
                                    (Encoding.UTF8.GetString(
                                        httpClient.GetAsync(uri).Result.
                                            Content.ReadAsByteArrayAsync().Result));

        }

        public static void AddCard(string addedName, byte[] addedImage)
        {
            InformationCard addedCard = new InformationCard(0, addedName, addedImage);
            httpClient.PostAsync(uri, new StringContent(JsonSerializer.Serialize(addedCard)));
        }

        public static void DeleteCard(int deletedId)
        {
            httpClient.DeleteAsync(uri + "?Id=" + deletedId);
        }

        public static void UpdateCard(int updatedId, string updatedName, byte[] updatedImage)
        {
            InformationCard updatedCard = new InformationCard(updatedId, updatedName, updatedImage);
            httpClient.PutAsync(uri, new StringContent(JsonSerializer.Serialize(updatedCard)));
        }
    }
}
