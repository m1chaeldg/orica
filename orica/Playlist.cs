using System.Collections.Generic;

namespace orica
{
    public class Playlist
    {
        private readonly Document doc;
        const int maxPlaytime = 30 * 60;

        public Playlist(Document doc)
        {
            this.doc = doc;
        }

        public IReadOnlyList<Song> CreatePlayList(int section)
        {
            var documentService = new DocumentService(doc);
            // get the person only available to that time
            string[] availablePersons = documentService.GetPersonAvailable(section);

            var songIndex = new Dictionary<string, NextSongGenerator>();

            foreach (string owner in availablePersons)
            {
                var ownerSongs = documentService.GetOwnerSongs(owner);
                songIndex[owner] = new NextSongGenerator(ownerSongs);
            }

            int songDuration = 0;
            bool stop = false;
            string lastOwner = string.Empty;

            var playlist = new List<Song>();

            while (!stop)
            {
                foreach (string owner in availablePersons)
                {
                    // let us stop since there is no more available song that fit on the reaming time
                    if (lastOwner == owner)
                    {
                        stop = true;
                        break;
                    }

                    // get the next song on the remaining time
                    Song nextSong = songIndex[owner].GetNext(maxPlaytime - songDuration);

                    // if song was found. add it to the playlist
                    if (nextSong != null)
                    {
                        playlist.Add(nextSong);

                        // save the last owner so that we can determine 
                        // if the loop tried to look for another song on another ower.
                        // if the next loop was still the same with the last song
                        // that means no more reamining song is fitted on the ramaining time
                        lastOwner = owner;
                        songDuration += nextSong.TimeInSeconds;
                    }

                    // else try to look another song on other owner

                }

                // no song was found that is fitted on 30 mins
                if (playlist.Count == 0)
                    break;
            }

            return playlist;
        }



    }
}