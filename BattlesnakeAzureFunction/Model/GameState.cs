using Newtonsoft.Json;

namespace BattlesnakeAzureFunction.Model
{
    public  class GameState {
        [JsonProperty("game")]
        public  Game Game;
        [JsonProperty("turn")]
        public  int Turn;
        [JsonProperty("board")]
        public  Board Board;
        [JsonProperty("you")]
        public  Snake You;
    }
}