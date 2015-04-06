using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour {

    private GameObject player;
    private LastPlayerSighting lastPlayerSlighting;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSlighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();


    }
	
	void OnTriggerStay(Collider other)
    {
        if(other.gameObject==player)
        {
            //ray 的投射方向
            Vector3 relPlayerPos = player.transform.position - this.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, relPlayerPos, out hit))

                if (hit.collider.gameObject == player)
                    lastPlayerSlighting.position = player.transform.position;
        }
    }
}
