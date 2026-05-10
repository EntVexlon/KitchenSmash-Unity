
using UnityEngine.SceneManagement;

public static class MainSceneLoader
{
    public enum Scene
    {
        MainMenu,
        GameScene,
        LoaderScene
    }

    private static Scene target_scene;
    public static void LoadScene(Scene target_scene)
    {
        MainSceneLoader.target_scene = target_scene;
        SceneManager.LoadScene(Scene.LoaderScene.ToString());
    }

    public static Scene GetTargetScene() => target_scene;
}
