using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStat : MonoBehaviour
{
    [SerializeField] private float BossHp;
    [SerializeField] private Boss boss;
    [Header("UI")]
    [SerializeField] private Text BossName;
    [SerializeField] private Image BossHpbar;
    [SerializeField] private GameObject BossCanvers;

    private float NowHp;
    public bool BossPattern = false;

    public GameObject Player;
    void Start()
    {
        NowHp = BossHp;
        BossName.text = "´«ÀÇ ¿©¿Õ";
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
        boss.Clear();
        float Speed = 20f;
        float y = gameObject.transform.position.y;
        while ((y - 100) < gameObject.transform.position.y)
        {
            gameObject.transform.position += Vector3.down * Speed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        
    }
}
