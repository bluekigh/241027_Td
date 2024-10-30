using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lookat : MonoBehaviour
{
    private void Update()
    {
        this.transform.LookAt(Camera.main.transform);
    }

}
