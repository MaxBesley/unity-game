Shader "Custom/Water"
{
    Properties
    {
        // Shader parameters
        _Color ("Color", Color) = (1,1,1,1)
        _NormalMap1 ("Normal texture 1", 2D) = "bump" {}
        _NormalMap2 ("Normal texture 2", 2D) = "bump" {}
        _NoiseTex ("Noise texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _Scale ("Noise scale", Range(0.01, 0.1)) = 0.03
        _Amplitude ("Amplitude", Range(0.01, 0.1)) = 0.015
        _Speed ("Speed", Range(0.01, 0.3)) = 0.15
        _NormalStrength ("Normal Strength", Range(0, 1)) = 0.5
        _RippleSpeed("Ripple Speed", Range(0.1, 20)) = 3.0
        _SoftFactor("Soft Factor", Range(0.01, 30.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "ForceNoShadowCasting" = "True"}
        // Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "ForceNoShadowCasting" = "True" }  // if you don't want fog to affect the water
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha vertex:vert
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _CameraDepthTexture;
        sampler2D _NormalMap1;
        sampler2D _NormalMap2;
        sampler2D _NoiseTex;

        float _Scale;
        float _Amplitude;
        float _Speed;
        float _RippleSpeed;
        float _NormalStrength;
        float _SoftFactor;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input
        {
            float2 uv_NormalMap1;
            float4 screenPos;
            float eyeDepth;
        };


        void vert(inout appdata_full v, out Input o)
        {
            // To make the vertices of the plane mesh move
            float2 NoiseUV = float2((v.texcoord.xy + _Time * _Speed) * _Scale);  // get coord of noise
            float NoiseValue = tex2Dlod(_NoiseTex, float4(NoiseUV, 0, 0)).x;     // note that `tex2D()` doesn't work in vertex function
            v.vertex.y += NoiseValue * _Amplitude;

            // For camera depth texture (different from "Fog.shader")
            UNITY_INITIALIZE_OUTPUT(Input, o);
            COMPUTE_EYEDEPTH(o.eyeDepth);
        }

        // https://stackoverflow.com/questions/50429120/what-does-the-shader-function-surf-do
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // This is HLSL surface shader boilerplate
            fixed4 c = _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            // Use depth texture macros
            float rawZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
            float sceneZ = LinearEyeDepth(rawZ);
            float partZ = IN.eyeDepth;

            // Compute by how much we should reduce the water albedo
            float fade = saturate(_SoftFactor * (sceneZ - partZ));
            o.Alpha = fade * 0.5;

            // Moving two normal maps across each other in orthogonal
            // directions creates a moving ripple effect on the water's surface
            float normalUVX = IN.uv_NormalMap1.x + sin(_Time) * _RippleSpeed;
            float normalUVY = IN.uv_NormalMap1.y + sin(_Time) * _RippleSpeed;

            // These are the altered 2D coordinates we will use to sample the normal maps
            float2 normalUVCoord1 = float2(normalUVX,    // change x
                                           IN.uv_NormalMap1.y);
            float2 normalUVCoord2 = float2(IN.uv_NormalMap1.x,
                                           normalUVY);   // change y

            // Layer the two normal maps over each other to make the plane's surface look like water
            o.Normal = UnpackNormal((tex2D(_NormalMap1, normalUVCoord1) + tex2D(_NormalMap2, normalUVCoord2)) * _NormalStrength);
        }
        ENDCG
    }
    FallBack "Diffuse" // if the hardware doesn't support the above subshader then instead use this more default shader
}

//
// Bibliography:
//
// - "Unity 3D Water Shader - HLSL Tutorial" https://www.youtube.com/watch?v=wcGT_jji5xQ&ab_channel=FreedomCoding    <-- the main resource I used
//
// - "How Water Works (in Video Games)" https://www.youtube.com/watch?v=BqJm3B8cubo&ab_channel=StylizedStation
//
// - "Why Unpack Normals in Unity3D?" https://canopy.procedural-worlds.com/library/deep-dives/maths/why-unpack-normals-in-unity3d-r113/
//
// - "Normal mapping wiki" https://en.wikipedia.org/wiki/Normal_mapping
//
// - "GPU Lecture 38: Surface Shaders in Unity" https://www.youtube.com/watch?v=eam965gx9bc&ab_channel=Lantertronics-AaronLanterman
//
// - "Introduction to Shaders in Unity: A Simple Surface Shader" https://www.youtube.com/watch?v=xaM1v7vi9GE&ab_channel=JenMakesGames
//
// - "Writing Surface Shaders" https://docs.unity3d.com/Manual/SL-SurfaceShaders.html
//
