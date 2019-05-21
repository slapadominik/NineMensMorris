using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;
using NineMensMorris.Properties;
using Color = System.Drawing.Color;

namespace NineMensMorris
{
    public partial class Form1 : Form
    {
        private PlayerType _playerWhite;
        private PlayerType _playerBlack;
        private GameEvaluationHeuristics _gameEvaluationHeuristicWhitePlayer = GameEvaluationHeuristics.PiecesCount;
        private GameEvaluationHeuristics _gameEvaluationHeuristicBlackPlayer = GameEvaluationHeuristics.PiecesCount;
        private AiAlgorithmType _aiAlgorithmPlayerWhite = AiAlgorithmType.MinMax;
        private AiAlgorithmType _aiAlgorithmPlayerBlack = AiAlgorithmType.MinMax;
        private IDictionary<string, PictureBox> _tiles;
        private int _playerWhiteMoves = 0;
        private int _playerBlackMoves = 0;
        private int _playerWhitePiecesInit = 0;
        private int _playerBlackPiecesInit = 0;
        private int _toCapture = 0;
        private PictureBox _locationFrom = null;
        private Game _game;

        private int AllMoves => _playerBlackMoves + _playerWhiteMoves;

        public Form1()
        {
            InitializeComponent();
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(700, 700, 200, 200);
            graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
            winnerPanel.Visible = false;
            currentPlayerPanel.Visible = false;
            _tiles = new Dictionary<string, PictureBox>
            {
                {Locations.A7,a7}, {Locations.D7, d7},{Locations.G7, g7},
                {Locations.B6,b6},  {Locations.D6, d6},{Locations.F6, f6},
                {Locations.C5, c5}, {Locations.D5, d5}, {Locations.E5, e5},
                {Locations.A4, a4}, {Locations.B4, b4}, {Locations.C4, c4}, {Locations.E4, e4}, {Locations.F4, f4}, {Locations.G4, g4},
                {Locations.C3, c3}, {Locations.D3, d3}, {Locations.E3, e3},
                {Locations.B2, b2}, {Locations.D2, d2}, {Locations.F2, f2},
                {Locations.A1, a1}, {Locations.D1, d1}, {Locations.G1, g1},
            };
        }

        public void DrawLShapeLine(System.Drawing.Graphics g, int intMarginLeft, int intMarginTop, int intWidth, int intHeight)
        {
            Pen myPen = new Pen(Color.Black);
            myPen.Width = 2;
            // Create array of points that define lines to draw.
            int marginleft = intMarginLeft;
            int marginTop = intMarginTop;
            int width = intWidth;
            int height = intHeight;
            int arrowSize = 3;
            Point[] points =
            {
                new Point(marginleft, marginTop),
                new Point(marginleft, height + marginTop),
                new Point(marginleft + width, marginTop + height),
                // Arrow
                new Point(marginleft + width - arrowSize, marginTop + height - arrowSize),
                new Point(marginleft + width - arrowSize, marginTop + height + arrowSize),
                new Point(marginleft + width, marginTop + height)
            };

            g.DrawLines(myPen, points);
        }

        private void d7_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void g7_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void a7_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void c5_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void d5_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void e5_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void d6_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void f6_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void b6_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }


        private void a4_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void b4_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void c4_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void e4_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void f4_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void g4_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void c3_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void d3_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void e3_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void f2_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void d2_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void b2_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void a1_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void d1_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }


        private void g1_Click(object sender, EventArgs e)
        {
            Move(sender as PictureBox);
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            GameSetup gameSetup = new GameSetup
            {
                PlayerWhite = _playerWhite,
                PlayerBlack = _playerBlack,
                PlayerWhiteAiType = _aiAlgorithmPlayerWhite,
                PlayerBlackAiType = _aiAlgorithmPlayerBlack,
                PlayerWhiteAiGameEvaluationHeuristics = _gameEvaluationHeuristicWhitePlayer,
                PlayerBlackAiGameEvaluationHeuristics = _gameEvaluationHeuristicBlackPlayer
            };
            logsListView.Items.Add(gameSetup.ToString());
            _game = new Game(gameSetup);
            currentPlayerPanel.Visible = true;
            currentPlayerLabel.Text = "White";
        }

        private void MoveAiButton_Click(object sender, EventArgs e)
        {
            try
            {
                var moveResult = _game.AiMove();
                ParseBoard(moveResult.Board);
                LogAiMove(moveResult);
                IncrementPlayersPiecesCounters(moveResult);
                SetAiStatistics(moveResult);
                SetCurrentPlayerText();
                SetAllMovesText();
            }
            catch (Exception ex)
            {
                logsListView.Items.Add(ex.Message);
            }
        }

        private void SetCurrentPlayerText()
        {
            if (GameConfiguration.Moves % 2 == 0)
            {
                currentPlayerLabel.Text = "white";
            }
            else
            {
                currentPlayerLabel.Text = "black";
            }
        }

        private void minmaxPlayerWhiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (minMaxPlayerBlackRadioButton.Checked)
            {
                Enum.TryParse(minmaxPlayerWhiteRadioButton.Text, out AiAlgorithmType aiAlgorithmType);
                _aiAlgorithmPlayerWhite = aiAlgorithmType;
            }
        }

        private void alphaBetaPlayerWhiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (alphaBetaPlayerWhiteRadioButton.Checked)
            {
                Enum.TryParse(alphaBetaPlayerWhiteRadioButton.Text, out AiAlgorithmType aiAlgorithmType);
                _aiAlgorithmPlayerWhite = aiAlgorithmType;
            }
        }

        private void minMaxPlayerBlackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (minMaxPlayerBlackRadioButton.Checked)
            {
                Enum.TryParse(minMaxPlayerBlackRadioButton.Text, out AiAlgorithmType aiAlgorithmType);
                _aiAlgorithmPlayerBlack = aiAlgorithmType;
            }
        }

        private void alphaBetaPlayerBlackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (alphaBetaPlayerBlackRadioButton.Checked)
            {
                Enum.TryParse(alphaBetaPlayerBlackRadioButton.Text, out AiAlgorithmType aiAlgorithmType);
                _aiAlgorithmPlayerBlack = aiAlgorithmType;
            }
        }

        private void piecesCountHeuristicPlayerWhiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (piecesCountHeuristicPlayerWhiteRadioButton.Checked)
            {
                var heurisitc = int.Parse((string)piecesCountHeuristicPlayerWhiteRadioButton.Tag);
                _gameEvaluationHeuristicWhitePlayer = (GameEvaluationHeuristics) heurisitc;
            }
        }

        private void millsCountHeuristicPlayerWhiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (millsCountHeuristicPlayerWhiteRadioButton.Checked)
            {
                var heurisitc = int.Parse((string) millsCountHeuristicPlayerWhiteRadioButton.Tag);
                _gameEvaluationHeuristicWhitePlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }

        private void piecesCountHeuristicPlayerBlackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (piecesCountHeuristicPlayerBlackRadioButton.Checked)
            {
                var heurisitc = int.Parse((string)piecesCountHeuristicPlayerBlackRadioButton.Tag);
                _gameEvaluationHeuristicBlackPlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }

        private void millsCountHeuristicPlayerBlackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (millsCountHeuristicPlayerBlackRadioButton.Checked)
            {
                var heurisitc = int.Parse((string)millsCountHeuristicPlayerBlackRadioButton.Tag);
                _gameEvaluationHeuristicBlackPlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }

        private void playerWhiteAIRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (playerWhiteAIRadioButton.Checked)
            {
                Enum.TryParse(playerWhiteAIRadioButton.Text, out PlayerType playerWhite);
                _playerWhite = playerWhite;
            }
        }

        private void playerWhiteHumanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (playerWhiteHumanRadioButton.Checked)
            {
                Enum.TryParse(playerWhiteHumanRadioButton.Text, out PlayerType playerWhite);
                _playerWhite = playerWhite;
            }
        }

        private void playerBlackHumanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (playerBlackHumanRadioButton.Checked)
            {
                Enum.TryParse(playerBlackHumanRadioButton.Text, out PlayerType playerBlack);
                _playerBlack = playerBlack;
            }
        }

        private void playerBlackAIRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (playerBlackAIRadioButton.Checked)
            {
                Enum.TryParse(playerBlackAIRadioButton.Text, out PlayerType playerBlack);
                _playerBlack = playerBlack;
            }
        }

        private void ParseBoard(Board board)
        {
            foreach (var location in board.GetLocations())
            {
                var piece = board.GetPiece(location);
                if (piece == null)
                {
                    RemovePieceFromTile(_tiles[location]);
                }
                else
                {
                   AddPieceToTile(_tiles[piece.Location], piece.Color);
                }
            }
        }

        private void Move(PictureBox pictureBox)
        {
            if (_game != null)
            {
                if (_game.CurrentPlayer.PlayerType == PlayerType.Human)
                {
                    try
                    {
                        if (_toCapture > 0)
                        {
                            CaptureMove(pictureBox.Name, pictureBox);
                        }
                        else if (GameConfiguration.GameStatus(_game.CurrentPlayer.Color) == GameStatus.Initialization)
                        {
                            NormalMove(null, pictureBox.Name, pictureBox);
                        }
                        else if (_locationFrom != null)
                        {
                            if (_game.IsLocationEmpty(pictureBox.Name))
                            {
                                NormalMove(_locationFrom.Name, pictureBox.Name, pictureBox);
                                RemovePieceFromTile(_locationFrom);
                                _locationFrom = null;
                            }
                            else
                            {
                                logsListView.Items.Add($"Location {pictureBox.Name} already contains piece.");
                            }
                        }
                        else if (_locationFrom == null)
                        {
                            if (_game.DoesLocationContainFriendlyPiece(pictureBox.Name))
                            {
                                _locationFrom = pictureBox;
                            }
                            else
                            {
                                logsListView.Items.Add($"Location {pictureBox.Name} does not contain friendly piece.");
                            }
                        }

                        SetCurrentPlayerText();
                    }
                    catch (Exception ex)
                    {
                        _locationFrom = null;
                        logsListView.Items.Add(ex.Message);
                    }
                }
                else
                {
                    logsListView.Items.Add("It's AI move right now. Press Move AI button to make move.");
                }
            }
            else
            {
                logsListView.Items.Add("Game has not started yet. Press button \"Start new game\"");
            }
        }

        private void NormalMove(string from, string to, PictureBox pictureBox)
        {
            var moveResult = _game.HumanMove(from, to);
            if (moveResult.MoveType == MoveType.NewMill)
            {
                _toCapture++;
            }
            logsListView.Items.Add($"{moveResult.PlayerColor}: MOVE {moveResult.MoveType} {from} -> {to}");
            IncrementPlayersPiecesCounters(moveResult);
            AddPieceToTile(pictureBox, moveResult.PlayerColor);
            allMovesLabel.Text = AllMoves.ToString();
        }

        private void CaptureMove(string to, PictureBox pictureBox)
        {
            var moveResult = _game.HumanCapture(to);
            logsListView.Items.Add($"{moveResult.PlayerColor}: CAPTURED {pictureBox.Name}");
            RemovePieceFromTile(pictureBox);
            _toCapture = 0;
            if (moveResult.MoveType == MoveType.BlackWins)
            {
                winnerPanel.Visible = true;
                winnerColorLabel.Text = "Black";
            }

            if (moveResult.MoveType == MoveType.WhiteWins)
            {
                winnerPanel.Visible = true;
                winnerColorLabel.Text = "White";
            }
        }

        private void AddPieceToTile(PictureBox pictureBox, Logic.Consts.Color color)
        {
            pictureBox.Image = color == Logic.Consts.Color.White ? Resources.PieceWhite : Resources.PieceBlack;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void RemovePieceFromTile(PictureBox pictureBox)
        {
            pictureBox.Image = Resources.blackcircle3;
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        }

        private void IncrementPlayersPiecesCounters(MoveResult moveResult)
        {
            if (moveResult.PlayerColor == Logic.Consts.Color.White)
            {
                playerWhiteMoves.Text = (++_playerWhiteMoves).ToString();
                if (_playerWhitePiecesInit < 9 && moveResult.MoveType == MoveType.AddPiece || moveResult.MoveType == MoveType.NewMill)
                {
                    playerWhitePiecesInit.Text = (++_playerWhitePiecesInit).ToString();
                }
            }
            else if (moveResult.PlayerColor == Logic.Consts.Color.Black)
            {
                playerBlackMoves.Text = (++_playerBlackMoves).ToString();
                if (_playerBlackPiecesInit < 9 && moveResult.MoveType == MoveType.AddPiece || moveResult.MoveType == MoveType.NewMill)
                {
                    playersBlackPiecesInit.Text = (++_playerBlackPiecesInit).ToString();
                }
            }
        }

        private void IncrementPlayersPiecesCounters(AiMoveResult moveResult)
        {
            if (moveResult.PlayerColor == Logic.Consts.Color.White)
            {
                playerWhiteMoves.Text = (++_playerWhiteMoves).ToString();
                if (_playerWhitePiecesInit < 9 && moveResult.MoveType == MoveType.AddPiece || moveResult.MoveType == MoveType.NewMill)
                {
                    playerWhitePiecesInit.Text = (++_playerWhitePiecesInit).ToString();
                }
            }
            else if (moveResult.PlayerColor == Logic.Consts.Color.Black)
            {
                playerBlackMoves.Text = (++_playerBlackMoves).ToString();
                if (_playerBlackPiecesInit < 9 && moveResult.MoveType == MoveType.AddPiece || moveResult.MoveType == MoveType.NewMill)
                {
                    playersBlackPiecesInit.Text = (++_playerBlackPiecesInit).ToString();
                }
            }
        }

        private void SetAllMovesText()
        {
            allMovesLabel.Text = GameConfiguration.Moves.ToString();
        }

        private void LogAiMove(AiMoveResult move)
        {
            logsListView.Items.Add($"{move.PlayerColor}: MOVE {move.MoveType}. Elapsed time: {move.Elapsed}. Nodes visited: {move.NodesVisited}");
        }

        private void SetAiStatistics(AiMoveResult move)
        {
            timeAiMoveLabel.Text = move.Elapsed.ToString();
            nodesVisitedLabel.Text = move.NodesVisited.ToString();
        }

        private void twoPieceConfWhiteHeuristic_CheckedChanged(object sender, EventArgs e)
        {
            if (twoPieceConfWhiteHeuristic.Checked)
            {
                var heurisitc = int.Parse((string)twoPieceConfWhiteHeuristic.Tag);
                _gameEvaluationHeuristicWhitePlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }

        private void threePieceConfWhiteHeuristic_CheckedChanged(object sender, EventArgs e)
        {
            if (threePieceConfWhiteHeuristic.Checked)
            {
                var heurisitc = int.Parse((string)threePieceConfWhiteHeuristic.Tag);
                _gameEvaluationHeuristicWhitePlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }

        private void twoPieceConfBlackHeuristic_CheckedChanged(object sender, EventArgs e)
        {
            if (twoPieceConfBlackHeuristic.Checked)
            {
                var heurisitc = int.Parse((string)twoPieceConfBlackHeuristic.Tag);
                _gameEvaluationHeuristicBlackPlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }

        private void threePieceConfBlackHeuristic_CheckedChanged(object sender, EventArgs e)
        {
            if (twoPieceConfBlackHeuristic.Checked)
            {
                var heurisitc = int.Parse((string)twoPieceConfBlackHeuristic.Tag);
                _gameEvaluationHeuristicBlackPlayer = (GameEvaluationHeuristics)heurisitc;
            }
        }
    }
}
