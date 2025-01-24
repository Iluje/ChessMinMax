using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Rook", menuName = "Piece/Rook")]
    public class Rook : Piece
    {
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            int i;
            int j;
            
            for (i = position.x + 1; i <= 7; i++) 
            { 
                if (BoardsHandler.Instance.Pieces[i, position.y] == null)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[i, position.y].isWhite != isWhite)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (i = position.x - 1; i >= 0; i--) 
            { 
                if (BoardsHandler.Instance.Pieces[i, position.y] == null) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[i, position.y].isWhite != isWhite) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            for (j = position.y + 1; j <= 7; j++) 
            { 
                if (BoardsHandler.Instance.Pieces[position.x, j] == null)
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[position.x, j].isWhite != isWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (j = position.y - 1; j >= 0; j--) 
            { 
                if (BoardsHandler.Instance.Pieces[position.x, j] == null) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (BoardsHandler.Instance.Pieces[position.x, j].isWhite != isWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            return movements;
        }
    }
}