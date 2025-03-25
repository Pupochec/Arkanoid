using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public Animation BlackoutAnimation;
    public bool UseBlackout;

    private string LoadSceneName;

    public void LoadLevl(string LevlName)
    {
        Time.timeScale = 1f;
        BlackoutAnimation.Play();
        if (UseBlackout)
        {
            LoadSceneName = LevlName;
            Invoke(nameof(Load), 1f);
        }
        else
        {
            SceneManager.LoadScene(LevlName);
        }
    }

    private void Load()
    {
        SceneManager.LoadScene(LoadSceneName);
    }
}
