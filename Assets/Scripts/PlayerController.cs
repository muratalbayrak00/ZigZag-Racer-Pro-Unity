using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 1;
    public AudioSource crashAudio;
    public AudioSource ses2;
    Rigidbody rb;
    bool movingLeft = true;
    private bool playing;
    bool firstInput = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }
    int a = 5;
    
    // Update is called once per frame
    void Update()
    {
        /* if (!GameManager.instance.gameStarted)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 playing = true;
             }
         }

         else if (playing)
         {
             CheckInput();
             Move();
         }*/
        
        
        if (GameManager.instance.gameStarted)
        {
          //  ses2.Pause();
            
            Move();
            CheckInput();
        }
        

        if (transform.position.y <= -2)
        {
            if(a == 5)
            {
                playing = false;
            
                GameManager.instance.GameOver();
                if (Random.Range(0,3)==0)
                {
                    AddManager.instance.ShowInterstitial();
                }
                
                a++;
            }
            
            
        }
        
        
    }

    void SpeedUP()
    {
        if (GameManager.instance.score % 20 == 0)
        {

            if (GameManager.instance.score != 0)
            {
                playerSpeed++;
                Debug.Log(playerSpeed);
                //   GameManager.instance.score++;
            }


        }
    }
    private void FixedUpdate()
    {
        if (playing)
        {
            //Move();
        }
        
    }

    void CheckInput()
    {
        if (firstInput) {

            firstInput = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SpeedUP();
            GameManager.instance.score++;
            GameManager.instance.scoreText.text = GameManager.instance.score.ToString();
            ChangeDirection();
        }
    }

    void Move()
    {
        //rb.velocity = transform.forward * playerSpeed;


        transform.position += -transform.forward * playerSpeed * Time.deltaTime;
    }

    void ChangeDirection()
    {
        if (movingLeft)
        {
            movingLeft = false;

            transform.rotation = Quaternion.Euler(0, -90f, 0);
          
            

        }
        else
        {
            movingLeft = true;
            transform.rotation = Quaternion.Euler(0, 180f, 0);
           
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "zemin")
        {
            StartCoroutine(RemoveFloor(collision.gameObject));
           // Debug.Log("hihih");

        }

        //started = false;
        //rb.isKinematic = false;
        //rb.velocity = Vector3.down * 20f;

    }
    IEnumerator RemoveFloor(GameObject sil)
    {
        yield return new WaitForSeconds(3f);
        Destroy(sil);
    }

}
