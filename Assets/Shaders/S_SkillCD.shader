Shader "Shader_UI/S_SkillCD"
{
  Properties
  {
    _Progress ("进度", Range(0.0,1.0)) = 0
    _Bum ("加深",Range(0.0,1.0)) = 0.2
  }
  SubShader
  {
    Tags{
      "Queue" = "AlphaTest"
      "IgnoreProjector" = "True"
      "RenderType" = "Transparent"
    }
    // No culling or depth
    Cull Off ZWrite Off ZTest Always
    Blend SrcAlpha OneMinusSrcAlpha
    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f
      {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
      };

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

      sampler2D _MainTex;
      float _Progress;
      float _Bum;

      fixed4 frag (v2f i) : SV_Target
      {
        fixed4 col = tex2D(_MainTex, i.uv);
        col.a -= step( 1 - i.uv.y, 1 - _Progress ) * _Bum;
        col.rgb = max(col.rgb,float3(0,0,0));
        return col;
      }
      ENDCG
    }
  }
}
