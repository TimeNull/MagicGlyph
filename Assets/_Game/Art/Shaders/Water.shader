// Toony Colors Pro+Mobile 2
// (c) 2014-2021 Jean Moreno

Shader "Water"
{
	Properties
	{
		[Enum(Front, 2, Back, 1, Both, 0)] _Cull ("Render Face", Float) = 2.0
		[TCP2ToggleNoKeyword] _ZWrite ("Depth Write", Float) = 1.0
		[HideInInspector] _RenderingMode ("rendering mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("blending source", Float) = 1.0
		[HideInInspector] _DstBlend ("blending destination", Float) = 0.0
		[TCP2Separator]

		[TCP2HeaderHelp(Base)]
		_BaseColor ("Color", Color) = (1,1,1,1)
		[TCP2ColorNoAlpha] _HColor ("Highlight Color", Color) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] _SColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)
		_BaseMap ("Albedo", 2D) = "white" {}
		[TCP2Vector4FloatsDrawer(Speed,Amplitude,Frequency,Offset)] _BaseMap_SinAnimParams ("Albedo UV Sine Distortion Parameters", Float) = (1, 0.05, 1, 0)
		_Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
		[TCP2Separator]

		[TCP2Header(Ramp Shading)]
		
		_RampThreshold ("Threshold", Range(0.01,1)) = 0.5
		_RampSmoothing ("Smoothing", Range(0.001,1)) = 0.5
		[TCP2Separator]
		
		[TCP2HeaderHelp(Vertex Displacement)]
		_WorldDisplacementTex ("World Displacement Texture", 2D) = "black" {}
		 _DisplacementStrength ("Displacement Strength", Range(-1,1)) = 0.01
		[TCP2Separator]
		
		[TCP2HeaderHelp(Vertex Waves Animation)]
		_WavesSpeed ("Speed", Float) = 2
		_WavesHeight ("Height", Float) = 0.1
		_WavesFrequency ("Frequency", Range(0,10)) = 1
		
		[TCP2HeaderHelp(Depth Based Effects)]
		[TCP2ColorNoAlpha] _DepthColor ("Depth Color", Color) = (0,0,1,1)
		[PowerSlider(5.0)] _DepthColorDistance ("Depth Color Distance", Range(0.01,3)) = 0.5
		[PowerSlider(5.0)] _DepthAlphaDistance ("Depth Alpha Distance", Range(0.01,10)) = 0.5
		_DepthAlphaMin ("Depth Alpha Min", Range(0,1)) = 0.5
		_FoamSpread ("Foam Spread", Range(0,5)) = 2
		_FoamStrength ("Foam Strength", Range(0,1)) = 0.8
		_FoamColor ("Foam Color (RGB) Opacity (A)", Color) = (0.9,0.9,0.9,1)
		_FoamTex ("Foam Texture", 2D) = "black" {}
		_FoamSpeed ("Foam Speed", Vector) = (2,2,2,2)
		_FoamSmoothness ("Foam Smoothness", Range(0,0.5)) = 0.02
		
		[ToggleOff(_RECEIVE_SHADOWS_OFF)] _ReceiveShadowsOff ("Receive Shadows", Float) = 1

		//Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType"="TransparentCutout"
			"Queue"="AlphaTest"
		}

		HLSLINCLUDE
		#define fixed half
		#define fixed2 half2
		#define fixed3 half3
		#define fixed4 half4

		#if UNITY_VERSION >= 202020
			#define URP_10_OR_NEWER
		#endif

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

		// Uniforms

		// Shader Properties
		sampler2D _WorldDisplacementTex;
		sampler2D _BaseMap;
		sampler2D _FoamTex;

		CBUFFER_START(UnityPerMaterial)
			
			// Shader Properties
			float4 _WorldDisplacementTex_ST;
			float _DisplacementStrength;
			float _WavesFrequency;
			float _WavesHeight;
			float _WavesSpeed;
			float4 _BaseMap_ST;
			half4 _BaseMap_SinAnimParams;
			float _Cutoff;
			fixed4 _BaseColor;
			fixed4 _DepthColor;
			float _DepthColorDistance;
			float _FoamSpread;
			float _FoamStrength;
			fixed4 _FoamColor;
			float4 _FoamSpeed;
			float4 _FoamTex_ST;
			float _FoamSmoothness;
			float _DepthAlphaDistance;
			float _DepthAlphaMin;
			float _RampThreshold;
			float _RampSmoothing;
			fixed4 _SColor;
			fixed4 _HColor;
		CBUFFER_END

		// Built-in renderer (CG) to SRP (HLSL) bindings
		#define UnityObjectToClipPos TransformObjectToHClip
		#define _WorldSpaceLightPos0 _MainLightPosition
		
		ENDHLSL

		Pass
		{
			Name "Main"
			Tags
			{
				"LightMode"="UniversalForward"
			}
		Blend [_SrcBlend] [_DstBlend]
		Cull [_Cull]
		ZWrite [_ZWrite]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard SRP library
			// All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 3.0

			// -------------------------------------
			// Material keywords
			#pragma shader_feature _ _RECEIVE_SHADOWS_OFF

			// -------------------------------------
			// Universal Render Pipeline keywords
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile _ _SHADOWS_SOFT
			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK

			// -------------------------------------

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex Vertex
			#pragma fragment Fragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
		#pragma shader_feature _ _ALPHAPREMULTIPLY_ON

			// vertex input
			struct Attributes
			{
				float4 vertex       : POSITION;
				float3 normal       : NORMAL;
				float4 tangent      : TANGENT;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			// vertex output / fragment input
			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float3 normal         : NORMAL;
				float4 worldPosAndFog : TEXCOORD0;
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord    : TEXCOORD1; // compute shadow coord per-vertex for the main light
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				half3 vertexLights : TEXCOORD2;
			#endif
				float4 screenPosition : TEXCOORD3;
				float4 pack1 : TEXCOORD4; /* pack1.xy = texcoord0  pack1.zw = sinUvAnimVertexPos */
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings Vertex(Attributes input)
			{
				Varyings output = (Varyings)0;

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				// Used for texture UV sine animation
				float2 sinUvAnimVertexPos = input.vertex.xy + input.vertex.yz;
				output.pack1.zw = sinUvAnimVertexPos;

				// Texture Coordinates
				output.pack1.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;
				// Shader Properties Sampling
				float3 __vertexDisplacementWorld = ( tex2Dlod(_WorldDisplacementTex, float4(output.pack1.xy * _WorldDisplacementTex_ST.xy + _WorldDisplacementTex_ST.zw, 0, 0)).rgb * _DisplacementStrength );
				float __wavesFrequency = ( _WavesFrequency );
				float __wavesHeight = ( _WavesHeight );
				float __wavesSpeed = ( _WavesSpeed );
				float4 __wavesSinOffsets1 = ( float4(1,2.2,0.6,1.3) );
				float4 __wavesPhaseOffsets1 = ( float4(1,1.3,2.2,0.4) );
				float4 __wavesSinOffsets2 = ( float4(0.6,1.3,3.1,2.4) );
				float4 __wavesPhaseOffsets2 = ( float4(2.2,0.4,3.3,2.9) );
				float4 __wavesSinOffsets3 = ( float4(1.4,1.8,4.2,3.6) );
				float4 __wavesPhaseOffsets3 = ( float4(0.2,2.6,0.7,3.1) );
				float4 __wavesSinOffsets4 = ( float4(1.1,2.8,1.7,4.3) );
				float4 __wavesPhaseOffsets4 = ( float4(0.5,4.8,3.1,2.3) );

				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				worldPos.xyz += __vertexDisplacementWorld;
				input.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
				
				// Vertex water waves
				float _waveFrequency = __wavesFrequency;
				float _waveHeight = __wavesHeight;
				float3 _vertexWavePos = worldPos.xyz * _waveFrequency;
				float _phase = _Time.y * __wavesSpeed;
				half4 vsw_offsets_x = __wavesSinOffsets1;
				half4 vsw_ph_offsets_x = __wavesPhaseOffsets1;
				half4 vsw_offsets_z = __wavesSinOffsets2;
				half4 vsw_ph_offsets_z = __wavesPhaseOffsets2;
				half4 vsw_offsets_x2 = __wavesSinOffsets3;
				half4 vsw_ph_offsets_x2 = __wavesPhaseOffsets3;
				half4 vsw_offsets_z2 = __wavesSinOffsets4;
				half4 vsw_ph_offsets_z2 = __wavesPhaseOffsets4;
				half4 waveX = sin((_vertexWavePos.xxxx * vsw_offsets_x) + (_phase.xxxx * vsw_ph_offsets_x));
				half4 waveZ = sin((_vertexWavePos.zzzz * vsw_offsets_z) + (_phase.xxxx * vsw_ph_offsets_z));
				half4 waveX2 = sin((_vertexWavePos.xxxx * vsw_offsets_x2) + (_phase.xxxx * vsw_ph_offsets_x2));
				half4 waveZ2 = sin((_vertexWavePos.zzzz * vsw_offsets_z2) + (_phase.xxxx * vsw_ph_offsets_z2));
				
				float waveFactorX = (dot(waveX.xyzw, 1) + dot(waveX2.xyzw, 1)) * _waveHeight / 8;
				float waveFactorZ = (dot(waveZ.xyzw, 1) + dot(waveZ2.xyzw, 1)) * _waveHeight / 8;
				input.vertex.y += (waveFactorX + waveFactorZ);
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				output.shadowCoord = GetShadowCoord(vertexInput);
			#endif
				float4 clipPos = vertexInput.positionCS;

				float4 screenPos = ComputeScreenPos(clipPos);
				output.screenPosition.xyzw = screenPos;

				VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(input.normal);
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				// Vertex lighting
				output.vertexLights = VertexLighting(vertexInput.positionWS, vertexNormalInput.normalWS);
			#endif

				// world position
				output.worldPosAndFog = float4(vertexInput.positionWS.xyz, 0);

				// normal
				output.normal = NormalizeNormalPerVertex(vertexNormalInput.normalWS);

				// clip position
				output.positionCS = vertexInput.positionCS;

				return output;
			}

			half4 Fragment(Varyings input) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				// Texture Coordinates
				
				float3 positionWS = input.worldPosAndFog.xyz;
				float3 normalWS = NormalizeNormalPerPixel(input.normal);

				float2 uvSinAnim__BaseMap = (input.pack1.zw * _BaseMap_SinAnimParams.z) + (_Time.yy * _BaseMap_SinAnimParams.x);
				// Shader Properties Sampling
				float4 __albedo = ( tex2D(_BaseMap, input.pack1.xy + (((sin(0.9 * uvSinAnim__BaseMap + _BaseMap_SinAnimParams.w) + sin(1.33 * uvSinAnim__BaseMap + 3.14 * _BaseMap_SinAnimParams.w) + sin(2.4 * uvSinAnim__BaseMap + 5.3 * _BaseMap_SinAnimParams.w)) / 3) * _BaseMap_SinAnimParams.y)).rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __cutoff = ( _Cutoff );
				float3 __depthColor = ( _DepthColor.rgb );
				float __depthColorDistance = ( _DepthColorDistance );
				float __foamSpread = ( _FoamSpread );
				float __foamStrength = ( _FoamStrength );
				float4 __foamColor = ( _FoamColor.rgba );
				float4 __foamSpeed = ( _FoamSpeed.xyzw );
				float2 __foamTextureBaseUv = ( input.pack1.xy.xy );
				float __foamMask = ( .0 );
				float __foamSmoothness = ( _FoamSmoothness );
				float __depthAlphaDistance = ( _DepthAlphaDistance );
				float __depthAlphaMin = ( _DepthAlphaMin );
				float __ambientIntensity = ( 1.0 );
				float __rampThreshold = ( _RampThreshold );
				float __rampSmoothing = ( _RampSmoothing );
				float3 __shadowColor = ( _SColor.rgb );
				float3 __highlightColor = ( _HColor.rgb );

				// Sample depth texture and calculate difference with local depth
				float sceneDepth = SampleSceneDepth(input.screenPosition.xyzw.xy / input.screenPosition.xyzw.w);
				if (unity_OrthoParams.w > 0.0)
				{
					// Orthographic camera
					#if UNITY_REVERSED_Z
						sceneDepth = ((_ProjectionParams.z - _ProjectionParams.y) * (1.0 - sceneDepth) + _ProjectionParams.y);
					#else
						sceneDepth = ((_ProjectionParams.z - _ProjectionParams.y) * (sceneDepth) + _ProjectionParams.y);
					#endif
				}
				else
				{
					// Perspective camera
					sceneDepth = LinearEyeDepth(sceneDepth, _ZBufferParams);
				}
				
				//float localDepth = LinearEyeDepth(worldPos, UNITY_MATRIX_V);
				float localDepth = LinearEyeDepth(input.screenPosition.xyzw.z / input.screenPosition.xyzw.w, _ZBufferParams);
				float depthDiff = abs(sceneDepth - localDepth);

				// main texture
				half3 albedo = __albedo.rgb;
				half alpha = __alpha;
				half3 emission = half3(0,0,0);

				//Alpha Testing
				clip(alpha - __cutoff);
				
				albedo *= __mainColor.rgb;
				
				// Depth-based color
				half3 depthColor = __depthColor;
				half3 depthColorDist = __depthColorDistance;
				albedo.rgb = lerp(depthColor, albedo.rgb, saturate(depthColorDist * depthDiff));
				
				// Depth-based water foam
				half foamSpread = __foamSpread;
				half foamStrength = __foamStrength;
				half4 foamColor = __foamColor;
				
				half4 foamSpeed = __foamSpeed;
				float2 foamUV = __foamTextureBaseUv;
				
				float2 foamUV1 = foamUV.xy + _Time.yy * foamSpeed.xy * 0.05;
				half3 foam = ( tex2D(_FoamTex, foamUV1 * _FoamTex_ST.xy + _FoamTex_ST.zw).rgb );
				
				foamUV.xy += _Time.yy * foamSpeed.zw * 0.05;
				half3 foam2 = ( tex2D(_FoamTex, foamUV * _FoamTex_ST.xy + _FoamTex_ST.zw).rgb );
				
				foam = (foam + foam2) / 2.0;
				float foamDepth = saturate(foamSpread * depthDiff) * (1.0 - __foamMask);
				half foamSmooth = __foamSmoothness;
				half foamTerm = (smoothstep(foam.r - foamSmooth, foam.r + foamSmooth, saturate(foamStrength - foamDepth)) * saturate(1 - foamDepth)) * foamColor.a;
				albedo.rgb = lerp(albedo.rgb, foamColor.rgb, foamTerm);
				alpha = lerp(alpha, foamColor.a, foamTerm);
				
				// Depth-based alpha
				alpha *= saturate((__depthAlphaDistance * depthDiff) + __depthAlphaMin);

				// main light: direction, color, distanceAttenuation, shadowAttenuation
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord = input.shadowCoord;
			#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
				float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
			#else
				float4 shadowCoord = float4(0, 0, 0, 0);
			#endif

			#if defined(URP_10_OR_NEWER)
				#if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
					half4 shadowMask = SAMPLE_SHADOWMASK(input.lightmapUV);
				#elif !defined (LIGHTMAP_ON)
					half4 shadowMask = unity_ProbesOcclusion;
				#else
					half4 shadowMask = half4(1, 1, 1, 1);
				#endif
				
				Light mainLight = GetMainLight(shadowCoord, positionWS, shadowMask);
			#else
				Light mainLight = GetMainLight(shadowCoord);
			#endif

				// ambient or lightmap
				// Samples SH fully per-pixel. SampleSHVertex and SampleSHPixel functions
				// are also defined in case you want to sample some terms per-vertex.
				half3 bakedGI = SampleSH(normalWS);
				half occlusion = 1;

				half3 indirectDiffuse = bakedGI;
				indirectDiffuse *= occlusion * albedo * __ambientIntensity;

				half3 lightDir = mainLight.direction;
				half3 lightColor = mainLight.color.rgb;

				half atten = mainLight.shadowAttenuation * mainLight.distanceAttenuation;

				half ndl = dot(normalWS, lightDir);
				half3 ramp;
				
				half rampThreshold = __rampThreshold;
				half rampSmooth = __rampSmoothing * 0.5;
				ndl = saturate(ndl);
				ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

				// apply attenuation
				ramp *= atten;

				half3 color = half3(0,0,0);
				half3 accumulatedRamp = ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
				half3 accumulatedColors = ramp * lightColor.rgb;

				// Additional lights loop
			#ifdef _ADDITIONAL_LIGHTS
				uint additionalLightsCount = GetAdditionalLightsCount();
				for (uint lightIndex = 0u; lightIndex < additionalLightsCount; ++lightIndex)
				{
					#if defined(URP_10_OR_NEWER)
						Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);
					#else
						Light light = GetAdditionalLight(lightIndex, positionWS);
					#endif
					half atten = light.shadowAttenuation * light.distanceAttenuation;
					half3 lightDir = light.direction;
					half3 lightColor = light.color.rgb;

					half ndl = dot(normalWS, lightDir);
					half3 ramp;
					
					ndl = saturate(ndl);
					ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

					// apply attenuation (shadowmaps & point/spot lights attenuation)
					ramp *= atten;

					accumulatedRamp += ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
					accumulatedColors += ramp * lightColor.rgb;

				}
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				color += input.vertexLights * albedo;
			#endif

				accumulatedRamp = saturate(accumulatedRamp);
				half3 shadowColor = (1 - accumulatedRamp.rgb) * __shadowColor;
				accumulatedRamp = accumulatedColors.rgb * __highlightColor + shadowColor;
				color += albedo * accumulatedRamp;

				// apply ambient
				color += indirectDiffuse;

				// Premultiply blending
				#if defined(_ALPHAPREMULTIPLY_ON)
					color.rgb *= alpha;
				#endif

				color += emission;

				return half4(color, alpha);
			}
			ENDHLSL
		}

		// Depth & Shadow Caster Passes
		HLSLINCLUDE

		#if defined(SHADOW_CASTER_PASS) || defined(DEPTH_ONLY_PASS)

			#define fixed half
			#define fixed2 half2
			#define fixed3 half3
			#define fixed4 half4

			float3 _LightDirection;

			struct Attributes
			{
				float4 vertex   : POSITION;
				float3 normal   : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float4 screenPosition : TEXCOORD1;
				float4 pack1 : TEXCOORD2; /* pack1.xy = texcoord0  pack1.zw = sinUvAnimVertexPos */
			#if defined(DEPTH_ONLY_PASS)
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			#endif
			};

			float4 GetShadowPositionHClip(Attributes input)
			{
				float3 positionWS = TransformObjectToWorld(input.vertex.xyz);
				float3 normalWS = TransformObjectToWorldNormal(input.normal);

				float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));

				#if UNITY_REVERSED_Z
					positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#else
					positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#endif

				return positionCS;
			}

			Varyings ShadowDepthPassVertex(Attributes input)
			{
				Varyings output;
				UNITY_SETUP_INSTANCE_ID(input);
				#if defined(DEPTH_ONLY_PASS)
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				#endif

				// Used for texture UV sine animation
				float2 sinUvAnimVertexPos = input.vertex.xy + input.vertex.yz;
				output.pack1.zw = sinUvAnimVertexPos;

				// Texture Coordinates
				output.pack1.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;
				// Shader Properties Sampling
				float3 __vertexDisplacementWorld = ( tex2Dlod(_WorldDisplacementTex, float4(output.pack1.xy * _WorldDisplacementTex_ST.xy + _WorldDisplacementTex_ST.zw, 0, 0)).rgb * _DisplacementStrength );
				float __wavesFrequency = ( _WavesFrequency );
				float __wavesHeight = ( _WavesHeight );
				float __wavesSpeed = ( _WavesSpeed );
				float4 __wavesSinOffsets1 = ( float4(1,2.2,0.6,1.3) );
				float4 __wavesPhaseOffsets1 = ( float4(1,1.3,2.2,0.4) );
				float4 __wavesSinOffsets2 = ( float4(0.6,1.3,3.1,2.4) );
				float4 __wavesPhaseOffsets2 = ( float4(2.2,0.4,3.3,2.9) );
				float4 __wavesSinOffsets3 = ( float4(1.4,1.8,4.2,3.6) );
				float4 __wavesPhaseOffsets3 = ( float4(0.2,2.6,0.7,3.1) );
				float4 __wavesSinOffsets4 = ( float4(1.1,2.8,1.7,4.3) );
				float4 __wavesPhaseOffsets4 = ( float4(0.5,4.8,3.1,2.3) );

				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				worldPos.xyz += __vertexDisplacementWorld;
				input.vertex.xyz = mul(unity_WorldToObject, float4(worldPos, 1)).xyz;
				
				// Vertex water waves
				float _waveFrequency = __wavesFrequency;
				float _waveHeight = __wavesHeight;
				float3 _vertexWavePos = worldPos.xyz * _waveFrequency;
				float _phase = _Time.y * __wavesSpeed;
				half4 vsw_offsets_x = __wavesSinOffsets1;
				half4 vsw_ph_offsets_x = __wavesPhaseOffsets1;
				half4 vsw_offsets_z = __wavesSinOffsets2;
				half4 vsw_ph_offsets_z = __wavesPhaseOffsets2;
				half4 vsw_offsets_x2 = __wavesSinOffsets3;
				half4 vsw_ph_offsets_x2 = __wavesPhaseOffsets3;
				half4 vsw_offsets_z2 = __wavesSinOffsets4;
				half4 vsw_ph_offsets_z2 = __wavesPhaseOffsets4;
				half4 waveX = sin((_vertexWavePos.xxxx * vsw_offsets_x) + (_phase.xxxx * vsw_ph_offsets_x));
				half4 waveZ = sin((_vertexWavePos.zzzz * vsw_offsets_z) + (_phase.xxxx * vsw_ph_offsets_z));
				half4 waveX2 = sin((_vertexWavePos.xxxx * vsw_offsets_x2) + (_phase.xxxx * vsw_ph_offsets_x2));
				half4 waveZ2 = sin((_vertexWavePos.zzzz * vsw_offsets_z2) + (_phase.xxxx * vsw_ph_offsets_z2));
				
				float waveFactorX = (dot(waveX.xyzw, 1) + dot(waveX2.xyzw, 1)) * _waveHeight / 8;
				float waveFactorZ = (dot(waveZ.xyzw, 1) + dot(waveZ2.xyzw, 1)) * _waveHeight / 8;
				input.vertex.y += (waveFactorX + waveFactorZ);
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);

				//Screen Space UV
				float4 screenPos = ComputeScreenPos(vertexInput.positionCS);
				output.screenPosition.xyzw = screenPos;

				#if defined(DEPTH_ONLY_PASS)
					output.positionCS = TransformObjectToHClip(input.vertex.xyz);
				#elif defined(SHADOW_CASTER_PASS)
					output.positionCS = GetShadowPositionHClip(input);
				#else
					output.positionCS = float4(0,0,0,0);
				#endif

				return output;
			}

			half4 ShadowDepthPassFragment(Varyings input) : SV_TARGET
			{
				#if defined(DEPTH_ONLY_PASS)
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
				#endif

				float2 uvSinAnim__BaseMap = (input.pack1.zw * _BaseMap_SinAnimParams.z) + (_Time.yy * _BaseMap_SinAnimParams.x);
				// Shader Properties Sampling
				float4 __albedo = ( tex2D(_BaseMap, input.pack1.xy + (((sin(0.9 * uvSinAnim__BaseMap + _BaseMap_SinAnimParams.w) + sin(1.33 * uvSinAnim__BaseMap + 3.14 * _BaseMap_SinAnimParams.w) + sin(2.4 * uvSinAnim__BaseMap + 5.3 * _BaseMap_SinAnimParams.w)) / 3) * _BaseMap_SinAnimParams.y)).rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __cutoff = ( _Cutoff );

				half3 albedo = __albedo.rgb;
				half alpha = __alpha;
				half3 emission = half3(0,0,0);

				//Alpha Testing
				clip(alpha - __cutoff);

				return 0;
			}

		#endif
		ENDHLSL

		Pass
		{
			Name "ShadowCaster"
			Tags
			{
				"LightMode" = "ShadowCaster"
			}

			ZWrite On
			ZTest LEqual

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile SHADOW_CASTER_PASS

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

			ENDHLSL
		}

	}

	FallBack "Hidden/InternalErrorShader"
	CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}

/* TCP_DATA u config(unity:"2020.3.1f1";ver:"2.7.1";tmplt:"SG2_Template_URP";features:list["UNITY_5_4","UNITY_5_5","UNITY_5_6","UNITY_2017_1","UNITY_2018_1","UNITY_2018_2","UNITY_2018_3","UNITY_2019_1","UNITY_2019_2","UNITY_2019_3","UNITY_2020_1","VERTEX_DISPLACEMENT_WORLD","VERTEX_SIN_WAVES","VSW_8","VSW_WORLDPOS","DEPTH_BUFFER_COLOR","DEPTH_BUFFER_FOAM","FOAM_ANIM","SMOOTH_FOAM","NO_FOAM_BACKFACE","DEPTH_BUFFER_ALPHA","TEMPLATE_LWRP","ALPHA_TESTING","AUTO_TRANSPARENT_BLENDING"];flags:list[];flags_extra:dict[];keywords:dict[RENDER_TYPE="TransparentCutout",RampTextureDrawer="[TCP2Gradient]",RampTextureLabel="Ramp Texture",SHADER_TARGET="3.0"];shaderProperties:list[sp(name:"Albedo";imps:list[imp_mp_texture(uto:True;tov:"";tov_lbl:"";gto:True;sbt:False;scr:False;scv:"";scv_lbl:"";gsc:False;roff:False;goff:False;sin_anm:True;sin_anmv:"";sin_anmv_lbl:"";gsin:False;notile:False;triplanar_local:False;def:"white";locked_uv:False;uv:0;cc:4;chan:"RGBA";mip:-1;mipprop:False;ssuv_vert:False;ssuv_obj:False;uv_type:Texcoord;uv_chan:"XZ";uv_shaderproperty:__NULL__;prop:"_BaseMap";md:"";custom:False;refs:"";guid:"56b347b4-39af-4a3e-81f5-2340f85dac9a";op:Multiply;lbl:"Albedo";gpu_inst:False;locked:False;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False)];customTextures:list[];codeInjection:codeInjection(injectedFiles:list[];mark:False);matLayers:list[]) */
/* TCP_HASH 508cce2b63af90c2f1cfa4bc835014c5 */
