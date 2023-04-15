using System;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardAd : MonoBehaviour
{
    
    [SerializeField]
    private UnityEvent OnReceiveReward = null;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            AdService.instanse.ShowReward(OnReceiveReward);
        });
    }

}
