using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {


    private GameObject player;
    private LastPlayerSighting lastPlayerSlighting;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSlighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

    }

    void OnTriggerStay(Collider other)
    {
        if (renderer.enabled)
            if (other.gameObject == player)
                lastPlayerSlighting.position = other.transform.position;
    }
}
