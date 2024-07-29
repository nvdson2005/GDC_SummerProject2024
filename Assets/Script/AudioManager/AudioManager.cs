using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        foreach(Sound sound in sounds){
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }
    void Start(){
        StartCoroutine(Playbgsound());
    }
    IEnumerator Playbgsound(){
        yield return new WaitForSeconds(1f);
        Play("BackgroundSound");
    }
    // Update is called once per frame
    public void Play(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }
    public bool IsPlaying(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        return sound.source.isPlaying;
    }
    public void Stop(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }
}
