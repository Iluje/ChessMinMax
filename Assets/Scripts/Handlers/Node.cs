using System.Collections.Generic;
using Pieces;
using UnityEngine;

namespace Handlers
{
    public class Node
    {
        // [,] = tableau à deux vecteur. ( tableau 2d )
        public Piece[,] Pieces;
        public bool IsWhiteTurn;
        public bool IsWhiteThinking;
        public int Depth = 2;
        
        // Constructeur
        public Node(Piece[,] pieces, bool isWhiteTurn, bool isWhiteThinking)
        {
            Pieces = (Piece[,]) pieces.Clone();
            IsWhiteTurn = isWhiteTurn;
            IsWhiteThinking = isWhiteThinking;
        }

        public Node(Node parentNode, Vector2Int fromMove, Vector2Int toMove)
        {
            
        }

        public bool IsTerminal()
        {
            return false;
        }
        
        public int HeursticValue()
        {
            int WhiteValue = 0;
            int BlackValue = 0;
            int HeuristicValue = 0;

            if (Pieces == null)
            {
                Debug.LogError("La liste _boardsHandler.Pieces est null!");
            }

            foreach (Piece piece in Pieces)
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
            }
            
            if (IsWhiteThinking) HeuristicValue = WhiteValue - BlackValue;
            else HeuristicValue = BlackValue - WhiteValue;

            return HeuristicValue;
        }

        public List<Node> Children()
        {
            List<Node> children = new List<Node>();
            for (int x = 0; x < Pieces.GetLength(0); x++)
            { 
                for (int y = 0; y < Pieces.GetLength(1); y++)
                {
                    Piece piece = Pieces[x, y];
                    if (piece != null) //&& piece.isWhite == IsWhiteTurn)
                    { 
                       // Debug.Log(Pieces[x, y].name);
                            
                        Vector2Int position = new Vector2Int(x, y);
                        List<Vector2Int> availableMovement = piece.AvailableMovements(position);
                        
                        foreach (Vector2Int movement in availableMovement)
                        { 
                            Node node = new Node(Pieces, !IsWhiteTurn, IsWhiteThinking);
                            node.MovePiece(node.Pieces, piece, position,movement);
                            children.Add(node);
                        }
                        
                        // Position = position;
                    } 
                }
            }
            return children;
        }

        public Piece[,] MovePiece(Piece[,] pieces, Piece piece, Vector2Int from, Vector2Int to)
        {
            Piece NewPiece = Pieces[from.x, from.y];
            pieces[to.x, to.y] = NewPiece;
            pieces[from.x, from.y] = null;
            HeursticValue();
            Debug.Log("<color=Yellow> Piece </color>"+ Pieces[from.x, from.y] + piece.name + " De " + from.x + "," + from.y +" à " + to.x + "," + to.y + " valeur de " + HeursticValue());
            
            return pieces;
        }

        private Piece[,] CreateCopy()
        {
            if (Pieces == null) return null;

            int rows = Pieces.GetLength(0);
            int cols = Pieces.GetLength(1);
            Piece[,] newPieces = new Piece[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (Pieces[row, col] != null)
                    {
                        newPieces[row, col] = Pieces[row, col];
                    }
                }
            }

            return newPieces;
        }
    }
}