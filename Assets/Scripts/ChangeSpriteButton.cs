using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteButton : MonoBehaviour
{

    public bool ResetBall;
    public Sprite TargetSprite;
    private void Start()
    {
        ChangeSprite();
    }
    public void ChangeSprite()
    {
        if(Teleport.i[0]==5&&ResetBall)
        gameObject.GetComponent<Image>().sprite = TargetSprite;
    }
}
