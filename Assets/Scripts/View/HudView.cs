using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour
{
    public Image ActiveImg;
    public void ActiveOpen(Sprite src)
    {
        if (ActiveImg == null)
            return;
        ActiveImg.gameObject.SetActive(true);
        ActiveImg.sprite = src;
    }

    public void ActiveClose()
    {
        if (ActiveImg == null)
            return;
        ActiveImg.gameObject.SetActive(false);
        ActiveImg.sprite = null;
    }
}
