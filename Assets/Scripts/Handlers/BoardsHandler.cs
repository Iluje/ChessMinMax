using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;
using Utils;

namespace Handlers
{
    public class BoardsHandler : MonoBehaviourSingleton<BoardsHandler>
    {
        [Header("Pieces Data")] [SerializeField]
        private HeuristicValue _heuristicValue;
        
        [Header("Pieces Data")]
        [SerializeField] private Piece blackPawn;
        [SerializeField] private Piece whitePawn;
        [SerializeField] private Piece blackRook;
        [SerializeField] private Piece whiteRook;
        [SerializeField] private Piece blackKnight;
        [SerializeField] private Piece whiteKnight;
        [SerializeField] private Piece blackBishop;
        [SerializeField] private Piece whiteBishop;
        [SerializeField] private Piece blackKing;
        [SerializeField] private Piece whiteKing;
        [SerializeField] private Piece blackQueen;
        [SerializeField] private Piece whiteQueen;
        
        [Header("References")]
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private GameObject transparentPrefab;
        [SerializeField] private Transform gridParent;

        [Header("Matrix")]
        public Piece[,] Pieces;
        public GameObject[,] PiecesDisplay;
        
        

        private void Start()
        {
            Time.timeScale = 1;
            
            Pieces = new Piece[,]
            {
                { blackRook, blackKnight, blackBishop, blackQueen, blackKing, blackBishop, blackKnight, blackRook },
                { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn },
                { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook },
            };
            DisplayMatrix();
        }

        public void DisplayMatrix()
        {
            PiecesDisplay = new GameObject[Pieces.GetLength(0), Pieces.GetLength(1)];
            
            PawnToQueen();
            
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    GameObject newPiece;
                    
                    if (Pieces[i, j] != null)
                    {
                        // Instancier un prefab Image pour chaque élément
                        newPiece = Instantiate(piecePrefab, gridParent);
                        newPiece.GetComponent<PieceHandler>().Setup(Pieces[i, j], new Vector2Int(i, j));
                    }
                    else
                    {
                        newPiece = Instantiate(transparentPrefab, gridParent);
                        newPiece.GetComponent<PieceHandler>().SetupTransparent(new Vector2Int(i, j));
                    }
                    PiecesDisplay[i, j] = newPiece;
                    
                }
            }
            IsEndGame();
        }
        public void GetHeuristicValue()
        {
            
        }
        
        
        
        public void ResetMatrix()
        {
            foreach (Transform child in gridParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void PawnToQueen()
        {
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                if (Pieces[0, i] == whitePawn)
                {
                    Pieces[0, i] = whiteQueen;
                }
            }
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                if (Pieces[7, i] == blackPawn)
                {
                    Pieces[7, i] = blackQueen;
                }
            }
        }

        public void IsEndGame()
        {
            GameManager.Instance.isBlackKing = false;
            GameManager.Instance.isWhiteKing = false;
            
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    if (Pieces[i, j] == blackKing)
                    {
                        GameManager.Instance.isBlackKing = true;
                    }
                    if (Pieces[i, j] == whiteKing)
                    {
                        GameManager.Instance.isWhiteKing = true;
                    }
                }
            }

            if (GameManager.Instance.isBlackKing == false)
            {
                Time.timeScale = 0;
                GameManager.Instance.endGamePanel.SetActive(true);
                GameManager.Instance.endGameText.text = " Victory White Player ! ";
            }
            if (GameManager.Instance.isWhiteKing == false)
            {
                Time.timeScale = 0;
                GameManager.Instance.endGamePanel.SetActive(true);
                GameManager.Instance.endGameText.text = " Victory Black Player ! ";
            }
        }
    }
    
    public class Node
    {
        // [,] = tableau à deux vecteur. ( tableau 2d )
        public Piece[,] Pieces;
        public bool IsWhiteTurn;
        public Node() {}
        
        public Node(Piece[,] pieces, bool isWhiteTurn)
        {
            Pieces = pieces;
            IsWhiteTurn = isWhiteTurn;
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
            Debug.Log(HeuristicValue);
            return HeuristicValue;
        }

        // public List<Node> Children()
        // {
        //     List<Node> children = new List<Node>();
        //     // Pour chaque movement possible
        //     Piece[,] pieces = CreateCopy();
        //     Node node = new Node(pieces, false);
        //     MovePiece(node.Pieces);
        //     children.Add(node);
        // }

        public Piece[,] MovePiece(Piece[,] pieces, Piece piece, Vector2Int from, Vector2Int to)
        {
            // Déplacement de la piece sur le pieces
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