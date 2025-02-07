using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;
using Utils;

namespace Handlers
{
    public class BoardsHandler : MonoBehaviourSingleton<BoardsHandler>
    {
        
       // [Header("Pieces Data")] [SerializeField]
       // private HeuristicValue _heuristicValue;
       
        [Header("Pieces Data")]
        [SerializeField] public Piece blackPawn;
        [SerializeField] public Piece whitePawn;
        [SerializeField] public Piece blackRook;
        [SerializeField] public Piece whiteRook;
        [SerializeField] public Piece blackKnight;
        [SerializeField] public Piece whiteKnight;
        [SerializeField] public Piece blackBishop;
        [SerializeField] public Piece whiteBishop;
        [SerializeField] public Piece blackKing;
        [SerializeField] public Piece whiteKing;
        [SerializeField] public Piece blackQueen;
        [SerializeField] public Piece whiteQueen;
        
        [Header("References")]
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private GameObject transparentPrefab;
        [SerializeField] private Transform gridParent;

        [Header("Matrix")]
        public Piece[,] Pieces;
        public GameObject[,] PiecesDisplay;

        public int NumberOfNode;
        public Node Node;
        public GameManager GameManager;

        private void Start()
        {
            Time.timeScale = 1;
            
            // Pieces = new Piece[,]
            // {
            //     { blackRook, blackKnight, blackBishop, blackQueen, blackKing, blackBishop, blackKnight, blackRook },
            //     { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn },
            //     { null, null, null, null, null, null,null , null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn },
            //     { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook },
            // };
            
            // Pieces = new Piece[,]
            // {
            //     { blackRook, blackKnight, blackBishop, blackQueen, blackKing, blackBishop, blackKnight, blackRook },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null,null , null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook },
            // };
            
            // Pieces = new Piece[,]
            // {
            //     {null ,null ,whiteKing ,null ,null ,null ,null ,null},
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null,null , null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, whiteBishop, null, null, null, null },
            //     { null, null, whiteRook, blackKing, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     {null , null, null,null ,null ,null ,null ,null  },
            // };
            
            // Pieces = new Piece[,]
            // {
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, whiteBishop, null, null, null, null, null, null },
            //     { whiteKnight, whiteKnight, null, null, null, null, null, null },
            //     { blackPawn, blackKing, null, whiteKing, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            // };
            
            Pieces = new Piece[,]
            {
                { blackRook, blackKnight, blackBishop, null, blackKing, blackBishop, null, blackRook },
                { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, null, null, blackPawn },       
                { null, null, null, null, null, blackPawn, null, null },                                 
                { null, null, null, whiteKnight, null, null, null, null },                             
                { null, null, null, whitePawn, null, null, null, null },                                 
                { null, null, null, null, whitePawn, null, null, null },                                
                { whitePawn, whitePawn, whitePawn, null, null, whitePawn, whitePawn, whitePawn },       
                { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, null, whiteKnight, whiteRook }
            };
            
            // Pieces = new Piece[,]
            // {
            //     { whiteKing, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, whiteQueen, null, null, null, null, null, blackRook },
            //     { null, null, null, null, null, null, null, blackKing },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            // };

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
} 
