Shader "Standard (Double Face)"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic (R) Smoothness (A) Occlusion (G)", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		Cull Off

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
		};

		sampler2D _Normal;
		sampler2D _Metallic;
		UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_MainTex));
			fixed4 m = tex2D(_Metallic, IN.uv_MainTex);
			o.Metallic = m.r;
			o.Smoothness = m.a;
			o.Occlusion = m.g;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
