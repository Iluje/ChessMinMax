using System.Collections.Generic;
using Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public int Depht;
        
        public AiHandler AiHandler;
        [Header("Selected Piece")]
        public PieceHandler lastClickGameObject;

        [Header("End Game")]
        public GameObject endGamePanel;
        public Text endGameText;

        [Header("Sound")]
        public GameObject audioManager;
        
        [Header("Data")]
        public bool isWhiteTurn = true;
        public bool isBlackKing;
        public bool isWhiteKing;

        
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetButtonDown("Fire2"))
            {
               ThinkMinMax();
               //ThinkAlphaBeta();
            }
        }
        
        [ContextMenu("Think")]
        private void ThinkMinMax()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            Node currentNode = new Node(BoardsHandler.Instance.Pieces, isWhiteTurn, isWhiteTurn, BoardsHandler.Instance.valueLenghtRows, BoardsHandler.Instance.valueLenghtCols);
            
            List<Node> children = currentNode.Children(); 
            int bestValue = int.MinValue;

            Node bestChild = null;
            
            foreach (Node child in children)
            {
                int value = AiHandler.MinMax(child, Depht, false);
                
                 if (value > bestValue)
                 {
                     bestValue = value;
                     bestChild = child;
                 }
            }

            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.Pieces = bestChild.Pieces;
            BoardsHandler.Instance.DisplayMatrix();
            isWhiteTurn = !isWhiteTurn;
            
            stopwatch.Stop();
            Debug.Log(stopwatch.ElapsedMilliseconds + " ms ");
        }

        // private void ThinkAlphaBeta()
        // {
        //     Stopwatch stopwatch = Stopwatch.StartNew();
        //     
        //     Node currentNode = new Node(BoardsHandler.Instance.Pieces, isWhiteTurn, isWhiteTurn, BoardsHandler.Instance.valueLenghtRows, BoardsHandler.Instance.valueLenghtCols);
        //     
        //     List<Node> children = currentNode.Children(); 
        //     int bestValue = int.MinValue;
        //     Node bestChild = null;
        //     
        //     foreach (Node child in children)
        //     {
        //         int value = AiHandler.AlphaBeta(child, Depht, 0,0, false);
        //         
        //         if (value > bestValue)
        //         {
        //             bestValue = value;
        //             bestChild = child;
        //         }
        //         //Debug.Log(" best " + bestValue);
        //     }
        //
        //     BoardsHandler.Instance.ResetMatrix();
        //     BoardsHandler.Instance.Pieces = bestChild.Pieces;
        //     BoardsHandler.Instance.DisplayMatrix();
        //     isWhiteTurn = !isWhiteTurn;
        //     
        //     stopwatch.Stop();
        //     Debug.Log(stopwatch.ElapsedMilliseconds + " ms ");
        // }
    }
}