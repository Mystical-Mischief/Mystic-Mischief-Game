Shader "Unlit/Normal_GooseGame"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "white" {}
        _LightInt("Light Intensity", Range(0,1)) = 1
        _ShadowThreshold("Shadow Threshold", Range(-1,1)) = 0.2
        _ShadowIntensity("Shadow Color Intensity", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
       {
        Name "Shadow Caster"
        Tags{"RenderType"="Opaque" "LightMode"="ShadowCaster"}
        ZWrite On

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #pragma multi_compile_shadowcaster
        
        #include "UnityCg.cginc"


        
        struct v2f
        {
            V2F_SHADOW_CASTER;
        };
        v2f vert(appdata_full v)
        {
            v2f o;
            TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
            return o;
        }
        fixed4 frag(v2f i) : SV_Target
        {
            SHADOW_CASTER_FRAGMENT(i)
        }
        ENDCG
       }

        Pass
        {
            Name "Main Pass"
            Tags{"LightMode"="ForwardBase"}

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
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv_normal : TEXCOORD3;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal_world : TEXTCOORD1;
                float4 tangent_world : TEXCOORD4;
                float3 binormal_world : TEXCOORD5;
                float4 shadowCoord : TEXTCOORD2;
                


            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _ShadowMapTexture;

            sampler2D _NormalMap;
            float4 _NormalMap_ST;
            
            float _LightInt;
            float4 _LightColor0;
            float _ShadowThreshold;
            float _ShadowIntensity;

            float4 NDC(float4 pos)
            {
                float4 o = pos * 0.5f;
                #if defined(UNITY_HALF_TEXTEL_OFFSET)
                o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w * _ScreenParams.zw;
                #else
                o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
                #endif

                o.zw = pos.zw;
                return o;


            }

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.uv_normal = TRANSFORM_TEX(v.uv, _NormalMap);

                o.normal_world = normalize(mul(unity_ObjectToWorld, float4(v.normal, 0))).xyz;
                
                o.tangent_world = normalize(mul(v.tangent, unity_WorldToObject));

                o.binormal_world = normalize(cross(o.normal_world, o.tangent_world) * v.tangent.w);

                UNITY_TRANSFER_FOG(o,o.vertex);
                o.shadowCoord = NDC(o.vertex);
                return o;
            }
            //This function will be for DXT compression
            float3 DXTCompression(float4 normalMap)
            {
                #if defined(UNITY_NO_DXT5nm)
                return normalMap.rgb * 2 - 1;
                #else
                float3 normalCol;
                normalCol = float3 (normalMap.a * 2 - 1, normalMap.g * 2 - 1, 0);

                normalCol.b = sqrt(1 - saturate(dot(normalCol.xy, normalCol.xy)));
                return normalCol;
                #endif
            }

            float3 LambertShading
            (
             float3 colorRefl,
             float lightInt,
             float3 normal,
             float3 lightDir
            )
            {
                return colorRefl * lightInt * max(_ShadowIntensity , step(_ShadowThreshold, dot(normal, lightDir)));

            }
            


            fixed4 frag(v2f i) : SV_Target
            {
            float2 uv = i.shadowCoord.xy / i.shadowCoord.w;


            fixed4 normal_map = tex2D(_NormalMap, i.uv_normal);
            fixed3 normal_compressed = DXTCompression(normal_map);

            float3x3 TBN_matrix = float3x3
                    (
                        i.tangent_world.xyz,
                        i.binormal_world,
                        i.normal_world


                    );

            float3 normal_color = normalize(mul(normal_compressed, TBN_matrix));
            



            float3 normal = i.normal_world;
             // sample the texture
             fixed4 col = tex2D(_MainTex, i.uv);
             fixed shadow = tex2D(_ShadowMapTexture, uv).a;
            //This will be our lightdirection
            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
            //This will be our light Color
            fixed3 colorRefl = _LightColor0.rgb;
            //This is the function for LambertShading
            half3 diffuse = LambertShading( colorRefl , _LightInt, normal, lightDir);

            col.rgb *= diffuse * shadow;
            col.rgb *= dot(normal_color,lightDir);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
