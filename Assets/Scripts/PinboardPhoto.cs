using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinboardPhoto : MonoBehaviour
{
    public bool colliding = true;

    private void OnCollisionEnter(Collision collision)
    {
        print("Collided!");
        colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        print("Stopped collided!");
        colliding = false;
    }
}
