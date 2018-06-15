using Xunit;

namespace orica.test
{
    public class DocumentServiceTest
    {

        [Fact]
        public void CanGetAllPersonAvailableGivenASection()
        {
            // setup/arrange
            var doc = new Document
            {
                Availability = new Availability[]
                {
                    new Availability
                    {
                        Person = "Mike",
                        Sections = new []{1,2,3 }
                    },
                    new Availability
                    {
                        Person = "John",
                        Sections =new [] { 1}
                    },
                    new Availability
                    {
                        Person = "Zeth",
                        Sections = new []{ 2}
                    },
                    new Availability
                    {
                        Person = "Virus",
                        Sections = new []{2,3 }
                    }
                }
            };

            var documentService = new DocumentService(doc);


            //act
            var persons = documentService.GetPersonAvailable(1);
            //asert
            Assert.Equal("Mike", persons[0]);
            Assert.Equal("John", persons[1]);
            Assert.Equal(2, persons.Length);

            //act
            persons = documentService.GetPersonAvailable(2);
            //asert
            Assert.Equal("Mike", persons[0]);
            Assert.Equal("Zeth", persons[1]);
            Assert.Equal("Virus", persons[2]);
            Assert.Equal(3, persons.Length);


            //act
            persons = documentService.GetPersonAvailable(3);
            //asert
            Assert.Equal("Mike", persons[0]);
            Assert.Equal("Virus", persons[1]);
            Assert.Equal(2, persons.Length);


            //act
            persons = documentService.GetPersonAvailable(0);
            //asert
            Assert.Empty(persons);

            //act
            persons = documentService.GetPersonAvailable(4);
            //asert
            Assert.Empty(persons);

        }

        [Theory]
        [InlineData("Mike", 3)]
        [InlineData("John", 1)]
        [InlineData("Virus", 1)]
        [InlineData("Zeth", 0)]
        public void Can_GetOwnerSongs(string owner, int expectedCount)
        {
            var doc = new Document
            {
                Songs = new Song[]
                 {
                     new Song
                     {
                         Owner = "Mike"
                     },
                     new Song
                     {
                         Owner = "Mike"
                     },

                     new Song
                     {
                         Owner = "John"
                     },
                     new Song
                     {
                         Owner = "Mike"
                     },
                     new Song
                     {
                         Owner = "Virus"
                     }

                 }
            };

            var documentService = new DocumentService(doc);

            Assert.Equal(expectedCount, documentService.GetOwnerSongs(owner).Length);
        }
    }
}
