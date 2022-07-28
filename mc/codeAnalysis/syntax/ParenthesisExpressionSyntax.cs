using System.Collections.Generic;

namespace Minsk.Analysis.Syntax
{
     sealed class ParenthesisExpressionSyntax : ExpressionSyntax
     {

          public ParenthesisExpressionSyntax(SyntaxToken openParenthesiseToken, ExpressionSyntax expression, SyntaxToken closeParenthesiseToken)
          {
               OpenParenthesiseToken = openParenthesiseToken;
               Expression = expression;
               CloseParenthesiseToken = closeParenthesiseToken;
          }

          public override SyntaxKind Kind => SyntaxKind.ParenthesisExpression;

          public SyntaxToken OpenParenthesiseToken { get; }
          public ExpressionSyntax Expression { get; }
          public SyntaxToken CloseParenthesiseToken { get; }


          public override IEnumerable<SyntaxNode> GetChildren()
          {
               yield return OpenParenthesiseToken;
               yield return Expression;
               yield return CloseParenthesiseToken;

          }
     }
}