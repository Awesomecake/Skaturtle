using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    private SkaturtleLogic logic;

    [SerializeField] GameObject death;
    private GameObject deathObject;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        logic = GameObject.FindGameObjectWithTag("Player").GetComponent<SkaturtleLogic>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            deathObject = Instantiate(death, player.transform.position, Quaternion.identity);
            player.SetActive(false);
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.5f);
        logic.Respawn();
        player.SetActive(true);
        Destroy(deathObject);
    }
}
