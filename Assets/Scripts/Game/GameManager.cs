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
            // créer une liste de int
            List<int> values = new List<int>();
            
            // récuper le node actuelle du plateau avec pour paramètre, Le plateau, qui joue le tour, et qui pense.
            Node currentNode = new Node(BoardsHandler.Instance.Pieces, isWhiteTurn, isWhiteTurn);
            
            // Pour chaque enfant de ce Node
            foreach (Node child in currentNode.Children())
            { 
                // stocker dans un int, le résulta de la mehide MinMax
                int max = AiHandler.MinMax(child, 1, !child.IsWhiteTurn); 
                
                // l'ajouter dans la liste
                values.Add(max);
                
                Debug.Log(" heur " + values.Count);
                
                // créer une variable int qui serra égal à - l'infini
                int bestValue = int.MinValue;
                Node bestChild = null;
                // pour chaque valeur, dans Values
                foreach (int value in values)
                {
                    // si la value est supèrieur a la précédente 
                    if (value > bestValue)
                    {
                        // alors la value est égal à la meilleur valeur
                        bestValue = value;
                        bestChild = child;
                        
                        Debug.Log("<color=red> Valeur du best child </color>" + bestChild.Value);
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
}