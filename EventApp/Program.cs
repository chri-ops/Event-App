using System;

namespace EventApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till Event-Appen.\n");
            Console.WriteLine("Startar programmet...\n");
            Console.WriteLine("Tryck valfri tangent för att komma till Startmenyn...");
            Console.ReadKey();

            EventApp app = new EventApp();
            app.Start();
        }
    }
}
