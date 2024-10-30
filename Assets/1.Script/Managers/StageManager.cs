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

    bool stagenum = false;
    private GameObject explosion;
    public GameObject explosionprefab;
    public ParticleSystem expeffect;

    private void Start()
    {

        explosion = Instantiate(explosionprefab);
        expeffect = explosion.GetComponent<ParticleSystem>();
        road1 = Instantiate(stage[0]);
        stage1 = Instantiate(stage[1]);
        Instantiate(gameobject_UI);
        Env_Create();
        App.Instance.Explosion = reachdesti;
        //GameObject temp = new GameObject();  // TODO : TestCode
        //temp.AddComponent<TestChageStage>().stageManager = this; // TODO : TestCode
        App.Instance.changestage = EndGame;
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
        explosion.transform.position = App.Instance.Waypoint[App.Instance.Waypoint.Length - 1];
    }


    public void ChangeStage()
    {
        StartCoroutine(Sinkstage1());
        GameObject road = Instantiate(stage[2]);
        //App.Instance.Waypoint = road.GetComponent<StageWayPoint>().waypoint;
        explosion.transform.position = App.Instance.Waypoint[App.Instance.Waypoint.Length - 1];
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
            explosion.transform.position = App.Instance.Waypoint[App.Instance.Waypoint.Length - 1];
            //Debug.Log($" K is {k}");

        }
    }

    public void EndGame()
    {
        if (!stagenum)
        {
            //Debug.Log($" click endgame");
            stagenum = !stagenum;
            ChangeStage();
        }
        else
        {
            //Debug.Log($" click endgame");
            //App.Instance.AddScore(nameofPlayer,score);  // TODO : need nameofPlayer , score;
            Invoke("GoNextScene", 2);
        }
    }

    private void GoNextScene()
    {
        Debug.Log($" change scene");
        App.Instance.sceneChange(2);
    }

    void reachdesti()
    {
        explosion.SetActive(true);
        expeffect.Play();
        StartCoroutine(sleep());
    }

    private IEnumerator sleep()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log($" stop effect");
        expeffect.Stop();
        explosion.SetActive(false);
    }
}
