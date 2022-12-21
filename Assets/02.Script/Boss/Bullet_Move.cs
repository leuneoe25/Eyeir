using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;

    private void Start()
    {
        //생성으로부터 2초 후 삭제
        
    }

    private void Update()
    {
        //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
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
