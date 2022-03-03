using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public abstract class UIBase : MonoBehaviour
{
    protected string counterName;
    protected Text UIText;
    protected VariableController globalVars;

    protected virtual void Start()
    {
        UIText = GetComponent<Text>();
        globalVars = GameObject.FindGameObjectWithTag("GameController").GetComponent<VariableController>();
    }

    protected void UpdateUI(object newValue)
    {
        UIText.text = counterName + "\n" + newValue;
    }
}
