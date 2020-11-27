using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.OnBattleStateChange += UpdateActionUI;
    }
    private void UpdateActionUI(BattleState state)
    {
        GameObject actions = transform.GetChild(0).gameObject;
        Debug.Log(state);
        if (state == BattleState.ENEMYTURN)
        {
            actions.SetActive(false);
        } 
        else if (state == BattleState.PLAYERTURN)
        {
            actions.SetActive(true);
        }
    }
}
