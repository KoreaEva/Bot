using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Bot.Builder.FormFlow;

namespace GreatWall
{
    public enum FoodOptions { 자장면, 짬뽕, 탕수육, 기스면, 란자완스 };
    public enum LengthOptions { 보통, 곱배기 };

    [Serializable]
    public class FoodOrder
    {
        public FoodOptions? Food;
        public LengthOptions? Length;

        public static IForm<FoodOrder> BuildForm()
        {
            return new FormBuilder<FoodOrder>()
                    .Message("만리장성에 오신 여러분을 환영합니다.")
                    .Build();
        }
    }
}