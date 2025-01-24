using System.Collections.Generic;
using UnityEngine; 

namespace Pieces
{
    public abstract class Piece : ScriptableObject
    {
        public Sprite sprite;
        public bool isWhite;
        public int Point;

        public abstract List<Vector2Int> AvailableMovements(Vector2Int position);
    }
}