using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof( NavMeshSourceTag))]
public class MeshPointView : MonoBehaviour
{
    public enum MeshType
    {
        cube,
        cylinder
    }

    public bool isAutoPoint;
    public List<Transform> pointList;

}
