using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class EnemyUtilities 
{
    

    public static bool CanSeePlayer(Transform line1Start,Transform line1Stop,Transform line2Start,Transform line2Stop,Transform line3Start,
        Transform line3Stop)
    {
        bool Line1 = Physics.Linecast(line1Start.position, line1Stop.position,1<<LayerMask.NameToLayer("Player"));
        bool Line2 = Physics.Linecast(line2Start.position, line2Stop.position, 1 << LayerMask.NameToLayer("Player"));
        bool Line3 = Physics.Linecast(line3Start.position, line3Stop.position, 1 << LayerMask.NameToLayer("Player"));

        Debug.DrawLine(line1Start.position, line1Stop.position, Color.red);
        Debug.DrawLine(line2Start.position, line2Stop.position, Color.red);
        Debug.DrawLine(line3Start.position, line3Stop.position, Color.red);

        if (Line1 == true || Line2 == true || Line3 == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
