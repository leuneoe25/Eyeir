using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFallSpawn : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player") //�ݸ��� �±װ� �÷��̾�� ���ٸ�
        {
            //ĳ���͸� ����
            Debug.Log("ĳ���� ����");
            collision.transform.position = gameObject.transform.position;

        }
    }
}

//�ƾƾƾƾƾƾƾƾƾƾƾƾƾƾƾƾƾƾ�...........
