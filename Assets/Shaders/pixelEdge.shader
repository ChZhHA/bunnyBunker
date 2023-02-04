Shader "Shader_Effect/S_PixelPerfect"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Enable ("Enable", Int) = 0
        _Color ("Tint", Color) = (1,1,1,1)
        _PixelNum ("像素数", Integer) = 128
        _Rotate ("旋转", Float) = 0
        _Temp ("temp",Range(0,1)) = 0.5
        _Temp2 ("temp2",Range(0,1)) = 0.5
        _EdgeColor ("描边颜色",Color) = (0,0,0,1)
        _EdgeSize ("边缘大小",Float) = 0.1

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            struct appdata
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }
            


            float _Rotate;
            float _Temp;
            float _Temp2;
            int _PixelNum;
            float _EdgeSize;
            float4 _EdgeColor;
            int _Enable;
            fixed4 frag (v2f i) : SV_Target
            {
                // return tex2D(_MainTex, i.texcoord);
                float deg=_Rotate / 180 * 3.1415926;
                
                float2x2 mat= float2x2(
                cos(deg),  sin(deg), 
                -sin(deg), cos(deg) 
                );
                
                float2 nuv = mul(i.texcoord - float2(_Temp,_Temp2),mat) + float2(_Temp,_Temp2);
                
                float delta = _EdgeSize / _MainTex_TexelSize.z;
                float4 colSam1=tex2D(_MainTex, nuv + float2(-delta,-delta));
                float4 colSam2=tex2D(_MainTex, nuv + float2(-delta,0));
                float4 colSam3=tex2D(_MainTex, nuv + float2(-delta,delta));
                float4 colSam4=tex2D(_MainTex, nuv + float2(0,-delta));
                float4 colSam5=tex2D(_MainTex, nuv + float2(0,0));
                float4 colSam6=tex2D(_MainTex, nuv + float2(0,delta));
                float4 colSam7=tex2D(_MainTex, nuv + float2(delta,-delta));
                float4 colSam8=tex2D(_MainTex, nuv + float2(delta,0));
                float4 colSam9=tex2D(_MainTex, nuv + float2(delta,delta));        
                
                fixed4 col1 = tex2D(_MainTex, nuv);
                float4 colSamp = (colSam1+colSam2+colSam3+colSam4+colSam5+colSam6+colSam7+colSam8+colSam9+col1) / 10.0;

                float4 col;

                col.a= _Enable ? step(0.1,colSamp.a)* step(0,_EdgeSize-0.001) + (1-step(0,_EdgeSize-0.001))*step(0.5,col1.a) : col1.a;
                float flag = step(col1.a, colSamp.a) * step(col1.a, 0.1);
                col.rgb= flag * _EdgeColor.rgb+ (1- flag) *col1.rgb * i.color;

                // col.rgb=step(col1.a,col2.a) * _EdgeColor.rgb+step(col2.a,col1.a)*col1.rgb;
                

                // float dis1 = distance(col,_BaseColor);
                // float dis2 = distance(col,_BaseColor2);
                // float4 col2 = step(dis1,dis2) * _BaseColor + (1 - step(dis1,dis2)) * _BaseColor2;
                // float3 col2 = step(dis1,dis2) * ( _BaseColor.xyz - col.xyz ) / dis1 * pow(dis1,3) + col.xyz
                // + (1 - step(dis1,dis2)) * ( _BaseColor2.xyz - col.xyz ) / dis1 * pow(dis2,3) + col.xyz;

                // col.rgb=col2.rgb;
                // col.a = step(0.5,col.a);
                // col=float4(nuv,1,1);
                return col;
            }
            ENDCG
        }
    }
}
