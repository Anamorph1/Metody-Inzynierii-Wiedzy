using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGDSDatabase.Models.AGDS;
using AGDSDatabase.Models;

namespace AGDSDatabase
{
    class Util
    {
        public static List<Token> TransformToPolishNotation(List<Token> inputTokenList)
        {
            Queue<Token> outputQueue = new Queue<Token>();
            Stack<Token> stack = new Stack<Token>();

            int index = inputTokenList.Count - 1;
            while (index >= 0)
            {
                Token t = inputTokenList[index];

                switch (t.type)
                {
                    case Token.TokenType.LITERAL:
                        outputQueue.Enqueue(t);
                        break;
                    case Token.TokenType.BINARY_OP:
                    case Token.TokenType.CLOSE_PAREN:
                        stack.Push(t);
                        break;
                    case Token.TokenType.OPEN_PAREN:
                        while (stack.Peek().type != Token.TokenType.CLOSE_PAREN)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        stack.Pop();
                        while (stack.Count > 0)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        break;
                    default:
                        break;
                }
                --index;
            }
            while (stack.Count > 0)
            {
                outputQueue.Enqueue(stack.Pop());
            }

            return outputQueue.Reverse().ToList();
        }


        
        public static BoolExpr Make(ref List<Token>.Enumerator polishNotationTokensEnumerator, AGDSModel agdsModel)
        {
            if (polishNotationTokensEnumerator.Current.type == Token.TokenType.LITERAL)
            {
                BoolExpr lit = BoolExpr.CreateBoolVar(agdsModel.GetItemsByExpr(polishNotationTokensEnumerator.Current.value));
                polishNotationTokensEnumerator.MoveNext();
                return lit;
            }
            else
            {
                if (polishNotationTokensEnumerator.Current.value == "AND")
                {
                    polishNotationTokensEnumerator.MoveNext();
                    BoolExpr left = Make(ref polishNotationTokensEnumerator,agdsModel);
                    BoolExpr right = Make(ref polishNotationTokensEnumerator,agdsModel);
                    return BoolExpr.CreateAnd(left, right);
                }
                else if (polishNotationTokensEnumerator.Current.value == "OR")
                {
                    polishNotationTokensEnumerator.MoveNext();
                    BoolExpr left = Make(ref polishNotationTokensEnumerator,agdsModel);
                    BoolExpr right = Make(ref polishNotationTokensEnumerator,agdsModel);
                    return BoolExpr.CreateOr(left, right);
                }
            }
            return null;
        }

        public static List<Item> Eval(BoolExpr expr)
        {
            if (expr.IsLeaf())
            {
                return expr.Lit;
            }

            if (expr.Op == BoolExpr.BOP.OR)
            {
                return Eval(expr.Left).Union(Eval(expr.Right)).ToList();
            }

            if (expr.Op == BoolExpr.BOP.AND)
            {
                return Eval(expr.Left).Intersect(Eval(expr.Right)).ToList();
            }
            throw new ArgumentException();
        }
    }
}
