using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenCloseAnim : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(mAnimator != null)
        //{
        //    if(Input.GetKeyDown(KeyCode.T))
        //    {
        //        mAnimator.SetTrigger("TrOpen");
        //    }

        //    if(Input.GetKeyDown(KeyCode.Y))
        //    {
        //        mAnimator.SetTrigger("TrClose");
        //    }
        //}
        
    }

    public void OpenDoor()
    {
        mAnimator.SetTrigger("TrOpen");
    }

    public void CloseDoor()
    {
        mAnimator.SetTrigger("TrClose");
    }
}
