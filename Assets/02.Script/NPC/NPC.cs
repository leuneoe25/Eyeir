using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCSystem.NPCName name = NPCSystem.NPCName.Kai;
    //public GameObject Canvers;
    public GameObject image;

    private bool isStay = false;
    private bool OnScript = false;
    private GameObject Player;
    [SerializeField] private Text Name;
    [SerializeField] private Text Script;
    private int ScriptCount;
    private bool OnTyping = false;
    private bool isMaxScript = false;
    void Start()
    {
        //Canvers.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;


        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z) && isStay && !OnScript)
        {
            image.SetActive(true);
            OnScript = true;
            ScriptCount = 0;
            Player.GetComponent<PlayerState>().isStop = true;
            Vector3 v = Camera.main.WorldToScreenPoint(new Vector3( gameObject.transform.position.x + 10, gameObject.transform.position.y + 10));
            image.transform.position = v;


            Name.text = NPCSystem.Instance.GetName(name);
            StartCoroutine(Typing());


            return;
        }

        if(OnScript)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                image.SetActive(false);
                OnScript = false;
                Player.GetComponent<PlayerState>().isStop = false;
                return;
            }
            if(!OnTyping && Input.GetKeyDown(KeyCode.Return))
            {
                
                string s=(NPCSystem.Instance.GetScript(name, ScriptCount));
                StartCoroutine(Typing());
            }
        }
        

    }
    IEnumerator Typing(string s=null)
    {
        OnTyping = true;
        Script.text = "";
        if(isMaxScript)
        {
            s = "±×·¡¼­... °ËÀº ´«ÀÌ¶û, ¿©¿Õ´ÔÀº ¾È º¸·¯°¡?";
        }
        else
        {
            s = NPCSystem.Instance.GetScript(name, ScriptCount);
        }
        if (s == "x")
        {
            image.SetActive(false);
            OnScript = false;
            Player.GetComponent<PlayerState>().isStop = false;
            OnTyping = false;
            isMaxScript = true;
            yield break;
        }
        if (isMaxScript)
        {
            if(ScriptCount!=0)
            {
                image.SetActive(false);
                OnScript = false;
                Player.GetComponent<PlayerState>().isStop = false;
                OnTyping = false;
                yield break;
            }
            
        }
        

        for (int i = 0; i < s.Length;i++)
        {
            if(!OnScript)
            {
                yield break;
            }
            if(Input.GetKeyDown(KeyCode.Return)&& Script.text != ""&&!isMaxScript)
            {
                Script.text = s;
                ScriptCount++;
                OnTyping = false;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
            Script.text += s[i];
        }
        ScriptCount++;
        OnTyping = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            isStay = true;
            Player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isStay = false;
        }
    }
}
