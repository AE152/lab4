using System;

namespace lab4.Models
{
    public class RomanNumberExtend : RomanNumber
    {
        public RomanNumberExtend(string num) 
            :base(conv_to_arab(num))
        {
        }

        private static ushort conv_to_arab(string n)
        {
            ushort[] arabic = { 1000, 500, 100, 50, 10, 5, 1 };
            string[] roman = { "M", "D", "C", "L", "X", "V", "I" };
            ushort new_Arab_num = 0;
            ushort temp_prev = 0;
            int j;

            for (int i = n.Length - 1; i >= 0; i--)
            {
                for (j = 0; j < roman.Length; j++)
                    if (n[i] == roman[j][0])
                        break;
                if (temp_prev > arabic[j])
                    new_Arab_num -= arabic[j];
                else
                    new_Arab_num += arabic[j];
                temp_prev = arabic[j];
            }

            return new_Arab_num;
        }
    }
}
