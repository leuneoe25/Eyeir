using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIcicle : MonoBehaviour
{
    [SerializeField] private GameObject IcicleObject;
    public float DefultTime = 2;
    float time = 0;
    void Start()
    {
        time = DefultTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
            time -= Time.deltaTime;
        else
        {
            GameObject g =  Instantiate(IcicleObject,new Vector3( gameObject.transform.position.x, gameObject.transform.position.y-2), Quaternion.identity);
            g.SetActive(true);
            Debug.Log("c");
            time = DefultTime;
        }
    }
}
