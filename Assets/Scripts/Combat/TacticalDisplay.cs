using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TacticalDisplay : MonoBehaviour
{
    [SerializeField] private GameObject tacticalBarParent = null;
    private Quaternion startRotation;

    private void Awake()
    {
        tacticalBarParent.SetActive(false);
        startRotation = tacticalBarParent.transform.rotation;
    }
    void Update()
    {
        tacticalBarParent.transform.rotation = startRotation;
        StartCoroutine(LateCall());
    }
    private void OnDestroy()
    {
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
