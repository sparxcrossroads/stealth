using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour {

    public float timeToDoorsClose = 2f;     //close door_inner
    public float timeToLiftStart = 3f;      //lift start
    public float timeToEndLevel = 6f;       //endscene
    public float liftSpeed = 3f;

    private GameObject player;
    private Animator playerAnim;
    private HashIDs hash;
    private CameraMovement camMovement;
    private SceneFadeInOut sceneFadeInout;
    private LiftDoorsTracking lifeDoorsTracking;
    private bool playerInLift;
    private float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        sceneFadeInout = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        lifeDoorsTracking = GetComponent<LiftDoorsTracking>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInLift = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInLift = false;
            timer = 0;
        }
    }

    void Update()
    {
        if (playerInLift)
            LiftActivation();
        if (timer < timeToDoorsClose)
            lifeDoorsTracking.DoorFollowing();
        else
            lifeDoorsTracking.CloseDoors();
    }

    void LiftActivation()
    {
        timer += Time.deltaTime;

        if(timer>=timeToDoorsClose)
        {
            playerAnim.SetFloat(hash.speedFloat, 0f);
            camMovement.enabled = false;
            //改变父级
            player.transform.parent = transform;

            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);

            if (!audio.isPlaying)
                audio.Play();

            if (timer >= timeToEndLevel)
                sceneFadeInout.EndScene();

        }
    }
}
