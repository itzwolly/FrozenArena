Shader "Custom/Ice Crack Shader" {
	Properties{
		_Shininess("Shininess", Range(0.01, 1)) = 0.078125
		_MainTex("Base", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_SecondTex("Cracks (A)", 2D) = "white" {}
		_Color("Cracks Color", Color) = (0.5, 0.5, 0.5, 1)
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" "IgnoreProjector" = "True" }
		LOD 400

		CGPROGRAM
		#pragma surface surf BlinnPhong

		half _Shininess;
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _SecondTex;
		float3 _Color;
		half _Cutoff;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
			float2 uv_BumpMap;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 secondTex = tex2D(_SecondTex, IN.uv_SecondTex);
			float crackVisibility = saturate((secondTex.a - _Cutoff) * 10);
			o.Albedo = lerp(mainTex.rgb, secondTex.rgb * _Color, crackVisibility);
			o.Alpha = secondTex.a;
			o.Gloss = mainTex.a;
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	}
	FallBack "Transparent/Cutout/VertexLit"
}