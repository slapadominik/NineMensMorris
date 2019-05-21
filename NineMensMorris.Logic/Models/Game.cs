using System;
using NineMensMorris.Logic.AI.Algorithms;
using NineMensMorris.Logic.AI.CaptureHeuristics;
using NineMensMorris.Logic.AI.MoveHeuristics;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Exceptions;
using NineMensMorris.Logic.Helpers;

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

        public Game(GameSetup gameSetup)
        {
            _board = new Board();
            SetUpGame(gameSetup);
            _currentPlayer = _playerWhite;
        }

        public MoveResult HumanMove(string from, string to)
        {
            if (_currentPlayer.PlayerType != PlayerType.Human)
            {
                throw new InvalidPlayerTypeException($"Invalid move! Current player type is AI.");
            }

            var moveResult = _board.Move(from, to, _currentPlayer.Color);

            if (moveResult.MoveType == MoveType.AddPiece ||
                GameConfiguration.GameStatus(_currentPlayer.Color) == GameStatus.Initialization &&
                moveResult.MoveType == MoveType.NewMill)
            {
                IncrementPlayersPieces();
            }

            if (moveResult.MoveType != MoveType.NewMill)
            {
                GameConfiguration.Moves++;
                SetCurrentPlayer(ColorHelper.GetOpponentColor(_currentPlayer.Color));
            }
            return moveResult;
        }

        public MoveResult HumanCapture(string location)
        {
            if (_board.GetPiece(location)?.Color != ColorHelper.GetOpponentColor(_currentPlayer.Color))
            {
                throw new IllegalMoveException($"Illegal capture move! Location {location} does not contain opponent's piece.");
            }
            _board.SetPiece(location, null);
            if (_currentPlayer.Color == Color.White)
            {
                GameConfiguration.BlackPieces--;
                if (GameConfiguration.BlackPieces == 0)
                {
                    return new MoveResult(_board, MoveType.WhiteWins, CurrentPlayer.Color);
                }
            }
            else
            {
                GameConfiguration.WhitePieces--;
                if (GameConfiguration.WhitePieces == 0)
                {
                    return new MoveResult(_board, MoveType.WhiteWins, CurrentPlayer.Color);
                }
            }
            var moveResult = new MoveResult(_board, MoveType.Capture, CurrentPlayer.Color);
            SetOpponentAsCurrentPlayer();
            GameConfiguration.Moves++;
            return moveResult;
        }

        public AiMoveResult AiMove()
        {
            if (_currentPlayer.PlayerType != PlayerType.AI)
            {
                throw new InvalidPlayerTypeException("Invalid move! Current player type is human.");
            }

            AiMoveResult moveResult;
            if (_currentPlayer.Color == Color.White)
            {
                moveResult = _aiWhiteMove.Move(_board, _currentPlayer.Color);
            }
            else
            {
                moveResult = _aiBlackMove.Move(_board, _currentPlayer.Color);
            }
            _board = moveResult.Board;
            if (moveResult.MoveType == MoveType.AddPiece ||
                GameConfiguration.GameStatus(_currentPlayer.Color) == GameStatus.Initialization &&
                moveResult.MoveType == MoveType.NewMill)
            {
                IncrementPlayersPieces();
            }

            SetOpponentAsCurrentPlayer();
            GameConfiguration.Moves++;
            return moveResult;
        }

        private void IncrementPlayersPieces()
        {
            if (_currentPlayer.Color == Color.White)
            {
                GameConfiguration.WhitePieces++;
            }
            else
            {
                GameConfiguration.BlackPieces++;
            }
        }

        public bool IsLocationEmpty(string location)
        {
            return _board.GetPiece(location) == null;
        }

        public bool DoesLocationContainFriendlyPiece(string location)
        {
            if (_board.GetPiece(location) == null)
            {
                return false;
            }
            return _board.GetPiece(location).Color == _currentPlayer.Color;
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
        }

        private void SetOpponentAsCurrentPlayer()
        {
            SetCurrentPlayer(ColorHelper.GetOpponentColor(_currentPlayer.Color));
        }

        private void SetCurrentPlayer(Color color)
        {
            if (color == Color.White)
            {
                _currentPlayer = _playerWhite;
            }
            else if (color == Color.Black)
            {
                _currentPlayer = _playerBlack;
            }
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
                    switch (gameConfig.PlayerWhiteAiGameEvaluationHeuristics)
                    {
                        case GameEvaluationHeuristics.PiecesCount:
                            _aiWhiteMove = new MinMaxAiMove(new PiecesCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                        case GameEvaluationHeuristics.MillsCount:
                            _aiWhiteMove = new MinMaxAiMove(new MillsCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                        default:
                            _aiWhiteMove = new MinMaxAiMove(new PiecesCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
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
                    switch (gameConfig.PlayerBlackAiGameEvaluationHeuristics)
                    {
                        case GameEvaluationHeuristics.PiecesCount:
                            _aiBlackMove = new MinMaxAiMove(new PiecesCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                        case GameEvaluationHeuristics.MillsCount:
                            _aiBlackMove = new MinMaxAiMove(new MillsCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
                            break;
                        default:
                            _aiBlackMove = new MinMaxAiMove(new PiecesCountGameEvaluationHeuristic(), new PiecesToMillCaptureHeuristic());
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