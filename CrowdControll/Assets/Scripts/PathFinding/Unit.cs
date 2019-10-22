using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float speed = 20f;
    public float turnSpeed = 3f;
    public float turnDist = 5f;
    public float stoppingDist = 10f;

    Path path;

    private void Start()
    {
        StartCoroutine("UpdatePath");
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccesfull)
    {
        if(pathSuccesfull)
        {
            path = new Path(waypoints, transform.position, turnDist, stoppingDist);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if(Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while(true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1f;

        while(followingPath) {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while(path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if(pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }
            if(followingPath)
            {
                if(pathIndex >= path.slowDownIndex && stoppingDist > 0)
                {
                   speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDist);
                    if(speedPercent < 0.01)
                    {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
