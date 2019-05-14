using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Game _game;

        public Form1()
        {
            InitializeComponent();
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(700, 700, 200, 200);
            graphics.DrawEllipse(System.Drawing.Pens.Black, rectangle);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {            
            pictureBox1.Image = Resources.PieceBlack3;
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var box = sender as PictureBox;
            box.Image = Resources.PieceWhite1;
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            box.BackColor = Color.Transparent;
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            var box = sender as PictureBox;
            box.Image = Resources.PieceWhite1;
            pictureBox22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            box.BackColor = Color.Transparent;
            pictureBox22.Left -= 5;
            pictureBox22.Top -= 5;
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            GameSetup gameSetup = new GameSetup
            {
                PlayerWhite = _playerWhite,
                PlayerBlack = _playerBlack,
                PlayerWhiteAiType = AiAlgorithmType.MinMax,
                PlayerBlackAiType = AiAlgorithmType.MinMax,
                PlayerWhiteAiHeuristics = Heuristics.PiecesCount,
                PlayerBlackAiHeuristics = Heuristics.PiecesCount
            };
            logsListView.Items.Add(gameSetup.ToString());
            _game = new Game(gameSetup);
        }

        private void MoveAiButton_Click(object sender, EventArgs e)
        {
            try
            {
                _game.AiMove();
            }
            catch (Exception ex)
            {
                logsListView.Items.Add(ex.Message);
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
    }
}
