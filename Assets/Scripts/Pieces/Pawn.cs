using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Piece/Pawn")]
    public class Pawn : Piece
    {
        List<Vector2Int> _directionsBlackEat = new List<Vector2Int>
        {
            new Vector2Int(1, 1), new Vector2Int(1, -1)
        };
        
        List<Vector2Int> _directionsWhiteEat = new List<Vector2Int>
        {
            new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };

        public override List<Vector2Int> AvailableMovements(Vector2Int position, Piece[,] board)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            int i;

            if (isWhite == false)
            {
                if (position.x == 1)
                {
                    for (i = position.x + 1; i <= position.x + 2; i++)
                    {
                        if (board[i, position.y] == null)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (i = position.x + 1; i <= position.x + 1; i++)
                    {
                        if (board[i, position.y] == null)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                foreach (Vector2Int direction in _directionsBlackEat)
                {
                    Vector2Int testDirection = position + direction;

                    if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                    {
                        continue;
                    }

                    if (board[testDirection.x, testDirection.y] == null)
                    {
                        continue;
                    }

                    if (board[testDirection.x, testDirection.y].isWhite != isWhite)
                    {
                        movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                        continue;
                    }

                    continue;
                }
            }
            else
            {
                if (position.x == 6)
                {
                    for (i = position.x - 1; i >= position.x - 2; i--)
                    {
                        if (board[i, position.y] == null)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (i = position.x - 1; i >= position.x - 1; i--)
                    {
                        if (board[i, position.y] == null)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                foreach (Vector2Int direction in _directionsWhiteEat)
                {
                    Vector2Int testDirection = position + direction;

                    if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                    {
                        continue;
                    }

                    if (board[testDirection.x, testDirection.y] == null)
                    {
                        continue;
                    }

                    if (board[testDirection.x, testDirection.y].isWhite != isWhite)
                    {
                        movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                        continue;
                    }

                    continue;
                }
            }

            return movements;
        }
    }
}