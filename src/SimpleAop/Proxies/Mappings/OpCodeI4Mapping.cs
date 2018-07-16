using System;
using System.Reflection.Emit;

namespace SimpleAop.Proxies.Mappings
{
    public class OpCodeI4Mapping : LazyMapping<int, OpCode>
    {
        protected override void InitializeMapping()
        {
            // 범위 안에 있을 경우
            this.Map(o => o >= -1 && o <= 8 && o == 0).Return(o => OpCodes.Ldc_I4_0)
                .Map(o => o >= -1 && o <= 8 && o == 1).Return(o => OpCodes.Ldc_I4_1)
                .Map(o => o >= -1 && o <= 8 && o == 2).Return(o => OpCodes.Ldc_I4_2)
                .Map(o => o >= -1 && o <= 8 && o == 3).Return(o => OpCodes.Ldc_I4_3)
                .Map(o => o >= -1 && o <= 8 && o == 4).Return(o => OpCodes.Ldc_I4_4)
                .Map(o => o >= -1 && o <= 8 && o == 5).Return(o => OpCodes.Ldc_I4_5)
                .Map(o => o >= -1 && o <= 8 && o == 6).Return(o => OpCodes.Ldc_I4_6)
                .Map(o => o >= -1 && o <= 8 && o == 7).Return(o => OpCodes.Ldc_I4_7)
                .Map(o => o >= -1 && o <= 8 && o == 8).Return(o => OpCodes.Ldc_I4_8)
                .Map(o => o >= -1 && o <= 8 && o == -1).Return(o => OpCodes.Ldc_I4_M1)

                // SByte 범위 안에 있을 경우
                .Map(o => o >= SByte.MinValue && o <= SByte.MaxValue).Return(o => OpCodes.Ldc_I4_S)
				
                // Else
                .MapDefault().Return(o => OpCodes.Ldc_I4);
        }
    }
}