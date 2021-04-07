using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Lightling : MonoBehaviour
{
    [SerializeField] private GameObject LightlingPrefab;
    [SerializeField] private LayerMask layerMask = new LayerMask();
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    public int attackRange = 100;
    public int maxAttackrange = 1500;
    public int minAttackRange = 1000;
    public float SPCost = 10;

    private Button SPButton;

    private bool IsSuperAttack = false;

    private SpCost spCost;
    private GameObject hitCollider;
    private Transform MyKing;
    private GameObject searchPoint;
    private RTSPlayer player;
    private TacticalBehavior TB;
    private List<GameObject> targetList = new List<GameObject>();
    private List<GameObject> startPointList = new List<GameObject>();
    private List<GameObject> lightlingList = new List<GameObject>();
    private bool SpawnedButton;
    private float lightlingTimer;
    // Start is called before the first frame update
    void Start()
    {
        
         startPoint = GameObject.FindGameObjectWithTag("LightlingStartPoint");
        endPoint = GameObject.FindGameObjectWithTag("LightlingEndPoint");
        SpawnedButton = FindObjectOfType<SpButton>().InstantiateSpButton(SpecialAttackDict.SpecialAttackType.Lightling, GetComponent<Unit>());
        if (SpawnedButton) { SPButton = FindObjectOfType<SpButton>().GetButton(GetComponent<Unit>().SpBtnTicket).GetComponent<Button>(); }
        if (SPButton == null) { return; }
        SPButton.onClick.RemoveAllListeners();
        SPButton.onClick.AddListener(OnPointerDown);
        player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        spCost = FindObjectOfType<SpCost>();
        //searchPoint = attackPoint.transform;
        minAttackRange = (int)(transform.localScale.x * attackRange / 2);
        TB = GameObject.FindGameObjectWithTag("TacticalSystem").GetComponent<TacticalBehavior>();
    }

   /* public void OnpointerDown()
    {
        //Debug.Log($"FindAttackTargetInDistance");

        //if(SPAmount < SPCost) {return;}
        spCost.SPAmount -= (int)SPCost;

        var distance = float.MaxValue;
        var localDistance = 0f;
        distanceList.Clear();
        targetList.Clear();
        MyKing = GameObject.FindGameObjectWithTag("King0").transform;
        //bool findedTarget = false;
        //Search target in a distance
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale * attackRange, Quaternion.identity, layerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            distance = float.MaxValue;
            hitCollider = hitColliders[i++].transform.gameObject;
            // check If the target is cloestest to king && it is not in the same team && check if it already finded the target
            if ((localDistance = (hitCollider.transform.position - transform.position).sqrMagnitude) < distance && !targetList.Contains(hitCollider.transform))
            {
                int id = ((RTSNetworkManager)NetworkManager.singleton).Players.Count == 1 ? 1 : player.GetPlayerID() == 0 ? 1 : 0;
                if (hitCollider.CompareTag("Player" + id) || hitCollider.CompareTag("King" + id))
                {
                    if (localDistance > minAttackRange)
                    {
                        localDistance = distance;
                        startPoint.transform.position = MyKing.position;
                        endPoint.transform.position = hitCollider.transform.position;
                        hitCollider.GetComponent<Health>().DealDamage(100);
                        //Debug.Log($"change lightling point{startPoint.transform.position},  {endPoint.transform.position}");
                    }
                }
            }
        }


    }*/
    public void OnPointerDown()
    {
        targetList.Clear();
        startPointList.Clear();
        lightlingList.Clear();
        endPoints.Clear();
        startPoints.Clear();
        //Debug.Log($"FindAttackTargetInDistance");
        //if (attackPoint == null) { return; }
        //if(SPAmount < SPCost) {return;}
        spCost.SPAmount -= (int)SPCost;
        searchPoint = GameObject.FindGameObjectWithTag("King" + player.GetPlayerID());
        GameObject closestTarget = null;
        bool haveTarget = true;
        var distance = float.MaxValue;
        var localDistance = 0f;
        startPointList.Clear();
        targetList.Clear();
        
        while (haveTarget == true)
        {
            startPointList.Add(searchPoint);
            bool findedTarget = false;
            //Search target in a distance
            Collider[] hitColliders = Physics.OverlapBox(searchPoint.transform.position, transform.localScale * attackRange, Quaternion.identity, layerMask);
            int i = 0;
            while (i < hitColliders.Length)
            {
                distance = float.MaxValue;
                hitCollider = hitColliders[i++].transform.gameObject;
                // check If the target is cloestest to king && it is not in the same team && check if it already finded the target
                if ((localDistance = (hitCollider.transform.position - transform.position).sqrMagnitude) < distance && !targetList.Contains(hitCollider))
                {
                    int id = ((RTSNetworkManager)NetworkManager.singleton).Players.Count == 1 ? 1 : player.GetPlayerID() == 0 ? 1 : 0;
                    if (hitCollider.CompareTag("Player" + id) || hitCollider.CompareTag("King" + id))
                    {
                        //Debug.Log(localDistance);
                        if (localDistance > minAttackRange && localDistance < maxAttackrange)
                        {
                          
                            if (localDistance < distance)
                            {
                                findedTarget = true;
                                distance = localDistance;
                                closestTarget = hitCollider;
                                //StopTacticalBehavior while using Special Attack
                                TB.StopTacticalBehavior(player.GetPlayerID(), GetComponent<Unit>().unitType);
                                // Move the searchPoint to the next target, so it will not search at the same point
                                searchPoint = closestTarget;
                            }

                        }
                    }
                }
            }
            // if there is no more target is finded then break
            if (findedTarget == false)
            {
                break;
            }
           
            targetList.Add(closestTarget);
        }
            searchPoint = this.gameObject;
            // if it doesnot find any target return
            if (closestTarget == null) { return; }
            for (int a = 0; a < targetList.ToArray().Length; a++)
            {

                Lightlings(startPointList.ToArray()[a], targetList.ToArray()[a]);
            }
            lightlingTimer = 5;


    }
    public void Lightlings(GameObject startPoint, GameObject endPoint)
    {
        GameObject lightling;
        lightling = Instantiate(LightlingPrefab);
        lightling.transform.GetChild(0).position = startPoint.transform.position;
        lightling.transform.GetChild(1).position = endPoint.transform.position;
        startPoints.Add(lightling, startPoint);
        endPoints.Add(lightling, endPoint);
        lightlingList.Add(lightling);
        endPoint.GetComponent<Health>().DealDamage(50);
       
    }
    private void Update()
    {
        if (lightlingTimer > 0)
        {
            lightlingTimer -= Time.deltaTime;
            foreach (GameObject light in lightlingList)
            {
                startPoints.TryGetValue(light, out GameObject startpoint);
                light.transform.GetChild(0).position = startpoint.transform.position;
                endPoints.TryGetValue(light, out GameObject endpoint);
                light.transform.GetChild(1).position = endpoint.transform.position;
                //Debug.Log($"startpoint -- > {startpoint.transform.position}, endpoint -- >{endpoint.transform.position}");
            }
        }
        else
        {
            foreach (GameObject light in lightlingList)
            {
                Destroy(light);
            }

        }
    }
    public static Dictionary<GameObject, GameObject> startPoints = new Dictionary<GameObject, GameObject>()
    {

    };
    public static Dictionary<GameObject, GameObject> endPoints = new Dictionary<GameObject, GameObject>()
    {

    };


}
        
