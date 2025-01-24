using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Knight", menuName = "Piece/Knight")]
    public class Knight : Piece
    {
        List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(2, 1), new Vector2Int(2, -1), new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, 2), new Vector2Int(-1, 2), new Vector2Int(1, -2), new Vector2Int(-1, -2)
        };
        
        
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            List<Vector2Int> movements = new List<Vector2Int>();
            

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position + direction;

                if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                {
                    continue;
                }
                if (BoardsHandler.Instance.Pieces[testDirection.x, testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(testDirection.x, testDirection.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[testDirection.x, testDirection.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(testDirection.x, testDirection.y)); 
                    continue;
                }
                continue;
            }
            
            return movements;
        }
    }
}