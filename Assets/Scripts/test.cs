using System.Collections;
using System.Collections.Generic;
using Scream.UniMO.Common;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Awake()
    {
        DomainEvents.Register<OnWinnerDetermine>(OnWinnerDetermineEvent);
    }
    // to drop treasure
    private void OnWinnerDetermineEvent(OnWinnerDetermine param)
    {
        Debug.Log("test: " + param.Winner);
    }

    private void OnDestroy()
    {
        DomainEvents.UnRegister<OnWinnerDetermine>(OnWinnerDetermineEvent);
    }
}
