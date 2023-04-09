using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiDoor : MonoBehaviour
{
    
    public void OpenDoor()
    {
        this.gameObject.GetComponent<Animator>().Play("Open");
    }

    public void CloseDoor()
    {
        this.gameObject.GetComponent<Animator>().Play("Close");
    }

}
