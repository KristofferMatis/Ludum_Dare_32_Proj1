Shader "Custom/BoatRequirement" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_ScratchColor ("Scratch Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
		_ScratchTex ("Scratch (RGB)", 2D) = "white" {}
		_ScratchAmount ("Scratch amount", Range(0,1)) = 0.0
	}
	
	SubShader 
	{
		Tags { "RenderType"="Transparent" }
		
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vertShader
			#pragma fragment fragShader

			sampler2D _MainTex;		
			sampler2D _ScratchTex;
			fixed4 _Color;		
			fixed4 _ScratchColor;		
			fixed _ScratchAmount;

			struct VertexInput 
			{
				float4 position : POSITION0;
				float2 uv_MainTex : TEXCOORD0;
			};
			
			struct FragmentData 
			{
				float4 position : POSITION0;
				float2 uv_MainTex : TEXCOORD0;
			};
			
			FragmentData vertShader(VertexInput input)
			{
				FragmentData output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.position);
				output.uv_MainTex = input.uv_MainTex;
				
				return output;
			}

			fixed4 fragShader (FragmentData input) : COLOR0
			{
				fixed4 mainColor = tex2D (_MainTex, input.uv_MainTex);
				fixed4 scratchColor = tex2D (_ScratchTex, input.uv_MainTex);
				
				fixed4 resultColor = mainColor * _Color * mainColor.a * (1.0 - _ScratchAmount * scratchColor.a) + scratchColor * _ScratchColor * _ScratchAmount * scratchColor.a;
				return resultColor;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
