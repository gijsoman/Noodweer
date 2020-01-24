using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnemySoundBoard : MonoBehaviour
{
    private StudioEventEmitter[] eventEmitters;

    private void Start()
    {
        eventEmitters = GetComponents<StudioEventEmitter>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.LeftShift)){
            LookupCorrectEmitter("event:/Voice_lines/Ik_Wil_Niet_Meer");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)){
            LookupCorrectEmitter("event:/Voice_lines/Schiet");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.LeftShift)){
            LookupCorrectEmitter("event:/Voice_lines/Ik_Wil_Geen_Hulp");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            LookupCorrectEmitter("event:/Voice_lines/Schiet_Dan");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.LeftShift)){
            LookupCorrectEmitter("event:/Voice_lines/Frustratie");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){ 
            LookupCorrectEmitter("event:/Voice_lines/Ja_Precies");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && Input.GetKey(KeyCode.LeftShift)){
            LookupCorrectEmitter("event:/Voice_lines/Godverdomme");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            LookupCorrectEmitter("event:/Voice_lines/Schiet_Godverdomme");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKey(KeyCode.LeftShift)){
            LookupCorrectEmitter("event:/Voice_lines/Werk_Mee");}
        else if (Input.GetKeyDown(KeyCode.Alpha5)){
            LookupCorrectEmitter("event:/Voice_lines/Nee_Schiet");
        }

        if (Input.GetKeyDown(KeyCode.Q))
            LookupCorrectEmitter("event:/Voice_lines/Je_Durft_Niet");

        if (Input.GetKeyDown(KeyCode.E))
            LookupCorrectEmitter("event:/Voice_lines/Kom_Dan");

        if (Input.GetKeyDown(KeyCode.R))
            LookupCorrectEmitter("event:/Voice_lines/Mik_Hier");
    }

    private void LookupCorrectEmitter(string _event)
    {
        for (int i = 0; i < eventEmitters.Length; i++)
        {
            if (eventEmitters[i].Event == _event)
            {
                StopAllEmitters();
                eventEmitters[i].Play();
            }
        }
    }

    private void StopAllEmitters()
    {
        for (int i = 0; i < eventEmitters.Length; i++)
        {
            eventEmitters[i].Stop();
        }
    }
}
