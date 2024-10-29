using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stage;
    public GameObject[] env_Obj;
    public GameObject gameobject_UI;   // TODO : Final remove;
    GameObject road1;
    GameObject stage1;
    private void Start()
    {
        road1 = Instantiate(stage[0]);
        stage1 = Instantiate(stage[1]);
        Instantiate(gameobject_UI);
        Env_Create();
    }

    private void Env_Create()
    {

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i + 5.5f, 0, j - 3.5f), Quaternion.identity, this.gameObject.transform);
            }
        }
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i - 17.5f, 0, j - 3.5f), Quaternion.identity, this.gameObject.transform);
            }
        }

        for (int i = 0; i < 29; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i - 17.5f, 0, j + 6.5f), Quaternion.identity, this.gameObject.transform);
            }
        }

        for (int i = 0; i < 29; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i - 12.5f, 0, j - 14.5f), Quaternion.identity, this.gameObject.transform);
            }
        }
    }


    public void ChangeStage()
    {
        StartCoroutine(Sinkstage1());
        Instantiate(stage[2]);
        Instantiate(stage[3]);
    }

    private IEnumerator Sinkstage1()
    {
        Debug.Log($" Sinkstage1");
        float k = 0;
        Vector3 positions = Vector3.zero;
        while (k > -5)
        {
            yield return null;
            k -= 0.05f;
            positions.y += k;
            road1.transform.position = positions;
            stage1.transform.position = positions;
            Debug.Log($" K is {k}");

        }
    }

    public void EndGame()
    {
        Debug.Log($" click endgame");
        //App.Instance.AddScore(nameofPlayer,score);  // TODO : need nameofPlayer , score;
        Invoke("GoNextScene", 2);
    }

    private void GoNextScene()
    {
        Debug.Log($" change scene");
        App.Instance.sceneChange(2);
    }
}
