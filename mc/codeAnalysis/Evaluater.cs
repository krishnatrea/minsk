using System;
using Minsk.Analysis.Syntax;

namespace Minsk.Analysis
{
     class Evaluater
     {
          private readonly ExpressionSyntax _root;

          public Evaluater(ExpressionSyntax root)
          {
               _root = root;
          }

          public int Evaluator()
          {
               return EvaluateExpression(_root);
          }

          private int EvaluateExpression(ExpressionSyntax node)
          {

               if (node is LiteralExpressionSyntex n)
               {
                    return (int)n.LiteralToken.Value;
               }

               if (node is BinaryExpressionSyntax b)
               {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

                    if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
                    {
                         return left + right;
                    }
                    else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
                    {
                         return left - right;
                    }
                    else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
                    {
                         return left * right;
                    }
                    else if (b.OperatorToken.Kind == SyntaxKind.ForwordSlashToken)
                    {
                         // if(right == 0){
                         // }
                         return left / right;
                    }
                    else
                         throw new Exception($"Unexpected Binary Operator {b.OperatorToken.Kind} ");
               }

               if (node is ParenthesisExpressionSyntax p)
               {
                    return EvaluateExpression(p.Expression);
               }

               throw new Exception($"Unexpected Node {node.Kind}");
          }
     }
}