using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public static class MathHelper
    {
        /// <summary>
        /// 计算中位数
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns></returns>
        public static double Median(double[] arr)
        {
            //为了不修改arr值，对数组的计算和修改在tempArr数组中进行
            double[] tempArr = new double[arr.Length];
            arr.CopyTo(tempArr, 0);

            //对数组进行排序
            double temp;
            for (int i = 0; i < tempArr.Length; i++)
            {
                for (int j = i; j < tempArr.Length; j++)
                {
                    if (tempArr[i] > tempArr[j])
                    {
                        temp = tempArr[i];
                        tempArr[i] = tempArr[j];
                        tempArr[j] = temp;
                    }
                }
            }

            //针对数组元素的奇偶分类讨论
            if (tempArr.Length % 2 != 0)
            {
                if (arr.Length > 1)
                {
                    return tempArr[arr.Length / 2 + 1];
                }
                else
                {
                    return tempArr[0];
                }
            }
            else
            {
                if (tempArr.Length > 2)
                {
                    return (tempArr[tempArr.Length / 2] +
                        tempArr[tempArr.Length / 2 + 1]) / 2.0;
                }
                else
                {
                    return tempArr[tempArr.Length / 2];
                }
            }
        }
    }
}
