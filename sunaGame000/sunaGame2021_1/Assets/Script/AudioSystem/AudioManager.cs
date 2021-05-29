using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void Start_()
    {
        OptionData.Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
