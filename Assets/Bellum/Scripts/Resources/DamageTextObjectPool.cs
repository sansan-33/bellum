using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// A very simple object pooling class
public class DamageTextObjectPool : MonoBehaviour
{
    // the prefab that this object pool returns instances of
    public GameObject prefab;
    // collection of currently inactive instances of the prefab
    private Stack<GameObject> inactiveInstances = new Stack<GameObject>();
    public int inactiveInstancesCount = 0;

    // Returns an instance of the prefab
    public GameObject GetObject()
    {
        GameObject spawnedGameObject;

        // if there is an inactive instance of the prefab ready to return, return that
        if (inactiveInstances.Count > 0)
        {
            // remove the instance from teh collection of inactive instances
            spawnedGameObject = inactiveInstances.Pop();
        }
        // otherwise, create a new instance
        else
        {
            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);
            // add the PooledObject component to the prefab so we know it came from this pool
            DamageTextPooledObject pooledObject = spawnedGameObject.AddComponent<DamageTextPooledObject>();
            pooledObject.pool = this;
            inactiveInstancesCount = inactiveInstances.Count;
        }

        // put the instance in the root of the scene and enable it
        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);

        // return a reference to the instance
        return spawnedGameObject;
    }

    // Return an instance of the prefab to the pool
    public void ReturnObject(GameObject toReturn)
    {
        Destroy(toReturn);
        /*
        DamageTextPooledObject pooledObject = toReturn.GetComponent<DamageTextPooledObject>();
        // if the instance came from this pool, return it to the pool

        if (pooledObject != null && pooledObject.pool == this)
        {
            // make the instance a child of this and disable it
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);

            // add the instance to the collection of inactive instances
            //inactiveInstances.Push(toReturn);
            Debug.Log($"New Pool==>{name} , inactiveInstances pool count : b4 {inactiveInstancesCount}  after {inactiveInstances.Count}");

            //inactiveInstancesCount = inactiveInstances.Count;
            //Debug.Log($"Pool==>{name} , {toReturn.name}  make the instance a child of this and disable it");
        }
        // otherwise, just destroy it
        else
        {
            Debug.Log($"{toReturn.name} was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(toReturn);
        }
       */
    }
    private void Update()
    {
        //StartCoroutine(garbadgeCollection());
    }
    IEnumerator garbadgeCollection()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log($"garbadgeCollection {name} ");

        foreach (Transform child in transform)
        {
            Debug.Log($"{child.name} pushed ");
            //inactiveInstances.Push(child.gameObject);
        }
    }
}

// a component that simply identifies the pool that a GameObject came from
public class DamageTextPooledObject : MonoBehaviour
{
    public DamageTextObjectPool pool;
}