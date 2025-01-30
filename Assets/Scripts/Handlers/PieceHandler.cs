using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Handlers
{
    public class PieceHandler : MonoBehaviour, IPointerClickHandler
    {
        public Piece Piece;
        private Image _image;
        private Vector2Int _position;
        private bool _isMovement;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Setup(Piece piece, Vector2Int position)
        {
            Piece = piece;
            _image.sprite = piece.sprite;
            _position = position;
        }

        public void SetupTransparent(Vector2Int position)
        {
            _position = position;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Piece != null && GameManager.Instance.isWhiteTurn == Piece.isWhite)
            {
                if (_isMovement == false)
                {
                    GameManager.Instance.lastClickGameObject = gameObject.GetComponent<PieceHandler>();
                
                    BoardsHandler.Instance.ResetMatrix();
                    BoardsHandler.Instance.DisplayMatrix();
            
                    List<Vector2Int> availableMovements = Piece.AvailableMovements(_position);
                   
                    
                    foreach (Vector2Int availableMovement in availableMovements)
                    {
                        BoardsHandler.Instance.PiecesDisplay[availableMovement.x, availableMovement.y].GetComponent<PieceHandler>()._isMovement = true;
                        BoardsHandler.Instance.PiecesDisplay[availableMovement.x, availableMovement.y].GetComponent<Image>().color = new Color(1, 1, 0, 0.5f);
                    }
                
                    availableMovements.Clear();
                }
            }
            
            if (_isMovement)
            {
                // Audio
                GameManager.Instance.audioManager.GetComponent<AudioSource>().Play();
                
                // Movement
                Vector2Int lastPosition = GameManager.Instance.lastClickGameObject._position;
                BoardsHandler.Instance.Pieces[lastPosition.x, lastPosition.y] = null;
                BoardsHandler.Instance.Pieces[_position.x, _position.y] = GameManager.Instance.lastClickGameObject.Piece;
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix();

                GameManager.Instance.isWhiteTurn = !GameManager.Instance.isWhiteTurn;
            }
        }
    }
}