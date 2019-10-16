using System;
using UnityEngine;
using UnityEngine.AI;

public class _Path : MonoBehaviour
{
    public Transform[] waypoints;
    //private LineRenderer lineRender;

    private void Start()
    {
      //  lineRender = GetComponent<LineRenderer>();
        waypoints = Array.FindAll(GetComponentsInChildren<Transform>(), child => child != transform);
    }

    #region
    private void OnDrawGizmo()   //Just for Debugging and in the Editor (Not Finished)
    {
        var nav = GetComponent<NavMeshAgent>();
        nav.destination = waypoints[1].position;
        if (nav == null || nav.path == null)
            return;

        var line = GetComponent<LineRenderer>();
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.blue};
            line.startWidth = .5f;
            line.startColor = Color.blue;
            line.endColor = Color.red;
        }
        
        var path = nav.path;

        line.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }
    }
    #endregion
}
