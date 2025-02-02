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
        public int Value;
        
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

        public List<Node> Children()
        {
            // Créer une liste de Node qui serront appeler Children
            List<Node> children = new List<Node>();
            
            // parcourir la liste multidimentionelle sur l'axe X et Y et s'arrêter au moment ou la liste est fini.
            for (int x = 0; x < Pieces.GetLength(0); x++)
            { 
                for (int y = 0; y < Pieces.GetLength(1); y++)
                {
                    // créer une piece de type Piece à la position qui est acutellement parcourue
                    
                    Piece piece = Pieces[x, y];
                    if (piece != null) //&& piece.isWhite == IsWhiteTurn)
                    { 
                        
                        // Sur une liste de vector2Int qui s'appelle availableMovement, je lui dit qu'elle est égal à piece.AvailableMovements(position);
                        
                        Vector2Int position = new Vector2Int(x, y);
                        List<Vector2Int> availableMovement = piece.AvailableMovements(position);
                        
                        foreach (Vector2Int movement in availableMovement)
                        { 
                            // pour chaque Vector2int ( movement ) créer un node à l'aide du constructeur ( une copie enfant du Node)  .
                            // Comme paramètre du node enfant créer, je lui le tableau 2D, /// le reste j'ai pas compris pk.
                            Node node = new Node(Pieces, !IsWhiteTurn, IsWhiteThinking);
                            
                            // j'appel la methode MovePiece avec pour le tableau 2d, la position de la piece, et le movement possible.
                            node.MovePiece(node.Pieces, piece, position,movement);
                            
                            // dans la liste d'enfant du node, je lui ajoute le node, une fois le movePiece effectué.
                            children.Add(node);
                            
                            // résulta, j'ai un Node, ce Node possède une liste de node enfant avec un mouvement effectuer pour chaque enfant.
                        }
                        
                        // Position = position;
                    } 
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
            HeursticValue();
            //Debug.Log("<color=Yellow> Piece </color>"+ Pieces[from.x, from.y] + piece.name + " De " + from.x + "," + from.y +" à " + to.x + "," + to.y + " valeur de " + HeursticValue());
            
            return pieces;
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

            Value = HeuristicValue;
            return HeuristicValue;
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