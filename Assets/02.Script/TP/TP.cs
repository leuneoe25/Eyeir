using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public GameObject tp1;
    public GameObject Map;


    private GameObject Player;

    private bool stay = false;
    [SerializeField] private TPSystem system;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&& stay)   //���� ���������� z�� ������
        {
            Map.SetActive(true);
            system.TP(Player, tp1.transform.position);    //���������� �̵�
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            stay = true;
            Player = collision.gameObject;
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
