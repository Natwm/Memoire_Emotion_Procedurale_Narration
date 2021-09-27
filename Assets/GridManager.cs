using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float m_Size;
    [SerializeField] private Vector2 m_GridSize;
    [SerializeField] private GameObject m_TilesPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        CreateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTerrain()
    {
        for (int x = 0; x < m_GridSize.x; x++)
        {
            for (int y = 0; y < m_GridSize.y; y++)
            {
                GameObject tile = Instantiate(m_TilesPrefabs, new Vector3(x, y, -.5f) *2 * m_Size, Quaternion.identity, this.transform);
                tile.transform.localScale *= m_Size;
                tile.GetComponent<TileElt_Behaviours>().Tileposition = new Vector2(x, y);
            }
        }
    }
}
