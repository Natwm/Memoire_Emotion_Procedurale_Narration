using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileElt_Behaviours : MonoBehaviour
{
    [SerializeField] private Vector2 m_Tileposition;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Getter && Setter
    public Vector2 Tileposition { get => m_Tileposition; set => m_Tileposition = value; }

    #endregion
}
