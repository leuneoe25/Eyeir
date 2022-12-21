using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : MonoBehaviour
{
    [SerializeField] private int BossHp;
    private int NowHp;
    void Start()
    {
        NowHp = BossHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attcked(int Damage)
    {
        NowHp -= Damage;
        Debug.Log(Damage);
        if(NowHp <=0)
        {
            Debug.Log("Die");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Skill"))
        {
            Debug.Log(int.Parse(collision.transform.name));
            Attcked(SkillCommand.Instance.Damage(int.Parse(collision.transform.name)) + ItemManager.Instance.GetAttackDamage());
        }
    }
}
