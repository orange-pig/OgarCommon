using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgarCommon
{
    public class RaceHleper
    {
        public static string[] Races = new string[56] 
        {"汉","维吾尔","柯尔克孜","塔吉克","回","满","壮","蒙古",
         "土家","哈萨克","鄂温克","鄂伦春","侗","藏","乌孜别克",
         "俄罗斯","塔塔尔","苗","彝","布依","朝鲜","瑶","白",
         "哈尼","傣","黎","僳僳","佤","畲","拉祜","水","东乡",
         "纳西","景颇","土","达斡尔","仫佬","仡佬","羌","锡伯",
         "布朗","撒拉","毛南","阿昌","普米","怒","德昂","保安",
         "裕固","京","基诺","高山","独龙","赫哲","门巴","珞巴"
        };

        /// <summary>
        /// 返回民族的编号
        /// </summary>
        /// <param name="raceName"></param>
        /// <returns>成功:0-55, 失败: 255</returns>
        public static int GetRaceIndex(string raceName)
        {
            for (int i = 0; i < 56; i++)
            {
                if (Races[i].Equals(raceName))
                {
                    return i;
                }
            }

            return 255;
        }
    }
}
