using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;

    private void Start()
    {
        //�������κ��� 2�� �� ����
        
    }

    private void Update()
    {
        //�ι�° �Ķ���Ϳ� Space.World�� �������ν� Rotation�� ���� ���� ������ ������
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<PlayerState>().Damaged();
            return;
        }
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
    }
}
