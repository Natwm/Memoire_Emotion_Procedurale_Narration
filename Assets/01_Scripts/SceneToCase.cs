using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToCase : MonoBehaviour
{
    public Camera CaptureCamera;
    public RenderTexture renderTex;
    public SpriteRenderer FondCase;
    Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {
        Texture2D newTex = ToTexture2D(renderTex);
        FondCase.sprite = Sprite.Create(newTex, new Rect(0, 0, renderTex.width, renderTex.height),Vector2.zero);        
    }

    public Texture2D ToTexture2D(RenderTexture rTex)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rTex;
        Camera.main.Render();
        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rTex.width, rTex.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();
        RenderTexture.active = currentActiveRT;
        return tex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
