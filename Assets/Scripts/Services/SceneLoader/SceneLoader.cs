using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadStartScene() =>
        SceneManager.LoadScene(SceneNames.StartScene);

    public void RestartScene() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    public void LoadScene(string name) =>
        SceneManager.LoadScene(name);

    public void LoadScene(int index) => 
        SceneManager.LoadScene(index);

    public void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var maxSceneIndex = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex < maxSceneIndex)
            SceneManager.LoadScene(currentSceneIndex + 1);
        if (currentSceneIndex == maxSceneIndex)
        {
            Debug.Log("Last scene");
            SceneManager.LoadScene(0);
        }
    }
}