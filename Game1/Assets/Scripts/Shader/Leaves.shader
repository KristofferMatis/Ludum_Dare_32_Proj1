Shader "Custom/Leaves" {
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Shininess("Shininess", float) = 2.0
	}
	SubShader
	{
		Pass
		{
			Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase" }
			LOD 200
			
			Cull back
			
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma vertex vertShader
			#pragma fragment fragShader

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
			
			#include "UnityCG.cginc"

			struct Input
			{
				float4 pos : POSITION0;
				float3 norm : NORMAL0;
			};
			struct Output
			{
				float4 pos : POSITION0;
				float3 norm : NORMAL0;
				float4 worldPos : TEXCOORD0;
			};
			
			float4 _Color;
			float4 _LightDirection;
			float4 _LightColor0; 
			float _Shininess;
			
			Output vertShader (Input input) : POSITION0
			{
				Output output;
				output.pos = mul(UNITY_MATRIX_MVP, input.pos);
				output.norm = mul(_Object2World, input.norm);
				output.worldPos = mul(_Object2World, input.pos);
				return output;
			}
			
			float4 fragShader (Output output) : COLOR
			{
				float3 normal = normalize(output.norm);
				float4 dir = normalize(_WorldSpaceLightPos0);
				float shade = max(0.0, dot(dir.xyz, normal));
				
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - output.worldPos.xyz);
				
				return float4(_Color.rgb * shade * _LightColor0.xyz, 1.0);
			}

			ENDCG
		}
		
		Pass
		{
			Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase" }
			LOD 200
			
			Cull front
			
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma vertex vertShader
			#pragma fragment fragShader

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0
			
			#include "UnityCG.cginc"

			struct Input
			{
				float4 pos : POSITION0;
				float3 norm : NORMAL0;
			};
			struct Output
			{
				float4 pos : POSITION0;
				float3 norm : NORMAL0;
				float4 worldPos : TEXCOORD0;
			};
			
			float4 _Color;
			float4 _LightDirection;
			float4 _LightColor0; 
			float _Shininess;
			
			Output vertShader (Input input) : POSITION0
			{
				Output output;
				output.pos = mul(UNITY_MATRIX_MVP, input.pos);
				output.norm = mul(_Object2World, input.norm);
				output.worldPos = mul(_Object2World, input.pos);
				return output;
			}
			
			float4 fragShader (Output output) : COLOR
			{
				float3 normal = -normalize(output.norm);
				float4 dir = normalize(_WorldSpaceLightPos0);
				float shade = max(0.0, dot(dir.xyz, normal));
				
				float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - output.worldPos.xyz);
				
				return float4(_Color.rgb * shade * _LightColor0.xyz, 1.0);
			}
			ENDCG
		}
	} 
	//FallBack "Diffuse"
}
