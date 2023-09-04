using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //About: Static of player
    public float moveSpeed = 2f;
    public float jumpPower = 3f;
    public int maxHp;
    public GameObject eBtn;

    //About: For open tresure
    public int openAttack = 4;
    public int openAttackCount;

    //About: OneJump
    public bool isJump = false;
    
    //About: Rigidbody
    float gravity = -20f;
    float yVelocity = 0;

    //About: Class Zip
    public GameManager gameManager;
    public Zombie enemy;
    private CharacterController cc;
    private Animator anim;

    //About: Event//

    //For All_Scene
    private bool isAttack = false;
    private bool canGetTresure = false;
    private bool groundGoal = false;

    //For Level_1
    private bool groundWater = false;
    
    //For Level_2 
    private bool groundTree = false;
    public bool canCut_Tree = false;

    //For Level_3
    private bool groundEnemy = false;


    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        maxHp = gameManager.life;
        openAttackCount = openAttack;
        // print(maxHp);
        Invoke("Start_Delay", 3f);

    }
    void Update()
    {
        Move();
        Jump();
        Attack();
        Die();
        Get();
    }
 

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, y).normalized;

        //Look player dir
        if (dir != Vector3.zero)
        {
            transform.LookAt(transform.position + dir);
            anim.SetTrigger("ToRun_Idle");
        }
        else
        {
            anim.SetTrigger("ToIdle_Run");
        }
        
        //Input the gravity in dir.y 
        yVelocity += gravity * Time.deltaTime;
        dir.y =  yVelocity;

        if(isAttack)
        {
            cc.Move(Vector3.zero);
        }
        else
        {
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        //For cant double 'Jump'
        if (isJump && cc.collisionFlags == CollisionFlags.Below)
        {
            isJump = false;
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            yVelocity = jumpPower;
        }
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("ToAttack_Idle");
            isAttack = true;

            // If Player is in the 'Tresure' ,Check
            if(groundGoal)
            {
                if(openAttack >=0)
                {
                    openAttack -= 1;
                    print(openAttack);
                }

                else
                {
                    openAttack -= 0;
                    gameManager.canOpen = true;
                    canGetTresure = true;
                }
            }
            //If player is in the 'Tree', Check
            else if(groundTree)
            {
                canCut_Tree = true;

            }

            if(groundEnemy)
            {
                enemy.Hp -= 1;

            }

        }

        else
        {            
            anim.SetTrigger("ToIdle_Attack");
            isAttack = false;
        }

    }

    void Get()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(groundWater)
            {
                gameManager.haveWater = true;
                gameManager.canGet = false;
                eBtn.SetActive(false);
            }

        }
        
        
    }

    void Die()
    {
        //If player drop the 'Hole' or 'Water'
        if(transform.position.y < -13f)
        {
            Destroy(gameObject);
            gameManager.die = true;
        }

        if(gameManager.isDie)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //Level_1 Scene - 'Trap'
        if(other.gameObject.CompareTag("Trap"))
        {
            gameManager.life -= 1;
            print(gameManager.life);
            
        }

        //Level_1, Level_2 Scene - life + 1 'Star' , 'Angel'
        else if(other.gameObject.CompareTag("Heal"))
        {
            Destroy(other.gameObject);
            // print(gameManager.life);

            //if GamaManager.cs -> maxHp > life 
            if(gameManager.life <= maxHp )
            {
                gameManager.life += 1;
            }

            //if GameManager.cs -> maxHp < life (full)
            else
            {
                gameManager.life += 0;
            }
            gameManager.last_count -= 1;
        }

        //Level_1 Scene - life -1 , can Destroy(fire)
        else if(other.gameObject.CompareTag("Fire"))
        {
            // print("Fire");
            if(gameManager.haveWater)
            {
                Destroy(other.gameObject);
                gameManager.life -= 0;
            }

            else
            {
                gameManager.life -= 1;
            }

        }

        //Level_2 Scene - if player falling the water, player Die * Tag(Water) = Level_1, Tag(DieWater) = Level_2 differnce *
        else if(other.gameObject.CompareTag("DieWater"))
        {
            gameManager.isDie = true;
            gameManager.die = true;
        }

        //Level_3 
        else if(other.gameObject.CompareTag("DropRock"))
        {
            gameManager.life -= 1;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //For change haveWater = true
        if(other.gameObject.CompareTag("Water"))
        {
            print("Water");
            groundWater = true;
            eBtn.SetActive(true);

            if(gameManager.haveWater)
            {
                Destroy(other.gameObject);
                eBtn.SetActive(false);
            }
            else if(gameManager.getWater)
            {
                return;
            }

        }
        //For change Tresure
        else if(other.gameObject.CompareTag("Tresure"))
        {
            print("Tresure");
            groundGoal = true;

            //if win stop player
            if(canGetTresure)
            {
                moveSpeed = 0;
               
            }

        }

        //For level_2 - Cut trees
        if(other.gameObject.CompareTag("Tree"))
        {
            groundTree = true;
            print("tree");

            if(canCut_Tree)
            {
                Destroy(other.gameObject);
                canCut_Tree = false;
            }
        }

        

    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            groundWater = false;
        }

        if(other.gameObject.CompareTag("Tresure"))
        {
            groundGoal = false;
            openAttack = openAttackCount;
        }

        if(other.gameObject.CompareTag("Tree"))
        {
            groundTree = false;
            canCut_Tree = false;
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        //Player is attacked life -1 
        if(other.gameObject.CompareTag("Enemy"))
        {
            gameManager.life -= 1;
            groundEnemy = true;
            if(gameManager.life == 0 )
            {
                gameManager.life -= 0;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            groundEnemy = false;
        }
    }

}