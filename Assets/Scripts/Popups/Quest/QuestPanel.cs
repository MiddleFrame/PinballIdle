using System;
using Managers;
using Popups.Quest;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : Popup
{
    [SerializeField]
    private Text _title;
 
    
    [SerializeField]
    private GameObject FieldQuest;

    [SerializeField]
    private GameObject FieldQuestStroke;


    
    [SerializeField]
    private GlobalQuestPanel _globalQuestPanel;
    
    [SerializeField]
    private LocalQuestPanel _localQuestPanel;



    public void OpenGlobalQuest()
    {
        _title.text = "Global Quest";
        _globalQuestPanel.Open();
        
        if (FieldQuest.activeSelf)
            FieldQuest.SetActive(false);
        if (FieldQuestStroke.activeSelf)
            FieldQuestStroke.SetActive(false);

        UpdateGlobalQuest();
    }

    public void Init(QuestManager questManager)
    {
        _globalQuestPanel.Init(questManager);
        _localQuestPanel.Init(questManager);
    }
    
    public void OpenFieldQuest()
    {
        _title.text = "Field Quest";
        _globalQuestPanel.Close();
        if (!FieldQuest.activeSelf)
            FieldQuest.SetActive(true);
        if (!FieldQuestStroke.activeSelf)
            FieldQuestStroke.SetActive(true);
    }
    private void UpdateGlobalQuest()
    {
        _globalQuestPanel.UpdateVisualQuest();
    }
    
}
