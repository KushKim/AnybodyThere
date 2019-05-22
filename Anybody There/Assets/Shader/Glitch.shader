Shader "Custom/Glitch" {
	Properties {
 Properties {
		//_GlitchChanger("GlitchChanger", Range(0, 1)) = 0
		_GlitchAmount("GlitchAmount", Range(0, 1)) = 1
		[NoScaleOffset]_GlitchTex("GlitchTex", 2D) = "white" {}
		_GlitchColor1("GlitchColor1", Color) = (1, 1, 1, 1)
		_GlitchColor2("GlitchColor2", Color) = (1, 1, 1, 1)
		_GlitchColor3("GlitchColor3", Color) = (1, 1, 1, 1)

		_GlitchCutAmountX("GlitchCutAmountX", Range(0.1, 10)) = 1 //글리치 잘리는 양.
		_GlitchCutAmountY("GlitchCutAmountY", Range(0.1, 10)) = 1 //글리치 잘리는 양.
	   }

	   SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 200

		GrabPass{}

		CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf NoLighting noshadow  noambient novertexlights nolightmap nodynlightmap nodirlightmap nofog nometa noforwardadd nolppv noshadowmask halfasview

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _GrabTexture;
			sampler2D _GlitchTex;

			struct Input {
			 half4 screenPos;
			 half2 uv_GlitchTex;
			};

			fixed4 _Color;
			//half _GlitchChanger;
			half _GlitchAmount;
			half3 _GlitchColor1;
			half3 _GlitchColor2;
			half3 _GlitchColor3;
			half _GlitchCutAmountX;
			half _GlitchCutAmountY;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUIn... for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			   UNITY_INSTANCING_BUFFER_END(Props)

			   void surf(Input IN, inout SurfaceOutput o) {

				half3 screenUV = IN.screenPos.xyz / IN.screenPos.w;

				//half Test = IN.uv_MainTex;

				half GlitchUV = tex2D(_GlitchTex, half2(IN.uv_GlitchTex.x * _GlitchCutAmountX + (_Time.y * 100), IN.uv_GlitchTex.y * _GlitchCutAmountY + sin(_Time.y * 100)));
				half UV = GlitchUV * _GlitchAmount;
				half GlitchAmountFinal = saturate(_GlitchAmount * 10);

				// Albedo comes from a texture tinted by color
				fixed3 r = tex2D(_GrabTexture, half2(screenUV.x + UV, screenUV.y)).r * lerp(fixed3(1, 0, 0), _GlitchColor1, GlitchAmountFinal); //레드값 처리
				fixed3 g = tex2D(_GrabTexture, half2(screenUV.x - UV, screenUV.y)).g * lerp(fixed3(0, 1, 0), _GlitchColor2, GlitchAmountFinal); //그린값 처리
				fixed3 b = tex2D(_GrabTexture, half2(screenUV.x , screenUV.y + UV)).b * lerp(fixed3(0, 0, 1), _GlitchColor3, GlitchAmountFinal); //블루값 처리

				half3 GlitchFinal = r + g + b;
				//half3 GrabFinal = tex2D(_GrabTexture, screenUV);

				half3 EffectFinal = GlitchFinal;

				o.Albedo = EffectFinal;
				o.Alpha = 1;
			   }

			   half4 LightingNoLighting(SurfaceOutput s, half3 lightDir, half atten)
			   {
				half4 c;
				c.rgb = s.Albedo;
				c.a = s.Alpha;
				return c;
			   }

			   ENDCG
			  }
			  FallBack ""
	}


