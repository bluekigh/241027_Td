using UnityEngine;

public class Quitmanager : MonoBehaviour
{
    private void Start()
    {
        Invoke("Quit", 3);
    }

    void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit();
#endif
    }
}
