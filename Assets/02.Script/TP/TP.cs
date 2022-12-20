using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public GameObject tp1;
    public string tp2;
    public Vector2 Position;

    private bool stay = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&& stay)   //���� ���������� z�� ������
        {
            Debug.Log("����");
            TPSystem.Instance.TP(tp2, Position);    //���������� �̵�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            stay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            stay = false;
        }
    }

}
