using System.Collections.Generic;
using Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
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
        }
        
        [ContextMenu("Think")]
        private void Think()
        {
            List<int> values = new List<int>();
            Node currentNode = new Node(BoardsHandler.Instance.Pieces, isWhiteTurn, isWhiteTurn);
            foreach (Node child in currentNode.Children())
            { 
                int max = AiHandler.MinMax(child, 1, !child.IsWhiteTurn); 
                
                
                values.Add(AiHandler.MinMax(child, 1, !child.IsWhiteTurn));
                Debug.Log(" heur " + values.Count);
                
                int bestValue = int.MinValue;
                foreach (int value in values)
                {
                    if (value > bestValue)
                    {
                        //Debug.Log("Heuristic : " + value);
                        bestValue = value;
                    }
                }
                
                Debug.Log("Heuristic : " + max + " child is ");
                Debug.Log("<color=red> Heuristic best Value </color>" + bestValue);
            }
        }
    }
}