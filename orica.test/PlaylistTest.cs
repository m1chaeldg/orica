using Xunit;
using System.Linq;

namespace orica.test
{
    public class PlaylistTest
    {
        [Fact]
        public void CanGetPlaylist()
        {
            var doc = new Document
            {
                Availability = new Availability[]
                {
                    new Availability
                    {
                        Person = "Mike",
                        Sections = new int[]{ 1,2}
                    },
                    new Availability
                    {
                        Person = "John",
                        Sections = new int[]{2,3}
                    },
                    new Availability
                    {
                        Person = "Zeth",
                        Sections = new int[]{2,3}
                    }
                },
                Songs = new Song[]
                {
                    new Song
                    {
                        Time = "10:00",
                        Band = "Band1",
                        Owner = "Mike",
                    },
                    new Song
                    {
                        Time = "15:00",
                        Band = "Band2",
                        Owner = "Mike",
                    },
                    new Song
                    {
                        Time = "5:00",
                        Band = "Band3",
                        Owner = "Mike",
                    },

                     new Song
                    {
                        Time = "1:00",
                        Band = "Paid1",
                        Owner = "John",
                    },
                    new Song
                    {
                        Time = "10:00",
                        Band = "Paid2",
                        Owner = "John",
                    },
                    new Song
                    {
                        Time = "10:00",
                        Band = "Band1",
                        Owner = "John",
                    },
                    new Song
                    {
                        Time = "25:00",
                        Band = "Band1",
                        Owner = "Zeth",
                    },
                    new Song
                    {
                        Time = "2:00",
                        Band = "Band1",
                        Owner = "Zeth",
                    }
                }
            };

            Playlist playlist = new Playlist(doc);

            var list = playlist.CreatePlayList(1);

            // only one song since Mike is the only one in that section and his song cannot play again
            Assert.Equal(1, list.Count);


            list = playlist.CreatePlayList(2);

            Assert.Equal(5, list.Count);
            Assert.Equal(1740, list.Sum(c=>c.TimeInSeconds));


            list = playlist.CreatePlayList(3);

            Assert.Equal(5, list.Count);
            Assert.Equal(1800, list.Sum(c => c.TimeInSeconds));
        }

        [Fact]
        public void CanOnlyGetOneSongIfNothingCanFitOntheReaminigTime()
        {
            var doc = new Document
            {
                Availability = new Availability[]
                {
                    new Availability
                    {
                        Person = "Mike",
                        Sections = new int[]{ 1,2}
                    },
                    new Availability
                    {
                        Person = "John",
                        Sections = new int[]{2,3}
                    },
                    new Availability
                    {
                        Person = "Zeth",
                        Sections = new int[]{2,3}
                    }
                },
                Songs = new Song[]
                {
                    new Song
                    {
                        Time = "30:00",
                        Band = "Band1",
                        Owner = "Mike",
                    },
                     new Song
                    {
                        Time = "30:00",
                        Band = "Paid1",
                        Owner = "John",
                    },
                    new Song
                    {
                        Time = "30:00",
                        Band = "Band1",
                        Owner = "Zeth",
                    },
                }
            };

            Playlist playlist = new Playlist(doc);

            var list = playlist.CreatePlayList(1);

            // only one song since Mike is the only one in that section and his song cannot play again
            Assert.Equal(1, list.Count);


            list = playlist.CreatePlayList(2);

            Assert.Equal(1, list.Count);


            list = playlist.CreatePlayList(3);

            Assert.Equal(1, list.Count);
        }



        [Fact]
        public void NoSongWillReturnIfAllSongExceed30Mins()
        {
            var doc = new Document
            {
                Availability = new Availability[]
                {
                    new Availability
                    {
                        Person = "Mike",
                        Sections = new int[]{ 1,2}
                    },
                    new Availability
                    {
                        Person = "John",
                        Sections = new int[]{2,3}
                    },
                    new Availability
                    {
                        Person = "Zeth",
                        Sections = new int[]{2,3}
                    }
                },
                Songs = new Song[]
                {
                    new Song
                    {
                        Time = "30:01",
                        Band = "Band1",
                        Owner = "Mike",
                    },
                     new Song
                    {
                        Time = "30:01",
                        Band = "Paid1",
                        Owner = "John",
                    },
                    new Song
                    {
                        Time = "30:01",
                        Band = "Band1",
                        Owner = "Zeth",
                    },
                }
            };

            Playlist playlist = new Playlist(doc);

            var list = playlist.CreatePlayList(1);

            // only one song since Mike is the only one in that section and his song cannot play again
            Assert.Equal(0, list.Count);


            list = playlist.CreatePlayList(2);

            Assert.Equal(0, list.Count);


            list = playlist.CreatePlayList(3);

            Assert.Equal(0, list.Count);

        }
    }
}
