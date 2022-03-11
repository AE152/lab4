using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using lab4.Models;


namespace lab4.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
       public string greeting = "";

        
        public MainWindowViewModel()
        {
            OnClickCommand = ReactiveCommand.Create<string, string>((str) => Greeting = str);
        }

        public string Greeting
        {
            set
            {
                if (value != "=")
                    this.RaiseAndSetIfChanged(ref greeting, greeting + value);
                else
                {
                    this.RaiseAndSetIfChanged(ref greeting, greeting + "=" + Calc(greeting));
                    greeting = "";//отчишаем строку для следующего выражения                    
                }

            }
            get
            {
                return greeting;
            }
        }

        private string Obr_polsk_zapis(string virazhenie)
        {
            string polsk_zap = "";
            Stack<string> operations = new Stack<string>();
            for (int i = 0; i < virazhenie.Length; i++)
            {
                switch (virazhenie[i])
                {
                    case '+':
                        operations.Push("+");
                        polsk_zap += " ";
                        break;
                    case '-':
                        operations.Push("-");
                        polsk_zap += " ";
                        break;
                    case '*':
                        operations.Push("*");
                        polsk_zap += " ";
                        break;
                    case '/':
                        operations.Push("/");
                        polsk_zap += " ";
                        break;
                    default:
                        polsk_zap += virazhenie[i];
                        break;
                }
            }
            foreach (string operation in operations)
            {
                polsk_zap += " ";
                polsk_zap += operation;
            }
            return polsk_zap;
        }

        //Стековый калькулятор, работает через обратную польскую запись, приоритеты не реализованы, корректно работает с двумя цифрами
        private string Calc(string virazhenie)
        {
            string polsk_zap = Obr_polsk_zapis(virazhenie);
            Stack<string> otvet = new Stack<string>();
            for(int j = 0; j < polsk_zap.Length; j++)
            {
                if (polsk_zap[j] == '+' || polsk_zap[j] == '-' || polsk_zap[j] == '*' || polsk_zap[j] == '/')
                {
                    string num1 = "";
                    string num2 = "";
                    otvet.Pop();//проходим пробел
                    while(otvet.Peek() != " ")
                    {
                        num1 += otvet.Pop();
                    }
                    otvet.Pop();//проходим пробел
                    while ( (otvet.Count != 0) && (otvet.Peek() != " ") )
                    {
                        
                        num2 += otvet.Pop();
                    }
                    string num1_reverse = "";
                    for(int k = num1.Length-1; k >= 0; k--)
                    {
                        num1_reverse += num1[k]; 
                    }
                    string num2_reverse = "";
                    for (int k = num2.Length - 1; k >= 0; k--)
                    {
                        num2_reverse += num2[k];
                    }
                    RomanNumber num2_Rom = new RomanNumberExtend(num1_reverse);
                    RomanNumber num1_Rom = new RomanNumberExtend(num2_reverse);
                    switch (polsk_zap[j])
                    {
                        case '+':
                            otvet.Push((num1_Rom + num2_Rom).ToString());
                            break;
                        case '-':
                            otvet.Push((num1_Rom - num2_Rom).ToString());
                            break;
                        case '*':
                            otvet.Push((num1_Rom * num2_Rom).ToString());
                            break;
                        case '/':
                            otvet.Push((num1_Rom / num2_Rom).ToString());
                            break;

                    }
                }
                else
                    if(polsk_zap[j] == ' ')
                    {
                        otvet.Push(" ");
                    }
                    else
                        otvet.Push(polsk_zap.Substring(j,1));
            }
            return otvet.Pop(); 
        }
        public ReactiveCommand<string, string> OnClickCommand { get; }
    }
}
