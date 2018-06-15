using Xunit;

namespace orica.test
{
    public class NextSongGeneratorTest
    {
        [Fact]
        public void CanGetNextSong()
        {
            // setup/arrange
            var songs = new Song[]
                 {
                     new Song
                     {
                         Owner = "Mike",
                         Time = "00:12",
                     },
                     new Song
                     {
                         Owner = "Mike",
                         Time = "01:00"
                     },

                     new Song
                     {
                         Owner = "Mike",
                         Time = "5:00"
                     }
                 };

            NextSongGenerator gen = new NextSongGenerator(songs);

            var song = gen.GetNext(8 * 60);
            Assert.NotNull(song);
            Assert.Equal(1, gen.Counter);

            gen = new NextSongGenerator(songs);
            song = gen.GetNext(0);
            Assert.Null(song);
            Assert.Equal(0, gen.Counter);

            gen = new NextSongGenerator(songs);
            song = gen.GetNext(11);
            Assert.Null(song);
            Assert.Equal(0, gen.Counter);

            gen = new NextSongGenerator(songs);
            song = gen.GetNext(60);
            Assert.NotNull(song);


            
            gen = new NextSongGenerator(songs);
            song = gen.GetNext(61);
            Assert.NotNull(song);
            Assert.Equal(12, song.TimeInSeconds);

            song = gen.GetNext(61);
            Assert.NotNull(song);
            Assert.Equal(60, song.TimeInSeconds);

            song = gen.GetNext(61);
            Assert.NotNull(song);
            Assert.Equal(12, song.TimeInSeconds);
        }

        [Fact]
        public void CanReturnTheNextSongThatFitOnTheTimeAllocated()
        {
            // setup/arrange
            var songs = new Song[]
                 {
                     new Song
                     {
                         Owner = "Mike",
                         Time = "00:12",
                     },
                     // this will not return since this is beyond 61 secs
                     new Song
                     {
                         Owner = "Mike",
                         Time = "5:00" 
                     },
                     new Song
                     {
                         Owner = "Mike",
                         Time = "01:00"
                     },
                 };

            NextSongGenerator gen = new NextSongGenerator(songs);

            gen = new NextSongGenerator(songs);
            var song = gen.GetNext(61);
            Assert.NotNull(song);
            Assert.Equal(12, song.TimeInSeconds);

            song = gen.GetNext(61);
            Assert.NotNull(song);
            Assert.Equal(60, song.TimeInSeconds);

            song = gen.GetNext(61);
            Assert.NotNull(song);
            Assert.Equal(12, song.TimeInSeconds);
        }


        [Fact]
        public void NextSongReturnNullIfNoAvailableTime()
        {
            // setup/arrange
            var songs = new Song[]
                 {
                     new Song
                     {
                         Owner = "Mike",
                         Time = "00:12",
                     },
                     new Song
                     {
                         Owner = "Mike",
                         Time = "01:00"
                     },

                     new Song
                     {
                         Owner = "Mike",
                         Time = "5:00"
                     }
                 };

            NextSongGenerator gen;


            gen = new NextSongGenerator(songs);
            var song = gen.GetNext(0);
            Assert.Null(song);
            Assert.Equal(0, gen.Counter);

            gen = new NextSongGenerator(songs);
            song = gen.GetNext(11);
            Assert.Null(song);
            Assert.Equal(0, gen.Counter);
        }
    }
}
