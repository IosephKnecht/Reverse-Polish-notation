using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ОбратнаяПольскаяНотация
{
    class Calcul
    {
        Matrix matrix;
        string input;
        string rpn = null;
        bool work_end=true;

        Stack<double> digits = new Stack<double>();
        Stack<char> operators = new Stack<char>();

        public event Action<double, string> EndProcedure;
        public event Action Error;

        char[] active_sym = new char[] { '+', '-', '*', '/' };

        public Calcul(string input)
        {
            this.matrix = new Matrix(Q1,Q2,Q3,Q4,error,end);
            this.input = input;
        }

        private void Q1(char input_sym)
        {
            if ((operators.Peek() == '_' && digits.Count == 0 && active_sym.Contains(input_sym))||
                (!(operators.Count>=digits.Count)))
            {
                error(input_sym);
            }
            else
            {
                operators.Push(input_sym);
            }
        }

        private void Q2(char input_sym)
        {
            double result;
            Calculate(out result);
            operators.Push(input_sym);
            digits.Push(result);
        }

        private void Q3(char input_sym)
        {
            operators.Pop();
            operators.Pop();
        }

        private void Q4(char input_sym)
        {
            while ((operators.Count >= 1 || digits.Count >= 2) && (operators.Peek() != '('
                &&operators.Peek()!=')'&&operators.Peek()!='_'))
            {
                double result;
                Calculate(out result);
                digits.Push(result);
            }

            if (operators.Count > 0 && operators.Peek() == '(') operators.Pop();
            if (input_sym == '+' || input_sym == '-' || input_sym == '*' || input_sym == '/') operators.Push(input_sym);
        }

        private void error(char input_sym)
        {
            work_end = false;
            throw new Exception();
        }

        private void end(char input_sym)
        {
            EndProcedure(digits.Pop(), rpn);
            work_end = false;
        }

        private void Calculate(out double result)
        {
            double digit2=digits.Pop();

            double digit1=digits.Pop();

            char stack_sym = operators.Pop();

            rpn += stack_sym.ToString();

            switch (stack_sym)
            {
                case '+':
                    result= digit1 + digit2;
                    break;

                case '-':
                    result= digit1 - digit2;
                    break;

                case '*':
                    result= digit1 * digit2;
                    break;

                case '/':
                    result= digit1 / digit2;
                    break;
                default:
                    result = 0;
                    throw new Exception();
                    break;
            }
        }

        public void PolandNotationCalcul()
        {
            operators.Clear();
            operators.Push('_');

            string push_digit=null;

            while (work_end&&input != null)
            {
                int digit;

                if (input == "") input = "_";

                bool work = int.TryParse(input[0].ToString(),out digit);

                if (work)
                {
                    push_digit += digit;
                }
                else
                {
                    if (push_digit != null)
                    {
                        digits.Push(Convert.ToDouble(push_digit));
                        rpn += push_digit + " ";
                    }
                    push_digit = null;

                    char input_sym = input[0];

                    try
                    {
                        matrix.Trans(operators.Peek(), input_sym);
                    }
                    catch
                    {
                        Error();
                        break;
                    }
                }

                input = input.Remove(0, 1);
            }
        }
    }
}
