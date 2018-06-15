using Xunit;

namespace orica.test
{
    public class StorageTest
    {
        const string jsonData = "{\"songs\":[{\"song\":\"When I Come Around\",\"band\":\"Green Day\",\"time\":\"3:25\",\"owner\":\"Johnny\"},{\"song\":\"Otherside\",\"band\":\"Red Hot Chili Peppers\",\"time\":\"4:23\",\"owner\":\"Johnny\"},],\"availability\":[{\"person\":\"John\",\"sections\":[1]},{\"person\":\"Clint\",\"sections\":[1,2]},{\"person\":\"Lisa\",\"sections\":[1,2]}]}";

        [Fact]
        public void CanLoadData()
        {
            using (var temp = new TempFile(jsonData))
            {
                // setup
                Storage storage = new Storage();
                // act

               var doc = storage.Load(temp.FileName);

                // assert
                Assert.Equal(2, doc.Songs.Length);
                Assert.Equal("When I Come Around", doc.Songs[0].Title);
                Assert.Equal(3, doc.Availability.Length);
                Assert.Equal("John", doc.Availability[0].Person);
                Assert.Single(doc.Availability[0].Sections);
                Assert.Equal(2, doc.Availability[1].Sections.Length);
                Assert.Equal(2, doc.Availability[2].Sections.Length);
            }

        }
    }
}
