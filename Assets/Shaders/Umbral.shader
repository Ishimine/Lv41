Shader "Unlit/Umbral"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Umbral ("Umbral", Range(0,1)) = 0.5
		_Color ("Color", Color) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

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

			static const float PI = 3.14159f;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Umbral;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 umbral = tex2D(_MainTex, i.uv);
				fixed4 col = _Color;
				col.a = smoothstep(_Umbral - sin(_Umbral*PI)*.5f, _Umbral , umbral.r);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}