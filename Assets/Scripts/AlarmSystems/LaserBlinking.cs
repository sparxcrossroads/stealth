using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

    public float onTime;
    public float offTime;


    private float timer; //gap-time

    void Update()
    {
        timer += Time.deltaTime;

        if (this.renderer.enabled && timer >= onTime)
            SwitchBeam();

        if (!this.renderer.enabled && timer >= offTime)
            SwitchBeam();
    }

    void SwitchBeam()
    {
        timer = 0f;

        this.renderer.enabled = !this.renderer.enabled;
        this.light.enabled = !this.light.enabled;
    }
}
