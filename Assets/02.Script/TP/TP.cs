using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TP : MonoBehaviour
{
    public GameObject tp1;
    public string tp2;

    //public void ChangeScene()
    //{

    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))   //���� ���������� z�� ������
        {
            Debug.Log("����");
            SceneManager.LoadScene(tp2);    //���������� �̵�
        }
    }
}
