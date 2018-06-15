using System;
using Xunit;
using orica;


namespace orica.test
{
    public class SongTest
    {
        [Fact]
        public void CanProperlyDisplayTheDetailWhenUsingToString()
        {
            Song song = new Song
            {
                Band = "Network",
                Title = "Cartoon"
            };

            Assert.Equal("Cartoon - Network", song.ToString());
        }

        [Fact]
        public void CanConvertMM_SS_To_TotalSeconds()
        {
            Song song = new Song
            {
                Time = "1:1"
            };

            Assert.Equal(61, song.TimeInSeconds);


            song = new Song
            {
                Time = "0:1"
            };
            Assert.Equal(1, song.TimeInSeconds);

            song = new Song
            {
                Time = null
            };
            Assert.Equal(0, song.TimeInSeconds);


            song = new Song();
            Assert.Equal(0, song.TimeInSeconds);

            song = new Song
            {
                Time = "1:1:1"
            };
            Assert.Equal(0, song.TimeInSeconds);
        }


    }

}
