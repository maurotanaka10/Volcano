using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float attackForce;
    public int damageGiven;

    float xForce, yForce;
    public float xForceMin, xForceMax;
    public float yForceMin, yForceMax;

    private Animator an;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    private void Awake()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Draw();
        cc.enabled = false;
        StartCoroutine(MeteorTimer());
        rb.velocity = new Vector2(xForce, yForce);
        Destroy(gameObject, 4.5f);
    }

    private void Update()
    {
        
    }

    private void Draw()
    {
        xForce = Random.Range(xForceMin, xForceMax);
        yForce = Random.Range(yForceMin, yForceMax);
    }

    IEnumerator MeteorTimer()
    {
        yield return new WaitForSeconds(0.3f);
        cc.enabled = true;
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(Vector2.down * (speed * GameManager.instance.speedMultiplier));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            an.SetTrigger("fireball");
            rb.gravityScale = 0;
            rb.velocity = new Vector2(attackForce, 0.5f);
            SoundEfect.instance.sSwordHit.Play();
        }
        if(collision.gameObject.tag == "Ground")
        {
            an.SetTrigger("explosion");
            rb.velocity = new Vector2(0.0f, 0.0f);
            Destroy(gameObject, 0.3f);
            collision.gameObject.SetActive(false);
            SoundEfect.instance.sExplosion.Play();
        }
        if(collision.gameObject.tag == "Wall")
        {
            an.SetTrigger("explosion");
            rb.velocity = new Vector2(0.0f, 0.0f);
            Destroy(gameObject, 0.3f);
            collision.gameObject.GetComponent<Wall>().HurtWall(damageGiven);
            SoundEfect.instance.sExplosion.Play();

        }
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.GetComponent<VidaDoJogador>().HurtHero(damageGiven);
            Destroy(gameObject);
        }
    }
}
