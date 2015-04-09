using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;

	void Awake()
    {
        this.guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);

    }
	
	// Update is called once per frame
	void Update () {
        if (sceneStarting)
        {
            StartScene();
        }
	}

    void FadeToClear()
    {
        this.guiTexture.color = Color.Lerp(this.guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    void FadeToBlack()
    {
        this.guiTexture.color = Color.Lerp(this.guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if(this.guiTexture.color.a<=0.05f)
        {
            //把纹理颜色直接设为透明 然后禁用纹理 从而停止渲染 防止占用资源
            this.guiTexture.color = Color.clear;
            this.guiTexture.enabled = false;

            sceneStarting = false;
        }
    }
    public void EndScene()
    {
        this.guiTexture.enabled = true;

        FadeToBlack();

        if (guiTexture.color.a >= 0.95f)
            Application.LoadLevel(0);
    }
}
