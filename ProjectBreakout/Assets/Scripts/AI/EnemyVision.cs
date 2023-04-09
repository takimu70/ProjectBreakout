using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    public GameObject player;

    public bool detected;
    public Vector3 lastSeenPos;




    private void Update()
    {
        if (player != null)
        {

            Vector3 playerTarget = (player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToTarget <= viewRadius)
                {
                    if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                    {

                        detected = true;
                        lastSeenPos = player.transform.position;
                    }
                    else
                    {
                        detected = false;
                    }
                }
                else detected = false;
            }
            else detected = false;

        }
        else detected = false;

    }
}
