using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public GameObject tp1;
    public string tp2;
    public Vector2 Position;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))   //���� ���������� z�� ������
        {
            Debug.Log("����");
            TPSystem.Instance.sa(tp2, Position);    //���������� �̵�

        }
        
    }



}
