using Newtonsoft.Json;

namespace BattlesnakeAzureFunction.Model
{
    public sealed class GameState {
        [JsonProperty("game")]
        public readonly Game Game;
        [JsonProperty("turn")]
        public readonly int Turn;
        [JsonProperty("board")]
        public readonly Board Board;
        [JsonProperty("you")]
        public readonly Snake You;
    }
}