using UnityEngine.UI;

public class AllFieldPanel : Popup
{

    public void AddCloseButton(Button button)
    {
        if (closeButton != null) return;
        closeButton = button;
    }
}
