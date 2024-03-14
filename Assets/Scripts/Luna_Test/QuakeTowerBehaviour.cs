using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class QuakeTowerBehaviour : MonoBehaviour
{
    private TurretStats stats;
    public List<GameObject> enemyList;

    //public List<GameObject> targetList;
    public List<IDamageable> targetInterface;

    public bool activated = false;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<DefenceStats>().defenceStats;
        GetComponent<CapsuleCollider>().radius = stats.defenceRange;
        enemyList = new List<GameObject>();
    }

    void Update()
    {
        if (enemyList.Count > 0)
        {
            // If the first item of the list is null remove it (Cleans up killed enemies)
            if (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
                //targetList.RemoveAt(0);
            }

            /*if (*//*targetList == null && *//*enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    targetInterface.Add(targetList[i].GetComponent<IDamageable>());
                }
            }*/

            /*if (activated)
            {
                QuakeAttack((IDamageable)targetInterface);
            }*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If an enemy enters the range of the turret at it to a list
        if (other.gameObject.tag == "Enemy")
        {
            // Enemy Found
            enemyList.Add(other.gameObject);
            //targetInterface.Add(other.gameObject.GetComponent<IDamageable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If an enemy exits the range of the turret remove it from the list
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
            //targetInterface.Remove(other.gameObject.GetComponent<IDamageable>());
        }
    }

    /*public void QuakeAttack(IDamageable targetList)
    {
        foreach(GameObject target in enemyList)
        {
            targetList.DealDamage(stats.defenceDamage);
        }
    }*/
}