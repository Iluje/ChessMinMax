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
        public int HeuristicValue;
        public Dictionary<Piece, int[,]> Bonus; 
        
        // Constructeur
        public Node(Piece[,] pieces, bool isWhiteTurn, bool isWhiteThinking)
        {
            Pieces = (Piece[,]) pieces.Clone();
            IsWhiteTurn = isWhiteTurn;
            IsWhiteThinking = isWhiteThinking;
        }
        public List<Node> Children()
        {
            List<Node> children = new List<Node>();
            
            for (int x = 0; x < Pieces.GetLength(0); x++)
            { 
                for (int y = 0; y < Pieces.GetLength(1); y++)
                {
                    if (Pieces[x, y] != null)
                    {
                        if (Pieces[x, y].isWhite == IsWhiteTurn)
                        {
                            Piece piece = Pieces[x, y];
                            Vector2Int position = new Vector2Int(x, y);
                            List<Vector2Int> availableMovement = piece.AvailableMovements(position, Pieces);
                            
                            foreach (Vector2Int movement in availableMovement)
                            {
                                Node node = new Node(Pieces, !IsWhiteTurn, IsWhiteThinking);
                                node.MovePiece(node.Pieces, piece, position,movement);
                                
                                node.HeuristicValue = node.HeursticValue();
                                children.Add(node);
                            }
                        }
                    }
                    
                    
                    // Piece piece = Pieces[x, y];
                    // if (piece != null)
                    // { 
                    //     Vector2Int position = new Vector2Int(x, y);
                    //     List<Vector2Int> availableMovement = piece.AvailableMovements(position);
                    //     
                    //     foreach (Vector2Int movement in availableMovement)
                    //     { 
                    //         Node node = new Node(Pieces, !IsWhiteTurn, IsWhiteThinking);
                    //         node.MovePiece(node.Pieces, piece, position,movement);
                    //         children.Add(node);
                    //     }
                    // } 
                }
            }
            return children;
        }

        public Piece[,] MovePiece(Piece[,] pieces, Piece piece, Vector2Int from, Vector2Int to)
        {
            // je stock dans une variable la piece qui est dans la position X et Y de la liste 2D.
            Piece NewPiece = Pieces[from.x, from.y];
            
            // je dplace la piece stocker dans à la position ou il peut aller.  
            pieces[to.x, to.y] = NewPiece;
            pieces[from.x, from.y] = null;
            
            // appeler la methode HeursticValue.
            //HeursticValue();
            
            return pieces;
        }
        
        public int HeursticValue()
        {
            return HeuristicPiecesValue() + HeuristicPlacementValue();
        }

        /**
         * Méthode d'évaluation d'heuristic pour les valeurs de pieces
         */
        private int HeuristicPiecesValue()
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

        private int HeuristicPlacementValue()
        {
            // SOLUTION 1
            // J'ai créer un dictionnaire, avec dedans une Piece, et un tableau 2D.
            // récuperer la position de la piece avec un double for. 
            // une fois les position récuperer, prendre la position et regarder quel est le type de la piece.
            // pour récuperer le type de la piece, il faut utiliser la fonction GetType() Exemple :
            //node.Pieces[x, y].GetType();
            // si c'est un Pawn, alors lui ajouter le bonus du tableau à la même position acutelle de la piece sur le jeu
            
            // SOLUTION 2
            // J'ai créer un dictionnaire, avec dedans une Piece, et un tableau 2D.
            // récuperer la position de la piece sur le node.
            // si la piece est == à BoardHandler.Instance.(nom de la piece)
            // alors il va se référer au tableau 2D qui possède comme piece Le même nom de la piece.

            return 0;
        }

        public int CalculatePlacementValue(Piece piece, Vector2Int position)
        {
           
            Debug.Log( "CalculatePlacementValue" + position + " : " + piece.name);
            // if (piece.name == " BlackRook")
            // {
            //     Bonus = new Dictionary<Piece, int[,]>()
            //     {
            //     
            //     }
            // }
            
                
        return 0;
        }
        
        public bool IsTerminal()
        {
            return false;
        }

        // private Piece[,] CreateCopy()
        // {
        //     if (Pieces == null) return null;
        //
        //     int rows = Pieces.GetLength(0);
        //     int cols = Pieces.GetLength(1);
        //     Piece[,] newPieces = new Piece[rows, cols];
        //
        //     for (int row = 0; row < rows; row++)
        //     {
        //         for (int col = 0; col < cols; col++)
        //         {
        //             if (Pieces[row, col] != null)
        //             {
        //                 newPieces[row, col] = Pieces[row, col];
        //             }
        //         }
        //     }
        //
        //     return newPieces;
        // }
    }
}