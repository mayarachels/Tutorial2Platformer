using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private SpriteRenderer spi;

    public float speed;

    public Text score; //public makes it show up on the unity end so you can drag game objects or put in a specific number
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;

    public GameObject winTextObject;

    public GameObject loseTextObject;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;
    public AudioSource musicSource; //what you're calling it in unity and in ur code would be the musicSource
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>(); // equal to Rigidbody2D in Unity
        score.text = scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>(); //anim is equal to the object Animator in Unity
        spi = GetComponent<SpriteRenderer>();
    }

    // FixedUpdate cuz it involves Physics
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

void Update()
{
    if (Input.GetKeyDown(KeyCode.D))

        {
            anim.SetInteger("State", 1);
            spi.flipX = false;
        }
    if (Input.GetKeyDown(KeyCode.A))

        {
            anim.SetInteger("State", 1);
            spi.flipX = true;
        }
    if (Input.GetKeyUp(KeyCode.D))

        {
            anim.SetInteger("State", 0);
        }
    if (Input.GetKeyUp(KeyCode.A))

        {
            anim.SetInteger("State", 0);
        }
    if (Input.GetKeyDown(KeyCode.W))

        {
            anim.SetInteger("State", 2);
        }
    if (Input.GetKeyUp(KeyCode.W))

        {
            anim.SetInteger("State", 0);
        }
    
}
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject); //pretty much the same thing as role the ball
           
            if (scoreValue == 4)
        {
            transform.position = new Vector2(40.5f, 0.44f);
            livesValue = 3;
            lives.text = livesValue.ToString("Lives: " + livesValue);

        }
            if (scoreValue >= 8)
           {
                winTextObject.SetActive(true);
                musicSource.clip = musicClipTwo;
                musicSource.Play();
           } 
        }
        if (collision.collider.tag == "Enemy")
     {
          Destroy(collision.collider.gameObject);
           livesValue -= 1;
            lives.text = livesValue.ToString("Lives: " + livesValue);

            if (livesValue == 0)
            {
                loseTextObject.SetActive(true);
                Destroy(gameObject);
            }
     }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}