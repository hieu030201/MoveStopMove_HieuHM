using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] public Transform minPoint, maxPoint;
    public int enemyInMap;
    public int numberEnemyActive;

    public Vector3 RandomPoint()
    {
        Vector3 randPoint = Random.Range(minPoint.position.x, maxPoint.position.x) * Vector3.right + Random.Range(minPoint.position.z, maxPoint.position.z) * Vector3.forward;

        NavMeshHit hit;

        NavMesh.SamplePosition(randPoint, out hit, float.PositiveInfinity, 1);

        return hit.position;
    }
}
