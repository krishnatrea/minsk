using System.Collections.Generic;

namespace Minsk.Analysis.Syntax
{
     class NumberExpressionSyntax : ExpressionSyntax
     {

          public NumberExpressionSyntax(SyntaxToken numbertoken)
          {
               NumberToken = numbertoken;
          }

          public SyntaxToken NumberToken { get; }

          public override SyntaxKind Kind => SyntaxKind.NumberExpression;

          public override IEnumerable<SyntaxNode> GetChildren()
          {
               yield return NumberToken;
          }

     }
}