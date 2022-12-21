using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player") && !ItemManager.Instance.DTrap)
        {
            collision.gameObject.GetComponent<PlayerState>().isIceTile = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !ItemManager.Instance.DTrap)
        {
            collision.gameObject.GetComponent<PlayerState>().isIceTile = false;
        }
    }
}
