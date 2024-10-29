using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button StartGame;

    private void Start()
    {
        StartGame.onClick.AddListener(() =>
        {
            App.Instance.sceneChange(1);
        });
    }

}
