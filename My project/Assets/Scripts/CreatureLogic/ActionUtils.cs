using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionUtils {
    public static int obstacleLayer = 6;

    public static bool IsObstacleDetected(Rigidbody2D rb)
    {

        int layerMask = 1 << obstacleLayer;
        Vector2 rayDirection = rb.velocity.normalized * UnitUtilities.TILE;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, rb.velocity, UnitUtilities.TILE, layerMask);
        //For Us to see where the ray is being cast
        Debug.DrawLine(rb.position, rb.position + rayDirection, Color.red);

        if(hit.collider != null) {

            return true;
        }

        return false;
    }
}