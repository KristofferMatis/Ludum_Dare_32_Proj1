Shader "Custom/TransparentColor" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader 
	{
		Blend SrcAlpha OneMinusSrcAlpha
	
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vertShader
			#pragma fragment fragShader
			
			fixed4 _Color;

			struct VertexInput 
			{
				float4 position : POSITION0;
			};
			
			struct FragmentData 
			{
				float4 position : POSITION0;
			};
			
			FragmentData vertShader(VertexInput input)
			{
				FragmentData output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.position);
				
				return output;
			}

			fixed4 fragShader (FragmentData input) : COLOR0
			{				
				return _Color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
