using UnityEngine;

public class _GameManager : MonoBehaviour
{
    #region Singleton 

    public static _GameManager _GM;

    void Awake()
    {
       if(_GM == null)
       {
            DontDestroyOnLoad(gameObject);
            _GM = this;
       }
       else if(_GM != this)
            Destroy(gameObject);
    }

    #endregion

    public Vector3 resetPosition = new Vector3(10000f, 10000f, 10000f);
    public float normalFov = 60f;
}
