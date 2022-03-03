public abstract class UICounter : UIBase
{
    protected int prevValue; //Previous value of the coin counter.

    protected void CheckAndUpdateValue(int valueToCheck)
    {
        if (prevValue != valueToCheck)
        {
            prevValue = valueToCheck;
            UpdateUI(prevValue);
        }
    }
}
