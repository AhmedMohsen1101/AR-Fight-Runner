using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ResetARSession : MonoBehaviour
{
    private ARSession aRSession;
    public void ResetSession()
    {
        if (aRSession is null)
            aRSession = GetComponent<ARSession>();

        aRSession.Reset();
    }
}
