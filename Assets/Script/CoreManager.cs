using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreManager : MonoBehaviour
{
    public void GameOver()
    {
        SceneManager.LoadScene("GameEndScene");
    }
}
