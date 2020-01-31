using UnityEngine.SceneManagement;

public static class ScenesManager {

    public const string MainMenu = "Main";
    public const string Entrance = "Entrance";

    public static void LoadEntrance() {
        SceneManager.LoadScene(Entrance);
    }

    public static void LoadMain() {
        SceneManager.LoadScene(MainMenu);
    }
}
