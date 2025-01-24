using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CanvasManagerScene1 : MonoBehaviour
    {
        // Timer
        private bool _startTimers;
        private float _blackElapsedTime;
        private float _whiteElapsedTime;
        
        [Header("Timer")]
        public Text blackTimerText;
        public Text whiteTimerText;
        
        // Turn
        [Header("Turn Text")]
        [SerializeField] private Text playerTurnText;
        
        private void Update()
        {
            // Timer
            if (Input.GetButtonDown("Fire1"))
            {
                _startTimers = true;
            }
            
            if (_startTimers)
            {
                if (GameManager.Instance.isWhiteTurn)
                {
                    _whiteElapsedTime += Time.deltaTime;
                    UpdateWhiteTimerText(_whiteElapsedTime);
                }
                else
                {
                    _blackElapsedTime += Time.deltaTime;
                    UpdateBlackTimerText(_blackElapsedTime);
                }
            }
            
            // Turn
            PlayerTurnText();
        }

        void UpdateBlackTimerText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
            int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);

            blackTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        
        void UpdateWhiteTimerText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
            int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);

            whiteTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        
        // Turn
        public void PlayerTurnText()
        {
            if (GameManager.Instance.isWhiteTurn)
            {
                playerTurnText.text = " White Player Turn ";
            }
            else
            {
                playerTurnText.text = " Black Player Turn ";
            }
        }
    }
}
