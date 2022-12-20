using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] GameObject[] objectList;

    void Update()
    {
        for(int i = 0; i < objectList.Length; i++)
        {
            if (i == index) objectList[i].active = true;
            else objectList[i].active = false;
        }
    }

    public void setScene(int i)
    {
        index = i;
    }
}
