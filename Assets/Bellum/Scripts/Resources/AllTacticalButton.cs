using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AllTacticalButton : MonoBehaviour
{

    public Button buttonComponent;
    public int AllTacticalId;
    public TacticalBehavior tb;
    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        tb.AllUnitCommand(AllTacticalId);
    }
}
