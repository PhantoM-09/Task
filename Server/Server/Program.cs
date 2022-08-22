using System;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectionManager.LoadCards();
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();

            while (true)
            {
                Console.WriteLine("Ожидание...");
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string method = request.HttpMethod;
                switch (method)
                {
                    case "GET":
                        Console.WriteLine("Обработка запроса с GET методом");
                        CollectionManager.LoadCards();
                        Server.HandlingGetMethod(response);
                        break;
                    case "POST":
                        Console.WriteLine("Обработка запроса с POST методом");
                        Server.HandlingPostMethod(request);
                        break;
                    case "DELETE":
                        Console.WriteLine("Обработка запроса с DELETE методом");
                        Server.HandlingDeleteMethod(request);
                        break;
                    case "PUT":
                        Console.WriteLine("Обработка запроса с PUT методом");
                        Server.HandlingPutMethod(request);
                        break;
                }
            }
            listener.Stop();
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }
    }
}
