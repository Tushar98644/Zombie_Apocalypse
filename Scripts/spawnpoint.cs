using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnpoint : MonoBehaviour
{
    [Header("Zombiespawn var")]
    public GameObject zombieprefab;
    public Transform zombiespwanposition;
    private float repeatcycle = 1f;
    public GameObject dangerzone1;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            InvokeRepeating("Enemyspawner", 1f, repeatcycle);
            StartCoroutine(dangerzonetimer());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled=false;

        }
    }

    private void Enemyspawner()
    {
        Instantiate(zombieprefab, zombiespwanposition.position, zombiespwanposition.rotation);
    }

    IEnumerator dangerzonetimer()
    {
        dangerzone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerzone1.SetActive(false);
    }
}
