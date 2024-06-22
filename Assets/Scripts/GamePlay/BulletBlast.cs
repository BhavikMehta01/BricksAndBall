using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBlast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerGame.instance.allSounds[0].Play();
        Destroy(gameObject,0.2f);
    }

}
