using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class GameSetup
    {
        public PlayerType PlayerWhite { get; set; }
        public PlayerType PlayerBlack { get; set; }
        public AiAlgorithmType PlayerWhiteAiType { get; set; }
        public AiAlgorithmType PlayerBlackAiType { get; set; }
        public GameEvaluationHeuristics PlayerWhiteAiGameEvaluationHeuristics{ get; set; }
        public GameEvaluationHeuristics PlayerBlackAiGameEvaluationHeuristics{ get; set; }

        public override string ToString()
        {
            if (PlayerWhite == PlayerType.Human && PlayerBlack == PlayerType.Human)
            {
                return $"Game setup: [Player white: {PlayerWhite}, Player black: {PlayerBlack}]";
            }

            if (PlayerWhite == PlayerType.AI && PlayerBlack == PlayerType.Human)
            {
                return $"Game setup: [Player white: {PlayerWhite}, AI Algorithm: {PlayerWhiteAiType}, Heuristic: {PlayerWhiteAiGameEvaluationHeuristics}. Player black: {PlayerBlack}]";
            }

            if (PlayerWhite == PlayerType.Human && PlayerBlack == PlayerType.AI)
            {
                return $"Game setup: [Player white: {PlayerWhite}. Player black: {PlayerBlack}, AI Algorithm: {PlayerBlackAiType}, Heuristic: {PlayerBlackAiGameEvaluationHeuristics}.]";
            }

            return $"Game setup: [Player white: {PlayerWhite}, AI Algorithm: {PlayerWhiteAiType}, Heuristic: {PlayerWhiteAiGameEvaluationHeuristics}. Player black: {PlayerBlack}, AI Algorithm: {PlayerBlackAiType}, Heuristic: {PlayerBlackAiGameEvaluationHeuristics}.]";
        }
    }
}