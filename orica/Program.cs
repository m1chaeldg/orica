using System;
using System.IO;
using System.Reflection;

namespace orica
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            IStorage storage = new Storage();
            Document document = storage.Load(Path.Combine(dir, "Jukebox.json"));

            if (document.Availability == null || document.Songs == null)
            {
                Console.WriteLine("No JSON data. Make sure the Jukebox.json is located where the application is running");
            }
            Playlist playlist = new Playlist(document);

            foreach (int section in new int[] { 1, 2 })
            {
                if (section == 1)
                    Console.WriteLine("The first 30 minutes will be:");
                else
                    Console.WriteLine("The final 30 minutes will be:");

                int counter = 1;
                foreach (Song song in playlist.CreatePlayList(section))
                {
                    Console.WriteLine("{0}. {1}", counter++, song);
                }
            }
        }
    }
}
