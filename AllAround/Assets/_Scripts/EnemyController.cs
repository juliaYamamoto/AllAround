using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject fromPositionGameObject;
    public GameObject toPositionGameObject;
    public GameObject enemyGameObject;

    public float speed;

    public Vector3 targetPosition;
    private bool setupIsReady;

    private void Start()
    {
        EnemySetup();
    }

    private void EnemySetup()
    {
        enemyGameObject.transform.position = fromPositionGameObject.transform.position;
        targetPosition = toPositionGameObject.transform.position;

        setupIsReady = true;
    }

    private void Update()
    {
        if (setupIsReady)
        {
            enemyGameObject.transform.position = Vector3.MoveTowards(enemyGameObject.transform.position, targetPosition, speed * Time.deltaTime);
            if (enemyGameObject.transform.position == targetPosition)
            {
                if (targetPosition == toPositionGameObject.transform.position)
                {
                    targetPosition = fromPositionGameObject.transform.position;
                } else
                {
                    targetPosition = toPositionGameObject.transform.position;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(fromPositionGameObject.transform.position, fromPositionGameObject.transform.localScale);


        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(toPositionGameObject.transform.position, fromPositionGameObject.transform.localScale);
    }
}
