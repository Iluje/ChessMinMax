using Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
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
                Debug.Log("change");
            }
        }

        private void Start()
        {
            
        }

        private void TestGame()
        {
            
        }
        
        [ContextMenu("Think")]
        private void Think()
        {
            Node currentNode = new Node(BoardsHandler.Instance.Pieces, true);
            Debug.Log(currentNode.HeursticValue());
            Debug.Log(currentNode.Children());
        }
    }
}