using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class GameConfiguration
    {
        public PlayerType PlayerWhite { get; set; }
        public PlayerType PlayerBlack { get; set; }
        public AiAlgorithmType PlayerWhiteAiType { get; set; }
        public AiAlgorithmType PlayerBlackAiType { get; set; }
        public Heuristics Heuristics { get; set; }
    }
}