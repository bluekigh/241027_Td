using UnityEngine;

public class BuildAble : MonoBehaviour
{
    int buildingLV = 0;

    public int BuildLV()
    {
        return buildingLV;
    }

    public void build()
    {
        buildingLV++;
    }

}
