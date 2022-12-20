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
        if (Input.GetKeyDown(KeyCode.Z))   //만약 목적지에서 z를 누르면
        {
            Debug.Log("눌림");
            SceneManager.LoadScene(tp2);    //다음씬으로 이동
        }
    }
}
