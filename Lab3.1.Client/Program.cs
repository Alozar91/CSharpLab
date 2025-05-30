namespace Lab3._1.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            MyClient client = new MyClient("http://localhost:5074", httpClient);
            var people = client.StudentsAllAsync().Result;
            foreach (var p in people)
            {
                Console.WriteLine($"{p.Id}.{p.Name} - {p.Age}");
            }
            Console.ReadLine();
        }
    }
}
