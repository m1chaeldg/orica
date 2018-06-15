using System.Linq;

namespace orica
{
    public class DocumentService
    {
        private readonly Document doc;
        public DocumentService(Document doc)
        {
            this.doc = doc;
        }

        public string[] GetPersonAvailable(int section)
        {
            return doc.Availability
                .Where(c => c.Sections.Any(s => s == section))
                .Select(c => c.Person)
                .ToArray();
        }

        public Song[] GetOwnerSongs(string person)
        {
            return doc.Songs
                .Where(c => c.Owner == person)
                .ToArray();
        }
    }
}