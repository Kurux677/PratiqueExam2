Shader "Custom/LavaTexture" {
	Properties {
		_LavaTexture("texture de lave", 2D) = "Black" {}
		_RockTexture("Texture de roche", 2D) = "Gray" {}
		_DisplacementFactor("Deplacement",Range(0,1)) = 0.1
		_DispTexture("Texture de deplacement", 2D) = "Gray" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:disp

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.6

		sampler2D _LavaTexture;
		sampler2D _RockTexture;
		sampler2D _DispTexture;

		struct appdata {
			float4 vertex  : POSITION;
			float4 tangent : TANGENT;
			float3 normal  : NORMAL;
			float2 texcoord: TEXCOORD0;
		};
		float _DisplacementFactor;
		struct Input {
			float2 uv_LavaTexture;
			float2 uv_RockTexture;
			float2 uv_DispTexture;
		};

		void disp(inout appdata v) {
			float dispTexture = tex2Dlod(_DispTexture, float4(v.texcoord.xy, 0, 0)).r * _DisplacementFactor;
			v.vertex.xyz -= v.normal * dispTexture;
			v.vertex.xyz += v.normal * _DisplacementFactor;
		}

		

		

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			half amount = tex2Dlod(_DispTexture, float4(IN.uv_DispTexture, 0, 0)).r;
			fixed2 scroll = IN.uv_LavaTexture;
			scroll.x -= 5 * _Time;
			scroll.y -= 5 * _Time;
			fixed4 color = (tex2D(_LavaTexture, scroll)* tex2D(_LavaTexture, IN.uv_LavaTexture))- (tex2D(_RockTexture, IN.uv_RockTexture)*2.5);
			o.Albedo = color.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			//o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
