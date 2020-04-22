Shader "Custom/FogOfWarMask"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BlurPower("BlurPower", float) = 0.002
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
       //_Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting off
		LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert NoLighting alpha:blend
		#pragma target 3.0

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, float aten) {
		fixed4 color;
		color.rgb = s.Albedo;
		color.a = s.Alpha;
		return color;
		}

		fixed4 _Color;
		sampler2D _MainTex;
		float _BlurPower;

		struct Input {
		 float2 uv_MainTex;
		};

		 void surf(Input IN, inout SurfaceOutput o) {
		 half4 baseColor1 = tex2D(_MainTex, IN.uv_MainTex + float2(-_BlurPower, 0));
		 half4 baseColor2 = tex2D(_MainTex, IN.uv_MainTex + float2(0, -_BlurPower));
		 half4 baseColor3 = tex2D(_MainTex, IN.uv_MainTex + float2(_BlurPower, 0));
		 half4 baseColor4 = tex2D(_MainTex, IN.uv_MainTex + float2(0, _BlurPower));
		 half4 baseColor = 0.50 * (baseColor1 + baseColor2 + baseColor3 + baseColor4);

		 //o.Albedo = baseColor.rgb;
		 o.Albedo = _Color.rgb * baseColor.b;
		
		o.Alpha = _Color.a - baseColor.g;
		}
   ENDCG
  }
 
}
