﻿using NineMensMorris.Logic.Consts;

namespace NineMensMorris.Logic.Models
{
    public class GameSetup
    {
        public PlayerType PlayerWhite { get; set; }
        public PlayerType PlayerBlack { get; set; }
        public AiAlgorithmType PlayerWhiteAiType { get; set; }
        public AiAlgorithmType PlayerBlackAiType { get; set; }
        public Heuristics PlayerWhiteAiHeuristics{ get; set; }
        public Heuristics PlayerBlackAiHeuristics{ get; set; }

        public override string ToString()
        {
            return $"Game setup: [Player white: {PlayerWhite}, Player black: {PlayerBlack}]";
        }
    }
}