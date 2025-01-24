using UnityEngine;

namespace Game
{
    public class CanvasManagerScene0 : MonoBehaviour
    {
        // Changer de Scene
        public void ChangeSceneByIndex(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }

        // Quit Game
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}