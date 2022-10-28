using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaDoJogador : MonoBehaviour
{
    public int maxLifeHero;
    public int currentLifeHero;
    public int damagetaken;

    public GameObject heart1, heart2, heart3;

    Rigidbody2D rb;
    Animator an;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    private void Start()
    {
        currentLifeHero = maxLifeHero;
    }

    private void Update()
    {
        if(currentLifeHero == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if(currentLifeHero == 2)
        {
            heart1.SetActive(false);
        }
        if(currentLifeHero == 1)
        {
            heart2.SetActive(false);
        }
        if(currentLifeHero <= 0)
        {
            heart3.SetActive(false);
        }
    }

    public void HurtHero(int damageTaken)
    {
        currentLifeHero -= damageTaken;

        if(currentLifeHero <= 0)
        {
            an.SetTrigger("heroDie");
            StartCoroutine(Dead());
            transform.position = new Vector2(0.0f, 0.0f);
            GameManager.instance.GameOver();
            SoundEfect.instance.sDie.Play();
        }

        an.SetTrigger("heroHurt");
        SoundEfect.instance.sHitHero.Play();
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
