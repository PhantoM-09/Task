using Client.Models;
using HTTP_API;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Client.Infrastructure
{
    public class Converter
    {
        public static byte[] ConvertImageToByteArray(BitmapImage image)
        {
            byte[] array;

            PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
            pngBitmapEncoder.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                pngBitmapEncoder.Save(memoryStream);
                array = memoryStream.ToArray();
            }

            return array;
        }

        public static BitmapImage ConvertByteArrayToImage(byte[] array)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(array, 0, array.Length);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memoryStream;
            image.EndInit();
            return image;

        }

        public static List<ClientCard> ConvertServerDataToClientCard(List<InformationCard> list)
        {
            List<ClientCard> clientCards = new List<ClientCard>();

            foreach(var item in list)
            {
                clientCards.Add(new ClientCard(item.Id, item.Name, ConvertByteArrayToImage(item.Image)));
            }

            return clientCards;
        }

        public static InformationCard ConvertClientCardToServerData(ClientCard card)
        {
            return new InformationCard(card.Id, card.Name, ConvertImageToByteArray(card.Image));
        }
    }
}
