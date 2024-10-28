using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stage;
    public GameObject[] env_Obj;

    private void Start()
    {
        Instantiate(stage[0]);
        Instantiate(stage[1]);
        Env_Create();
    }

    private void Env_Create()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i + 5.5f, 0, j - 3.5f), Quaternion.identity);
            }
        }
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i - 17.5f, 0, j - 3.5f), Quaternion.identity);
            }
        }

        for (int i = 0; i < 29; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i - 17.5f, 0, j + 6.5f), Quaternion.identity);
            }
        }

        for (int i = 0; i < 29; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Instantiate(env_Obj[UnityEngine.Random.Range(0, env_Obj.Length)], new Vector3(i - 12.5f, 0, j - 14.5f), Quaternion.identity);
            }
        }
    }

}
