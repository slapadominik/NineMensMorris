using System.Collections.Generic;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI
{
    public class Node
    {
        private readonly Color _color;
        public int Value { get; set; }
        public IList<Node> Childs { get; set; }

        public Node(Color color)
        {
            _color = color;
            Childs = new List<Node>();
        }
    }
}