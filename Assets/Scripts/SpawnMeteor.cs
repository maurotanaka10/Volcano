using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public float spawnTime;
    public Transform meteorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTime * GameManager.instance.spawnMultiplier);
        Instantiate(meteorPrefab);
        SoundEfect.instance.sMeteorSpawn.Play();
        StartCoroutine(Spawn());
    }
}
