Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineWidth ("Outline Width", Range(0.0, 0.05)) = 0.01
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Pass
        {
            Cull Off
            ZWrite Off
            Blend One OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;
            float4 _MainTex_TexelSize;
            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float alpha = col.a;

                float alphaOutline = tex2D(_MainTex, i.uv + float2(_OutlineWidth, 0)).a;
                alphaOutline = max(alphaOutline, tex2D(_MainTex, i.uv + float2(-_OutlineWidth, 0)).a);
                alphaOutline = max(alphaOutline, tex2D(_MainTex, i.uv + float2(0, _OutlineWidth)).a);
                alphaOutline = max(alphaOutline, tex2D(_MainTex, i.uv + float2(0, -_OutlineWidth)).a);

                if (alphaOutline < 1 && alpha == 1)
                {
                    return _OutlineColor;
                }

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
