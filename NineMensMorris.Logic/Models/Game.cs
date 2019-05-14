using System.Security.Policy;
using NineMensMorris.Logic.AI.Algorithms;
using NineMensMorris.Logic.AI.CaptureHeuristics;
using NineMensMorris.Logic.AI.MoveHeuristics;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Exceptions;

namespace NineMensMorris.Logic.Models
{
    public class Game
    {
        private Board _board;
        private IAiMove _aiWhiteMove;
        private IAiMove _aiBlackMove;
        private Player _playerWhite;
        private Player _playerBlack;
        private Player _currentPlayer;
        private int _moves;

        public Game(GameSetup gameSetup)
        {
            _board = new Board();
            SetUpGame(gameSetup);
            _currentPlayer = _playerWhite;
        }

        public void HumanMove(string from, string to)
        {
            if (_currentPlayer.PlayerType != PlayerType.Human)
            {
                throw new InvalidPlayerTypeException();
            }
        }

        public void HumanCapture(string location)
        {
            //jesli mlynek
        }

        public void AiMove()
        {
            if (_currentPlayer.PlayerType != PlayerType.AI)
            {
                throw new InvalidPlayerTypeException();
            }

            MoveResult moveResult;
            if (_currentPlayer.Color == Color.White)
            {
                moveResult = _aiWhiteMove.Move(_board, _currentPlayer.Color);
            }
            else
            {
                moveResult = _aiBlackMove.Move(_board, _currentPlayer.Color);
            }
            _board = moveResult.Board;
            _moves++;

        }

        public int PossibleMovesCount(Color color)
        {
            return 0;
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
        }


        public int MovesCount()
        {
            return _moves;
        }

        private void SetUpGame(GameSetup gameConfig)
        {            
            if (gameConfig.PlayerWhite == PlayerType.Human)
            {
                _playerWhite = new Player(Color.White, PlayerType.Human);
            }
            else if (gameConfig.PlayerWhite == PlayerType.AI)
            {
                _playerWhite = new Player(Color.White, PlayerType.AI);
                if (gameConfig.PlayerWhiteAiType == AiAlgorithmType.MinMax)
                {
                    switch (gameConfig.PlayerWhiteAiHeuristics)
                    {
                        case Heuristics.PiecesCount:
                            _aiWhiteMove = new MinMaxAiMove(new PiecesCountMoveHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                        default:
                            _aiWhiteMove = new MinMaxAiMove(new PiecesCountMoveHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                    }
                }
                else if (gameConfig.PlayerWhiteAiType == AiAlgorithmType.AlphaBeta)
                {
                    _aiWhiteMove = new AlphaBetaAiMove();
                }
            }
            else
            {
                throw new InvalidConfigurationException($"Invalid game config.");
            }

            if (gameConfig.PlayerBlack == PlayerType.Human)
            {
                _playerBlack = new Player(Color.Black, PlayerType.Human);
            }
            else if (gameConfig.PlayerBlack == PlayerType.AI)
            {
                _playerBlack = new Player(Color.Black, PlayerType.AI);
                if (gameConfig.PlayerWhiteAiType == AiAlgorithmType.MinMax)
                {
                    switch (gameConfig.PlayerBlackAiHeuristics)
                    {
                        case Heuristics.PiecesCount:
                            _aiBlackMove = new MinMaxAiMove(new PiecesCountMoveHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                        default:
                            _aiBlackMove = new MinMaxAiMove(new PiecesCountMoveHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                    }
                }
                else if (gameConfig.PlayerBlackAiType == AiAlgorithmType.AlphaBeta)
                {
                    _aiBlackMove = new AlphaBetaAiMove();
                }
            }
            else
            {
                throw new InvalidConfigurationException($"Invalid game config.");
            }
        }
    }
}