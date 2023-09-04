using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed = 3f;
    public GameObject player;
    public float Hp = 2f;
    private float initialRotation; // 초기 회전 값을 저장할 변수
    private float startPos;


    Animator anim;

    void Start()
    {
        startPos = moveSpeed;
        anim = GetComponentInChildren<Animator>();
        initialRotation = transform.rotation.x; // 초기 회전 값을 저장
        moveSpeed = 0;
        Invoke("Delay_start",5f);
    }
    
    void Update()
    {
        Move();
    }
    void Delay_start()
    {
        moveSpeed = startPos;

    }

    void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(-direction * moveSpeed * Time.deltaTime);

        anim.SetTrigger("ToRun_Idle");

        transform.LookAt(player.transform);

        Quaternion targetRotation = Quaternion.Euler(initialRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = targetRotation;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("ToAttack_Run");
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("ToRun_Attack");
        }
    }
}
