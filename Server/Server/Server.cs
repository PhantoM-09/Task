using HTTP_API;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Server
{
    public class Server
    {
        public static void HandlingGetMethod(HttpListenerResponse response)
        {
            try
            {
                string responseData = JsonSerializer.Serialize(CollectionManager.GetCards());
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseData);

                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("GetMethod: " + ex.Message);
            }
        }

        public static void HandlingPostMethod(HttpListenerRequest request)
        {
            try
            {
                using (Stream stream = request.InputStream)
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        string cardJson = streamReader.ReadToEnd();
                        InformationCard addedCard = JsonSerializer.Deserialize<InformationCard>(cardJson);

                        if (addedCard == null)
                            throw new ArgumentNullException("addedCard");
                        CollectionManager.AddCard(addedCard);

                        CollectionManager.SaveCards();
                    }
                }
            }
            catch(ArgumentNullException argException)
            {
                Console.WriteLine("PostMethod: " + argException.ParamName + " " + argException.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("PostMethod: " + ex.Message);
            }
        }

        public static void HandlingDeleteMethod(HttpListenerRequest request)
        {
            try
            {
                NameValueCollection nameValueCollection = request.QueryString;
                int deletedId = int.Parse(nameValueCollection.Get("Id"));

                if (deletedId <= 0)
                    throw new ArgumentException("deletedId", "is not supported");
                CollectionManager.DeleteCard(deletedId);

                CollectionManager.SaveCards();
            }
            catch(ArgumentException argException)
            {
                Console.WriteLine("DeleteMethod: " + argException.ParamName + " " + argException.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("DeleteMethod: " + ex.Message);
            }
        }

        public static void HandlingPutMethod(HttpListenerRequest request)
        {
            try
            {
                using (Stream stream = request.InputStream)
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        string cardJson = streamReader.ReadToEnd();
                        InformationCard updatedCard = JsonSerializer.Deserialize<InformationCard>(cardJson);

                        if (updatedCard is null)
                            throw new ArgumentNullException("updatedCard");

                        if (updatedCard.Id <= 0)
                            throw new ArgumentException("updatedId", "is not supported");

                        CollectionManager.UpdateCard(updatedCard);

                        CollectionManager.SaveCards();
                    }
                }
            }

            catch (ArgumentNullException argException)
            {
                Console.WriteLine("PutMethod: " + argException.ParamName + " " + argException.Message);
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine("PutMethod: " + argException.ParamName + " " + argException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PutMethod: " + ex.Message);
            }
        }
    }
}
