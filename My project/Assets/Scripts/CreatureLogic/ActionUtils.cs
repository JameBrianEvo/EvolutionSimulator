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

    //average speeds are between 0.16  = (UnitUtilities.Tile) and 0.16 * 5
    //the higher the speed the higher the energy cost
    //we multiply the energy by speed since the cost of about 1 tile per energy is way too high
    public static int CalculateEnergy(Vector2 startingPosition, Vector2 endPosition, float speed)
    { 
        float distance = Vector2.Distance(startingPosition, endPosition);
        float energy = distance / UnitUtilities.TILE;
        return (int)(energy * speed);
    }
}
