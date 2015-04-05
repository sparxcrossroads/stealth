﻿using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {

    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    public Vector3 resetposition = new Vector3(1000f, 1000f, 1000f);
    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float musicFadeSpeed = 1f;

    private AlarmLight alarm;
    private Light mainLight;
    private AudioSource panicAudio;
    private AudioSource[] sirens;

    void Awake()
    {
        alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).light;

        panicAudio = transform.Find("secondaryMusic").audio;

        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
        sirens = new AudioSource[sirenGameObjects.Length];
        

        for(int i=0;i<sirens.Length;i++)
        {
            sirens[i] = sirenGameObjects[i].audio;
        }
    }
	// Update is called once per frame
	void Update () {
        SwitchAlarms();
        MusicFading();
	}
    void SwitchAlarms()
    {
        alarm.alarmOn = (position != resetposition);

        float newIntensity;

        if (position != resetposition)
            newIntensity = lightLowIntensity;
        else
            newIntensity = lightHighIntensity;

        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for(int i=0;i<sirens.Length;i++)
        {
            if (position != resetposition && !sirens[i].isPlaying)
                sirens[i].Play();
        }
    }

    void MusicFading()
    {
        if(position!=resetposition)
        {
            audio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);

            panicAudio.volume = Mathf.Lerp(audio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);

        }
        else 
        {
            audio.volume = Mathf.Lerp(audio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);

            panicAudio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}
