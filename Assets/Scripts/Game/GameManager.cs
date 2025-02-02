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

            if (Input.GetButtonDown("Fire2"))
            {
                Think();
            }
        }
        
        [ContextMenu("Think")]
        private void Think()
        {
            Node currentNode = new Node(BoardsHandler.Instance.Pieces, isWhiteTurn, isWhiteTurn);

            List<Node> children = currentNode.Children();
            int bestValue = int.MinValue;
            Node bestChild = null;
            
            
            foreach (Node child in children)
            {
                int value = child.HeursticValue();
                if (value > bestValue)
                {
                    bestValue = value;
                    bestChild = child;
                }
            }
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.Pieces = bestChild.Pieces;
            BoardsHandler.Instance.DisplayMatrix();

            //BoardsHandler.Instance.DisplayMatrix();

            //Debug.Log("Heuristic : " + max + " child is ");
            //Debug.Log("<color=red> Heuristic best Value </color>" + bestValue + " tour des joueurs blanc ?" + isWhiteTurn);
            //BoardsHandler.Instance.Pieces = bestChild.Pieces;

            //Debug.Log("<color=green> Board count  </color> " + BoardsHandler.Instance.Pieces);
        }
    }
}