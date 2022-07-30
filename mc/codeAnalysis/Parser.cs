using System.Collections.Generic;
using Minsk.Analysis.Syntax;


namespace Minsk.Analysis
{
     class Parser
     {

          private readonly SyntaxToken[] _tokens;
          private int _position;
          private List<string> _diagnostics = new List<string>();
          public IEnumerable<string> Diagnostics => _diagnostics;
          private SyntaxToken NextToken()
          {
               var current = Current;
               _position++;
               return current;
          }

          private SyntaxToken Match(SyntaxKind kind)
          {
               if (Current.Kind == kind)
               {
                    return NextToken();
               }
               _diagnostics.Add($"ERROR: Unexpected Token <{Current.Kind}> expected <{kind}>");
               return new SyntaxToken(kind, Current.Position, null, null);
          }

          public Parser(string text)
          {

               var lexer = new Lexer(text);
               var tokens = new List<SyntaxToken>();

               SyntaxToken token;

               do
               {
                    token = lexer.NextToken();
                    if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadToken)
                    {
                         tokens.Add(token);
                    }
               } while (token.Kind != SyntaxKind.EndOfFileToken);
               _tokens = tokens.ToArray();
               _diagnostics.AddRange(lexer.Diagnostics);
          }

          private SyntaxToken Peek(int offset)
          {
               int index = _position + offset;

               if (index >= _tokens.Length)
               {
                    return _tokens[_tokens.Length - 1];
               }
               return _tokens[index];
          }

          private SyntaxToken Current => Peek(0);

          public SyntaxTree Parse()
          {
               var expression = ParseTerm();
               var endofFileToken = Match(SyntaxKind.EndOfFileToken);
               return new SyntaxTree(_diagnostics, expression, endofFileToken);
          }

          private ExpressionSyntax ParseExpression()
          {
               return ParseTerm();
          }

          private ExpressionSyntax ParseTerm()
          {
               var left = ParseFactor();

               while (Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken)
               {
                    var operatorToken = NextToken();
                    var right = ParseFactor();
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
               }
               return left;
          }

          private ExpressionSyntax ParseFactor()
          {
               var left = ParsePrimaryExpression();

               while (Current.Kind == SyntaxKind.StarToken || Current.Kind == SyntaxKind.ForwordSlashToken)
               {
                    var operatorToken = NextToken();
                    var right = ParsePrimaryExpression();
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
               }
               return left;
          }

          private ExpressionSyntax ParsePrimaryExpression()
          {
               if (Current.Kind == SyntaxKind.OpenParenthesisToken)
               {
                    var left = NextToken();
                    var expression = ParseExpression();
                    var right = Match(SyntaxKind.CloseParenthesisToken);
                    return new ParenthesisExpressionSyntax(left, expression, right);
               }

               var numbertoken = Match(SyntaxKind.NumberToken);

               return new LiteralExpressionSyntex(numbertoken);

          }
     }
}