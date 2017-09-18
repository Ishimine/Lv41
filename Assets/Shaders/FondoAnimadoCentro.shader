Shader "Unlit/FondoAnimadoCentro"
{
	Properties
	{	
		_MainTex ("Texture", 2D) = "white" {}
		_Tint ("Color", Color) = (1,1,1,1)
		_ScrollSpeeds ("Scroll Speeds", vector) = (-5.0, -20.0, 0, 0)
		_AlphaExtra ("ExtraAlpha", Range(0,5)) = 0

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

			// Declare our second texture sampler and its Scale/Translate values
			half4 _Tint;

			float4 _ScrollSpeeds;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				// Shift the UVs so (0, 0) is in the middle of the quad.
				o.uv = v.uv - 0.5f;

			



				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

		fixed4 frag (v2f i) : SV_Target
			{
				// Convert our texture coordinates to polar form:
				float2 polar = float2(
					   atan2(i.uv.y, i.uv.x)/(2.0f * 3.141592653589f), // angle
					   length(i.uv)                                    // radius
					);

				// Apply texture scale
				polar *= _MainTex_ST.xy;

				// Scroll the texture over time.
				polar += _ScrollSpeeds.xy * _Time.x;

				// Sample using the polar coordinates, instead of the original uvs.
				// Here I multiply by MainTex
				fixed4 col = tex2D(_MainTex, polar) * _Tint;
				if(col.a > 0) col.a +=  + _AlphaExtra;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
