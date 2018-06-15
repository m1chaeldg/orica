using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace orica
{
    public class Song
    {
        [JsonProperty("song")]
        public string Title { get; set; }
        public string Band { get; set; }
        public string Time
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                string[] splited = value.Split(':');
                if (splited.Length == 2)
                {
                    if (int.TryParse(splited[0], out int min) &&
                        int.TryParse(splited[1], out int sec))
                        TimeInSeconds = min * 60 + sec;
                }
            }
        }

        public string Owner { get; set; }

        public int TimeInSeconds { get; private set; }

        public override string ToString(){
            return string.Format("{0} - {1}", this.Title, this.Band);
        }
    }


}