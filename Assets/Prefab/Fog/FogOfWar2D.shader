Shader "Custom/FogOfWar2D"
{
    Properties
    {
        _FogColor("Fog Color", Color) = (0,0,0,1)
        _PlayerPos("Player Position", Vector) = (0,0,0,0)
        _Radius("Vision Radius", Float) = 5.0
        _EdgeSoftness("Edge Softness", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 worldPos : TEXCOORD0;
            };

            float4 _PlayerPos;
            float _Radius;
            float _EdgeSoftness;
            float4 _FogColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xy;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dist = distance(i.worldPos, _PlayerPos.xy);

                // Alpha = 0 ภายในวงสว่าง
                // Alpha เริ่มเพิ่มจากวงขอบ (Radius - EdgeSoftness) → Radius
                float alpha = 1.0; // เริ่มเป็นมืดเต็ม
                if (dist <= _Radius - _EdgeSoftness)
                {
                    alpha = 0; // ภายในวงสว่าง
                }
                else if (dist <= _Radius)
                {
                    // ทำขอบนุ่ม
                    alpha = (dist - (_Radius - _EdgeSoftness)) / _EdgeSoftness;
                }

                return fixed4(_FogColor.rgb, alpha);
            }
            ENDCG
        }
    }
}

