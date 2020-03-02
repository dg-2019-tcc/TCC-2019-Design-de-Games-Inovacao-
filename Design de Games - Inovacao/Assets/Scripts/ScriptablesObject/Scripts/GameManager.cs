using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    public bool GamePaused = false;

    public static GameManager Instance;
    protected virtual void Awake()
    {
        #region Singleton

        if (Instance)
        {
            Debug.Log("There is a Soccer Manager Instance already. Destroying " + this.name);
            Destroy(this);
            return;
        }
        Instance = this;

        #endregion
    }


    public virtual void SetPauseState(bool pauseState)
    {
        GamePaused = pauseState;
        Debug.Log($"GamePaused: {GamePaused}");
    }

    public virtual void ChangePauseState()
    {
        SetPauseState(!GamePaused);
    }
}
