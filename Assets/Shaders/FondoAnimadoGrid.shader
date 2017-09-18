Shader "Unlit/FondoAnimadoGrid"
{
	Properties
	{
		_AlphaExtra ("ExtraAlpha", Range(0,5)) = 0
		_MainTex ("Texture", 2D) = "white" {}		
//		_AlphaTex ("Alpha Texture", 2D) = "white" {}		
		_Tint ("Color", Color) = (1,1,1,1)
		_ScrollSpeeds ("Scroll Speeds", vector) = (-5.0, -20.0, 0, 0)
	}
	SubShader
	{

	
	Lighting Off
	ZWrite Off
	Cull back
	Blend SrcAlpha OneMinusSrcAlpha


		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			half4 _Tint;
			float4 _ScrollSpeeds;

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			float _AlphaExtra;

			sampler2D _MainTex;
			float4 _MainTex_ST;
	//		sampler2D _AlphaTex;

		v2f vert (appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

    // Shift the uvs over time.
    o.uv += _ScrollSpeeds * _Time.x;

    UNITY_TRANSFER_FOG(o,o.vertex);
    return o;
}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed alpha = tex2D(_AlphaTex, TRANSFORM_TEX(v.uv, _AlphaTex)).a;
				fixed4 col = tex2D(_MainTex, i.uv) * _Tint + _AlphaExtra/* *alpha*/;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
