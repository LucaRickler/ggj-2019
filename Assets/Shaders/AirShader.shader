// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MistShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
        _SecColor ("Secondary Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _FogTex ("Mist Mask", 2D) = "white" {}
        _Ratio ("Mist Ratio", Range(0,1)) = 0.5
        _Size ("Mist Size", Float) = 10
        _SpeedX ("Mist Speed X", Float) = 0.2
        _SpeedY ("Mist Speed Y", Float) = 0
	}
	SubShader {         
        Tags { 
             "RenderType" = "Opaque" 
             "Queue" = "Transparent+1" 
        }
		LOD 200

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha 

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON

            #include "ClassicNoise.cginc"

            sampler2D _MainTex;
            sampler2D _FogTex;

            struct Input {
                float2 uv_MainTex;
            };

            half _Size;
            half _Ratio;
            half _SpeedX;
            half _SpeedY;
            fixed4 _Color;
            fixed4 _SecColor;
 
            struct Vertex {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };

            struct Fragment {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };

            Fragment vert(Vertex v) {
                Fragment o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = v.uv_MainTex;

                return o;
            }
                                             
            float4 frag(Fragment IN) : COLOR {
                float2 pos = IN.uv_MainTex + float2(_SpeedX * _Time.x, _SpeedY * _Time.x);
                half fogP = (cnoise(pos*_Size) + 0.2*cnoise(pos*_Size*10) + 0.25f) * tex2D(_FogTex, pos);
                float4 fog = (1.0f, 1.0f, 1.0f, 1.0f - fogP * _Ratio);

                float4 o = tex2D(_MainTex, IN.uv_MainTex) * _Color * fog;

                return o;
            }
	    ENDCG
	    }
    }
}