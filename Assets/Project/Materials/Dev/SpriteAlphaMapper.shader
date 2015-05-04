Shader "SergeyBedov/Sprite Alpha Mapper" {
   Properties {
      _MainTex ("Block Shape Image", 2D) = "white" {} 
      _BgTex ("Background Texture Image", 2D) = "white" {} 
      _MyColor ("Uncut Color (WORKS IF THIS ALPHA IS > 0.5)", Color) = (0,0,0,0)
      _Cutoff ("Cutoff", Range (0, 1)) = 0.2
   }
   SubShader {
      Pass {    
         Cull Off
         CGPROGRAM
 
         #pragma vertex vert
         #pragma fragment frag 
         #include "UnityCG.cginc"
 
         uniform sampler2D _MainTex;    
         uniform sampler2D _BgTex;   
         uniform float _Cutoff;
         uniform float4 _MyColor;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
            float2 texcoord1 : TEXCOORD1;
            float4 color : COLOR;
         };
         struct vertexOutput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
            float2 texcoord1 : TEXCOORD1;
            float4 color : COLOR;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
            output.texcoord = input.texcoord;
            output.texcoord1 = input.texcoord1;
            output.color = input.color;
            return output;
         }
         
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 mainTexColor = tex2D(_MainTex, input.texcoord.xy);
            if (mainTexColor.a == 0)
            {
               discard;
            }
            else if (mainTexColor.a < _Cutoff)
            {	
            	if (_MyColor.a < 0.5)
            		return mainTexColor;
            	else
            		return _MyColor;
            }
            float4 bgTexColor = tex2D(_BgTex, input.texcoord1.xy);
            return bgTexColor;
         }
 
         ENDCG
      }
   }
   Fallback "Unlit/Transparent Cutout"
}