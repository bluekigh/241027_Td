using UnityEngine;

public class BuildAble : MonoBehaviour
{
    int buildingLV = 0;
    GameObject builded;

    public int BuildLV()
    {
        return buildingLV;
    }

    public void build(GameObject build)
    {
        if (builded == null)
        {
            builded = build;
        }
        else
        {
            Destroy(builded);
            builded = build;
        }
        buildingLV++;

    }

    public void build()
    {

    }

}
