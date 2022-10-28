using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed;
    public float impulse;
    private float move;
    public Transform feet;
    private bool isGrounded;
    public LayerMask ground;
    bool facingRight;
    public GameObject sword;
    public int damageGroundGiven;
    public GameObject deadGround;
    private bool isWalking;

    private Vector2 startPosition;

    Rigidbody2D rb;
    SpriteRenderer sr;
    AudioSource aso;
    Animator an;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();
        aso = GetComponent<AudioSource>();

        startPosition = this.transform.position;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Vertical") && isGrounded)
        {
            Jump();
        }

        move = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(feet.position, 0.3f, ground);

        Walk();

        HeroAnimations();

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
            SoundEfect.instance.sSwordSwing.Play();
        }
        if (Input.GetButtonDown("Fire1") && isGrounded == false)
        {
            JumpAttack();
            SoundEfect.instance.sSwordSwing.Play();
        }
    }

    void Walk()
    {
        if (move != 0)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
            if (!aso.isPlaying && isGrounded)
            {
               aso.Play();
            }
        }
        else
        {
            aso.Stop();
        }

        if (move > 0 && facingRight)
        {
            Flip();
        }
        if (move < 0 && !facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, impulse);
            SoundEfect.instance.sJump.Play();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void HeroAnimations()
    {
        an.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        an.SetFloat("speedY", Mathf.Abs(rb.velocity.y));
    }

    void Attack()
    {
        an.SetTrigger("attack");
    }

    void JumpAttack()
    {
        an.SetTrigger("jumpAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NoGround")
        {
            gameObject.GetComponent<VidaDoJogador>().HurtHero(damageGroundGiven);
            transform.position = new Vector2(-1.6f, 1.92f);
            deadGround.SetActive(true);
            StartCoroutine(DeadGround());
        }
    }

    IEnumerator DeadGround()
    {
        yield return new WaitForSeconds(2.0f);
        deadGround.SetActive(false);
    }

    public void RestartPosition()
    {
        transform.position = startPosition;
    }
}
