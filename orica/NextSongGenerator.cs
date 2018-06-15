using System.Collections.Generic;

namespace orica
{
    public class NextSongGenerator
    {
        IReadOnlyList<Song> songs;

        public NextSongGenerator(Song[] songs)
        {
            this.songs = songs;
        }

        public int Counter { get; set; }

        public Song GetNext(int belowTime)
        {
            bool found = false;
            try
            {

                for (int i = Counter; i < songs.Count; i++)
                {
                    Song song = songs[i];
                    if (song.TimeInSeconds <= belowTime)
                    {
                        Counter = i;
                        found = true;
                        return song;
                    }
                }
                for (int i = 0; i < Counter; i++)
                {
                    Song song = songs[i];
                    if (song.TimeInSeconds <= belowTime)
                    {
                        Counter = i;
                        found = true;
                        return song;
                    }
                }

            }
            finally
            {
                if (found)
                    IncrementOrResetCounter();
            }

            return null;
        }

        private void IncrementOrResetCounter()
        {
            Counter++;

            if (Counter >= songs.Count)
                Counter = 0;
        }
    }
}