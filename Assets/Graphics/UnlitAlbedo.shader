Shader "Custom/UnlitAlbedo"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _AlbedoColor ("Albedo Color", Color) = (1, 1, 1, 1)
        _Alpha ("Transparency", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            // Set up blending and transparency
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest LEqual
            Cull Front

CGPROGRAM
    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float4 _AlbedoColor;
    float _Alpha;

    struct appdata_t
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    v2f vert(appdata_t v)
    {
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }

    float4 frag(v2f i) : SV_Target
    {
        // Get texture color
        float4 texCol = tex2D(_MainTex, i.uv);
        
        // Apply the albedo color to the texture
        // If the albedo color is (1, 1, 1, 1), no change will happen, otherwise, it will tint the texture
        float4 col = texCol * _AlbedoColor;  // Apply albedo color tint
        
        // Apply transparency using alpha slider
        col.a = texCol.a * _Alpha;  // Keep the texture's original alpha, but override with the transparency slider

        return col;
    }

    ENDCG
        }
    }

    Fallback "Diffuse"
}
