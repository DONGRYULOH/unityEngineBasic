using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{    
    public AudioClip _audio1;
    public AudioClip _audio2;

    private void OnTriggerEnter(Collider other)
    {
        /*AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(_audio1);
        audio.PlayOneShot(_audio2);

        float lifeTime = Mathf.Max(_audio1.length, _audio2.length);
        GameObject.Destroy(gameObject, lifeTime);*/

        Managers.Sound.Play("UnityChan/univ0001", Defines.Sound.Effect);
        Managers.Sound.Play(_audio2, Defines.Sound.Effect);        
    }
}
