using System.Collections.Generic;

namespace Minsk.Analysis.Syntax
{
     class LiteralExpressionSyntex : ExpressionSyntax
     {

          public LiteralExpressionSyntex(SyntaxToken literalToken)
          {
               LiteralToken = literalToken;
          }

          public SyntaxToken LiteralToken { get; }

          public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

          public override IEnumerable<SyntaxNode> GetChildren()
          {
               yield return LiteralToken;
          }

     }
}