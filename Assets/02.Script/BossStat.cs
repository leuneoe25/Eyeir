using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStat : MonoBehaviour
{
    [SerializeField] private float BossHp;
    [SerializeField] private Boss boss;
    [SerializeField] private string name;
    [Header("UI")]
    [SerializeField] private Text BossName;
    [SerializeField] private Image BossHpbar;
    [SerializeField] private GameObject BossCanvers;
    public CinemachineImpulseSource impulseSource;
    private float NowHp;
    public bool BossPattern = false;
    private bool isDie = false;

    [SerializeField] private GameObject TP;
    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetUI()
    {
        BossHpbar.fillAmount = (NowHp / BossHp);
    }
    void Attcked(int Damage)
    {
        impulseSource.GenerateImpulse(0.7f);
        NowHp -= Damage;
        if(NowHp <=0)
        {
            Debug.Log("Die");
            BossPattern = false;

            StopAllCoroutines();
            StartCoroutine(Die());
        }
        SetUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            BossPattern = true;
            BossCanvers.SetActive(true);
            NowHp = BossHp;
            BossName.text = name;
            TP.SetActive(false);
            Player = collision.gameObject;
        }
        if(collision.transform.CompareTag("Skill"))
        {
            Debug.Log(int.Parse(collision.transform.name));
            Attcked(SkillCommand.Instance.Damage(int.Parse(collision.transform.name)) + ItemManager.Instance.GetAttackDamage());
        }
    }
    IEnumerator Die()
    {
        BossCanvers.SetActive(false);
        TP.SetActive(true);
        TP.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        boss.Clear();
        float Speed = 20f;
        float y = gameObject.transform.position.y;
        while ((y - 100) < gameObject.transform.position.y)
        {
            TP.GetComponent<SpriteRenderer>().color+= new Color(0, 0, 0, 0.1f);
            gameObject.transform.position += Vector3.down * Speed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        
    }
}
