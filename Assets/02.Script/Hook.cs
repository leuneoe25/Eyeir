using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Vector3 dir = Vector3.zero;
    void Update()
    {
        if(dir!=Vector3.zero)
        {
            transform.position += dir;
        }
    }
    public void SetDir(Vector3 v)
    {
        dir = v;
        Debug.Log(dir);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            Debug.Log("Coll");
            dir = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
