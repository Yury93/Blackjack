using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimator 
{
    private Animator animator;
    private string enableState = "Enable";
    private string disableState = "Disable";
    public Button GetButton { get; private set; }
    public ButtonAnimator(Button button)
    {
        GetButton = button;
        animator = button.GetComponent<Animator>();
    }
    public void ButtonEnable()
    {
        animator.SetTrigger(enableState);
        GetButton.interactable = true;
    }
    public void ButtonDisabled()
    {
        animator.SetTrigger(disableState);
        GetButton.interactable = false;
    }

}
