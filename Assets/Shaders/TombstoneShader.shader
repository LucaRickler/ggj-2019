Shader "Custom/TombstoneShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_EmissionColor("EmissionColor", Color) = (1,1,1,1)

		_MainTex("Albedo (RGB)", 2D) = "white" {}

		_MetallicMap("MetallicMap", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "white" {}
		_EmissionMap("EmissionMap", 2D) = "white" {}

		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Transparent("Alpha", Range(0,1)) = 1.0
		//_Emission("Emission", Range(0,1)) = 0.0

	}
	SubShader{
		Tags {
			"RenderType" = "Opaque"
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		//"LightMode" = "ForwardBase"
	}

	LOD 200

	CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _MetallicMap;
		sampler2D _EmissionMap;
		float _Transparent;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float2 uv_MetallicMap;
			float2 uv_EmissionMap;
		};

		half _Glossiness;
		half _Metallic;
		half _Emission;
		fixed4 _Color, _EmissionColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			fixed4 m = tex2D(_MetallicMap, IN.uv_MetallicMap) * _Metallic;

			o.Metallic = m.r;
			float result = (sin(_Time.z*0.7) + 1) *0.5f;
			//fixed4 e = tex2D(_EmissionMap, IN.uv_EmissionMap) * _Emission;
			fixed4 e = tex2D(_EmissionMap, IN.uv_EmissionMap)* (_EmissionColor * result );
			o.Emission = e.rgb;

			o.Smoothness = m.a *_Glossiness;
			o.Alpha = _Transparent;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
