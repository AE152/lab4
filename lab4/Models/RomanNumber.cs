using System;

namespace lab4.Models
{
    public class RomanNumber : ICloneable, IComparable
    {
        private string Rom_num;
        public RomanNumber(ushort n)
        {
            if ((n <= 0) || (n > 3999))
                throw new RomanNumberException("В конструктор подается надепустимое значение!");
            Rom_num = convert_to_roman(n);
        }
        private static string convert_to_roman(int n)
        {
            int[] arabic = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int i = 0;
            string new_Rom_num = "";
            while (n > 0)
            {
                if (n >= arabic[i])
                {
                    n -= arabic[i];
                    new_Rom_num += roman[i];
                }
                else
                    i++;
            }
            return new_Rom_num;
        }
        private static int convert_to_arabic(string n)
        {
            int[] arabic = { 1000, 500, 100, 50, 10, 5, 1 };
            string[] roman = { "M", "D", "C", "L", "X", "V", "I" };
            int new_Arab_num = 0;
            int temp_prev = 0;
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
        //Сложение римских чисел
        public static RomanNumber operator +(RomanNumber? n1, RomanNumber? n2)
        {
            return new RomanNumber((ushort)(convert_to_arabic(n1.Rom_num) + convert_to_arabic(n2.Rom_num)));
        }
        //Вычитание римских чисел
        public static RomanNumber operator -(RomanNumber? n1, RomanNumber? n2)
        {
            int n_1 = convert_to_arabic(n1.Rom_num);
            int n_2 = convert_to_arabic(n2.Rom_num);
            if (n_1 <= n_2)
            {
                throw new RomanNumberException("Ошибка при вычитании!");
            }
            return new RomanNumber((ushort)(n_1 - n_2));
        }

        //Умножение римских чисел
        public static RomanNumber operator *(RomanNumber? n1, RomanNumber? n2)
        {
            int n_1 = convert_to_arabic(n1.Rom_num);
            int n_2 = convert_to_arabic(n2.Rom_num);
            return new RomanNumber((ushort)(n_1 * n_2));
        }

        //Целочисленное деление римских чисел
        public static RomanNumber operator /(RomanNumber? n1, RomanNumber? n2)
        {
            int n_1 = convert_to_arabic(n1.Rom_num);
            int n_2 = convert_to_arabic(n2.Rom_num);
            if ((n_1 % n_2 != 0))
            {
                throw new RomanNumberException("Ошибка при делении!");
            }
            return new RomanNumber((ushort)(n_1 / n_2));
        }

        //Возвращает строковое представление римского числа
        public override string ToString()
        {
            return Rom_num;
        }
        //Реализация интерфейса IClonable
        public object Clone()
        {
            int tmp = convert_to_arabic(Rom_num);
            return new RomanNumber((ushort)tmp);
            //throw new NotImplementedException();
        }
        //Реализация интерфейса IComparable
        public int CompareTo(object? obj)
        {
            if (obj is null)
                throw new RomanNumberException("Ошибка в CompareTo!");
            int num_obj = convert_to_arabic(obj.ToString());
            int Rom_num_arabic = convert_to_arabic(Rom_num);
            return Rom_num_arabic - num_obj;
        }

    }
}
