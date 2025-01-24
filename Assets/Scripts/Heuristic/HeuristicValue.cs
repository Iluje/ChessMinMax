using Handlers;
using Pieces;
using UnityEngine;
using UnityEngine.Serialization;

public class HeuristicValue : MonoBehaviour
{
    [SerializeField] BoardsHandler _boardsHandler;
    
    public int WhiteValue;
    public int BlackValue;
    public int WhiteHeuristic;
    public int BlackHeuristic;

    private void Start()
    {
        
    }
    
    public void ResetPoint()
    {
        if (_boardsHandler == null)
        {
            Debug.LogError("_boardsHandler est null!");
            return;
        }

        if (_boardsHandler.Pieces == null)
        {
            Debug.LogError("La liste _boardsHandler.Pieces est null!");
            return;
        }

        foreach (Piece piece in _boardsHandler.Pieces)
        {
            if (piece != null)
            {
                if (piece.isWhite)
                {
                    WhiteValue += piece.Point;
                }
                else
                { 
                    BlackValue += piece.Point;
                }
            }
            else
            {
                Debug.Log("Null Piece");
            }
        }
    }
}
