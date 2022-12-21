using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTP : MonoBehaviour
{
    public GameObject tp1;
    public GameObject Map;


    private GameObject Player;

    private bool stay = false;
    [SerializeField] private Image Pade;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && stay)   //만약 목적지에서 z를 누르면
        {
            Map.SetActive(true);
        }
    }

    IEnumerator PadeIn(GameObject Player, Vector2 Position)
    {
        Pade.gameObject.SetActive(true);
        Pade.color = new Color(0, 0, 0, 0);

        while (Pade.color.a < 1)
        {
            Pade.color += new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }

        Player.transform.position = Position;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("EndScene");
    }
    

    public void TP(GameObject Player, Vector2 Position)
    {
        StartCoroutine(PadeIn(Player, Position));

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
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
