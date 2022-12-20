using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFallSpawn : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player") //콜리션 태그가 플레이어와 같다면
        {
            //캐릭터를 스폰
            Debug.Log("캐릭터 스폰");
            collision.transform.position = gameObject.transform.position;

        }
    }
}

//아아아아아아아아아아아아아아아아아아아...........
