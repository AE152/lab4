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
                    this.RaiseAndSetIfChanged(ref greeting, greeting + value);
                    int i = 1;
                    string x1 = "";
                    string x2 = "";
                    while (greeting[i] != '=')
                    {
                        while(greeting[i] != '+' || greeting[i] != '-' || greeting[i] != '*' || greeting[i] != '/')
                        {
                            x1 += greeting[i];
                            i++;
                        }
                        x2 += greeting[i];
                        i++;
                    }
                    RomanNumber x11 = new RomanNumberExtend(x1);
                    RomanNumber x22 = new RomanNumberExtend(x2);
                    string result = (x11 + x22).ToString();
                }

            }
            get
            {
                return greeting;
            }
        }
        public ReactiveCommand<string, string> OnClickCommand { get; }
    }
}
