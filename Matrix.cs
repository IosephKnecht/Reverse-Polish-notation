using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ОбратнаяПольскаяНотация
{
    class Matrix
    {
        char stack_sym;
        char input_sym;

        char[] S = { '+', '-', '*', '/' ,'(','_'};
        char[] Sigma = { '+', '-', '*', '/', '(', ')', '_' };

        Q Q1, Q2, Q3, Q4, error, end;

        public Matrix(Q Q1, Q Q2, Q Q3, Q Q4, Q error, Q end)
        {
            this.Q1 = Q1;
            this.Q2 = Q2;
            this.Q3 = Q3;
            this.Q4 = Q4;
            this.error = error;
            this.end = end;

            trans_matrix = new Q[,]
            {
                { Q2,Q2,Q1,Q1,Q1,Q4,Q4 },
                {Q2,Q2,Q1,Q1,Q1,Q4,Q4 },
                {Q4,Q4,Q2,Q2,Q1,Q4,Q4 },
                {Q4,Q4,Q2,Q2,Q1,Q4,Q4 },
                {Q1,Q1,Q1,Q1,Q1,Q3,error },
                {Q1,Q1,Q1,Q1,Q1,error,end  },

            };
        }

        Q[,] trans_matrix;

        public void Trans(char stack_sym, char input_sym)
        {
            int i = S.ToList().IndexOf(stack_sym);
            int j= Sigma.ToList().IndexOf(input_sym);
            trans_matrix[i, j](input_sym);
        }
    }
}
