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
        // public Vector2Int Position;
        
        // Constructeur
        public Node(Piece[,] pieces, bool isWhiteTurn)
        {
            Pieces = (Piece[,]) pieces.Clone();
            IsWhiteTurn = isWhiteTurn;
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

            HeuristicValue = WhiteValue -= BlackValue;
            //Debug.Log(HeuristicValue);
            return HeuristicValue;
        }

        public List<Node> Children()
        {
            //List<Node> Childrens = new List<Node>();
            for (int x = 0; x < Pieces.GetLength(0); x++)
            { 
                for (int y = 0; y < Pieces.GetLength(1); y++)
                {
                    // Piece piece = Pieces[x, y];
                    // if (piece != null) //&& piece.isWhite == IsWhiteTurn)
                    // { 
                    //     Debug.Log(Pieces[x, y].name);
                    //         
                    //     Vector2Int position = new Vector2Int(x, y);
                    //     List<Vector2Int> availableMovement = piece.AvailableMovements(position);
                    //     
                    //     foreach (Vector2Int movement in availableMovement)
                    //     { 
                    //         Node node = new Node(Pieces, false);
                    //         //Childrens.Add(node);
                    //         node.MovePiece(node.Pieces, piece, position,movement); 
                    //         MovePiece(Pieces, piece,position, movement );
                    //     }
                    //         // Position = position;
                    // } 
                }
            }

            return Children();
            // instentie ine list de node qui s'appelle children

            // Pour chaque piece sur pieces
            // recuperer les mouvements disponible de la piece
            // pour chaque mouvement disponible
            // créer une copy de pieces avec le mouvement fait
            // ajoute cette copy a children



            // List<Node> children = new List<Node>();
            // // Pour chaque movement possible
            // Piece[,] pieces = CreateCopy();
            // Node node = new Node(pieces, false);
            // MovePiece(node.Pieces, node.IsWhiteTurn);
            // children.Add(node);
        }

        public Piece[,] MovePiece(Piece[,] pieces, Piece piece, Vector2Int from, Vector2Int to)
        {
            // Déplacement de la piece sur le pieces
            Piece NewPiece = piece;
            Debug.Log("<color=Yellow> New piece </color>"+ " De " + from.x + "," + from.y +" à " + to.x + "," + to.y);
            //Debug.Log("<color=green> new piece </color>" + NewPiece + "de" +  pieces[from.x, from.y] +" à "+ pieces[to.x, to.y]);
            pieces[from.x, from.y] = null;
            //pieces[to.x, to.y]  = NewPiece;
            
            //Debug.Log(OldPiece.name + "Bouge de" + from + "à" + to.x + "," + to.y);
            
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