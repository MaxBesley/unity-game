Shader "Unlit/Fog"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            struct vertIn
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertOut
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;  // Unity gives this to us for free
            float4 _FogColor;
            float _FogDensity, _FogStart;


            vertOut vert(vertIn v)
            {
                vertOut o;
                o.uv = v.uv;
                // transforms from model space to clip space
                // (Unity automatically replaced 'mul(UNITY_MATRIX_MVP,...)' with this)
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(vertOut v) : SV_Target
            {
                // sample colour from the object's texture
                float4 color = tex2D(_MainTex, v.uv);

                // sample the "camera depth texture" from the pixel
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, v.uv);
                // make `depth` be between 0 and 1
                depth = Linear01Depth(depth);

                // then multiply by camera’s far plane to get view distance
                float viewDistance = depth * _ProjectionParams.z;

                // calculate the (exponential squared) fog factor
                float fogFactor = (_FogDensity / sqrt(log(2))) * max(0.0f, viewDistance - _FogStart);
                fogFactor = exp2(-fogFactor * fogFactor);

                // interpolate between the fog and object color
                return lerp(_FogColor, color, saturate(fogFactor));
            }
            ENDCG
        }
    }
}

//
// Bibliography
//
// - "Simple Fog In Unity" https://www.youtube.com/watch?v=EFt_lLVDeRo&ab_channel=Acerola    <-- the main resource I used
//
// - "Catlike Coding" on fog https://catlikecoding.com/unity/tutorials/rendering/part-14/    <-- very insightful
//
// - "Unity - How to add a Material to a Camera" https://www.youtube.com/watch?v=HW8UePVtU5M&ab_channel=Quickz
//
// - Many tutorial videos from this channel --> https://www.youtube.com/@benjaminswee-shaders
//
// - "How Big Budget AAA Games Render Clouds" https://www.youtube.com/watch?v=Qj_tK_mdRcA&ab_channel=SimonDev    <-- helpful conceptually
//
