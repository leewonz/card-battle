Shader "ImageEffect/PatternDither"
{
    Properties
    {
        _MainTex ("Screen", 2D) = "white" {}
        _Pattern ("Pattern", 2D) = "white" {}
        [PowerSlider(2.0)] _Levels ("Levels", Range(1.0, 32.0)) = 4
        _PixelSize ("Pixel Size", Range(1.0, 8.0)) = 1
        [Toggle] _Pixelize ("Pixelize Screen", int) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex, _Pattern;
            float _Levels, _PixelSize;
            int _Pixelize;

            float4 _Pattern_ST;
            float4 _Pattern_TexelSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 pattern_uv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.pattern_uv = TRANSFORM_TEX(v.uv, _Pattern); //pattern의 타일링, 오프셋 적용
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv_screen;
                float2 uv_pat = i.pattern_uv * _ScreenParams.xy * _Pattern_TexelSize.xy / _PixelSize;

                if(_Pixelize)
                {
                    float2 steps = _ScreenParams.xy / _PixelSize;
                    uv_screen = floor(i.uv * steps) / steps; 
                }
                else
                {
                    uv_screen = i.uv;
                }

                fixed4 col_origin = tex2D(_MainTex, uv_screen);
                fixed4 col_pat = tex2D(_Pattern, uv_pat);
                fixed4 col_res = fixed4(0, 0, 0, 0);

                col_res.rgb = floor(col_origin.rgb * _Levels + col_pat.rgb) / _Levels;

                return col_res;
            }
            ENDCG
        }
    }
}
