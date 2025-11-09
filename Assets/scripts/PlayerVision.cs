using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float Range = 5f;        // ระยะการมองเห็น
    public SpriteRenderer fogSprite;       // Sprite ของ FogOverlay

    private Material fogMat;

    void Start()
    {
        if (fogSprite != null)
            fogMat = fogSprite.material;
    }

    void Update()
    {
        if (fogMat != null)
        {
            // ส่งตำแหน่ง Player เป็น world position ให้ Shader
            fogMat.SetVector("_PlayerPos", new Vector4(transform.position.x, transform.position.y, 0, 0));

            // ส่ง visionRadius ล่าสุดให้ Shader
            fogMat.SetFloat("_Radius", Range);
        }
    }

    // ตรวจสอบวงกลมใน Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, Range);
    }
}