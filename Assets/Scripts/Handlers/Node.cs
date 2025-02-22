using System;
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
        public Dictionary<Type, int[,]> BonusWhite;
        public Dictionary<Type, int[,]> BonusBlack;

        public int PiecesLenghtRows;
        public int PiecesLenghtCols;
        
        // Constructeur
        public Node(Piece[,] pieces, bool isWhiteTurn, bool isWhiteThinking, int piecesLenghtRows, int piecesLenghtCols)
        {
            Pieces = (Piece[,]) pieces.Clone();
            IsWhiteTurn = isWhiteTurn;
            IsWhiteThinking = isWhiteThinking;

            PiecesLenghtRows = piecesLenghtRows;
            PiecesLenghtCols = piecesLenghtCols;
        }
        public List<Node> Children()
        {
            List<Node> children = new List<Node>();
            
            for (int x = 0; x < PiecesLenghtRows; x++)
            { 
                for (int y = 0; y < PiecesLenghtCols; y++)
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
                                Node node = new Node(Pieces, !IsWhiteTurn, IsWhiteThinking, BoardsHandler.Instance.valueLenghtRows, BoardsHandler.Instance.valueLenghtCols);
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
            int valueToAdd = 0;
            
            for (int x = 0; x < PiecesLenghtRows; x++)
            {
                for (int y = 0; y < PiecesLenghtCols; y++)
                {
                    Piece piece = Pieces[x, y];
                    
                    if (!piece)
                    {
                        continue;
                    }
                    
                    Type pieceType = piece.GetType();

                    if (IsWhiteThinking)
                    {
                        if (HeuristicPlacement.BonusWhite.TryGetValue(pieceType, out int[,] value))
                        {
                            valueToAdd = value[x, y];
                            Debug.Log("Blanc");
                        }
                    }
                    else
                    {
                        if (HeuristicPlacement.BonusBlack.TryGetValue(pieceType, out int[,] value))
                        {
                            valueToAdd = value[x, y];
                            Debug.Log("Blanc");
                        }
                    }
                }
            }
            
            return valueToAdd;
            
            
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
        }
        
        public bool IsTerminal()
        {
            return false;
        }
        
    }
}