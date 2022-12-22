using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TPSystem : MonoBehaviour
{
    [SerializeField] private Image Pade;
    private bool ising = false;

    private void Start()
    {
    }

    IEnumerator PadeIn(GameObject Player, Vector2 Position)
    {
        ising = true;
        Pade.gameObject.SetActive(true);
        Pade.color = new Color(0, 0, 0, 0);

        while (Pade.color.a < 1)
        {
            Pade.color += new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }

        Player.transform.position = Position;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(PadeOut());
    }
    IEnumerator PadeOut()
    {
        Pade.color = new Color(0, 0, 0, 1);

        while (Pade.color.a > 0)
        {
            Pade.color -= new Color(0, 0, 0, 0.1f); ;
            yield return new WaitForSeconds(0.05f);
        }
        Pade.gameObject.SetActive(false);
        ising = false;
    }

    public void TP(GameObject Player, Vector2 Position)
    {
        if (ising)
            return;
        StartCoroutine(PadeIn(Player, Position));
       
    }

    
}
