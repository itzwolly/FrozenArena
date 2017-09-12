Shader "IcyBaby/IcyShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_AOTex ("Occlusion (R)", 2D) = "white" {}
		_SnowTex ("Cracks (RGB)", 2D) = "black" {}
		_NormalTex ("Normal (RGB)", 2D) = "normal" {}

		_FreshSnow ("Cracks built", Range(0,1)) = 0.0
		_MeltedSnow ("Cracks cap", Range(0,1)) = 0.0
		_Glossiness ("Smoothness", Range(0,1)) = 0.0
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_Amount ("Explode", Range(-1,1)) = 0.5
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 300
		
		Cull back
		ZWrite On
		ZTest LEqual

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert


		//DX11 shader model 5.0.
		#pragma target 5.0

		//Input
		sampler2D _MainTex;
		sampler2D _SnowTex;
		sampler2D _NormalTex;
		sampler2D _AOTex;
		sampler2D _Hight;
		float4 _RimColor;
		float _RimPower;




		struct Input {
			float2 uv_MainTex : TEXCOORD0;
			float2 uv_NormalTex : TEXCOORD0;
			float2 uv_AOTex : TEXCOORD0;
			fixed3 viewDir;
		};

	
		half _FreshSnow;
		half _MeltedSnow;
		half _Glossiness;
		half _Metallic;
		half3 Normal;
		fixed4 _Color;
		fixed4 _EmCol;
		float _Amount;


		void vert (inout appdata_full v) 
		{
			//Takes the verticies in all directions and move them acording to the normal direction
			v.vertex.xyz += v.normal * _Amount;
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 AO = tex2D (_AOTex,IN.uv_MainTex);
			fixed4 snowColor = tex2D (_SnowTex, IN.uv_MainTex);
			
			half smoothRange = 0;
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));

			if (_MeltedSnow <= 0 ){
				smoothRange=0.2;
				_FreshSnow *= (1+smoothRange);
			} else if (_MeltedSnow >= 0) {
				snowColor.r / c.rgb;
				smoothRange=0.2;
			}

			fixed3 col;


			if (snowColor.r >= _FreshSnow) {
				col = c.rgb;
			} else {
				col = fixed3(1,1,1);//snow
			}

			if (col.r < _MeltedSnow) col = c.rgb;



			// Metallic and smoothness come from slider variables
			//fixed4 EmissionCol (_EmCol, IN.uv_MainTex);

			o.Albedo = col;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha =  c.a;
			o.Occlusion = AO.r;
			o.Normal = UnpackNormal (tex2D (_NormalTex, IN.uv_MainTex));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
