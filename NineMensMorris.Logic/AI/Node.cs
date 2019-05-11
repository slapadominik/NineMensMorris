using System.Collections.Generic;
using NineMensMorris.Logic.Consts;
using NineMensMorris.Logic.Models;

namespace NineMensMorris.Logic.AI
{
    public class Node
    {
        public int Value { get; set; }
        public Board Board { get; set; }
        public IList<Node> Childs { get; set; }


        private readonly Color _color;
        public Node(Color color)
        {
            _color = color;
            Childs = new List<Node>();
        }

        public void AddState(Node node)
        {
            Childs.Add(node);
        }
    }
}