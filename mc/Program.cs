using System;

namespace mc
{
     public class Program
     {
          public static void Main(string[] args)
          {
               Console.WriteLine("> ");
               var line  = Console.ReadLine();
               if (string.IsNullOrWhiteSpace(line))
               return;

               var lexer = new Lexer(line);
               
               while(true) {

                    var token = lexer.NextToken();
                    if(token.Kind == SyntaxKind.EndOfFileToken)
                    break;
                    
                    Console.Write($"{token.Kind}: '{token.Text}' : '{token.Position}' ");

                    if(token.Value != null) {
                         Console.Write($"{token.Value}");
                    }

                    Console.WriteLine();
               }

          }
     }

     abstract class SyntaxNode {
          public abstract SyntaxKind Kind {get;}
     }

     class ExpressionSyntax : SyntaxNode {

     }

     class NumberSyntax : ExpressionSyntax {
          public NumberSyntax(SyntaxToken numbertoken)
          {
          }
     }

     enum SyntaxKind {
          NumberToken, 
          WhiteSpaceToken,
          PlusToken,
          MinusToken,
          StarToken,
          ForwordSlashToken,
          OpenParenthesisToken,
          CloseParenthesisToken,
          BadToken,
          EndOfFileToken,
      }

     class SyntaxToken {
          
          public SyntaxToken(SyntaxKind kind, int position, string text, Object value ){
               Kind = kind;
               Position = position;
               Text = text;
               Value = value;

          }
          public SyntaxKind Kind {get;}
          public int Position {get;}
          public string Text {get;}

          public Object Value {get;}
     }

     class Lexer
     {
          private readonly string _text;
          private int _position;

          public Lexer(string text) {

               _text = text;

          }
          
          private void  Next () {
               _position ++ ;
          }
          private char Current {
               get {
                    if(_position >= _text.Length){
                         return '\0';
                    }

                    return _text[_position];
               }
          }

          public SyntaxToken NextToken() {
               // +, - , *, %
               // <number> <whiteSpace>
               if(_position >= _text.Length){
                    return new SyntaxToken(SyntaxKind.EndOfFileToken, _position++,"\0", null);
               }
               
               if(char.IsDigit(Current)){
                    var start = _position;
                    while(char.IsDigit(Current)) {                         
                         Next();
                    }

                    var lenght = _position - start;

                    var token = _text.Substring(start, lenght);
                    
                    int.TryParse(token, out var value);
                    return new SyntaxToken(SyntaxKind.NumberToken, start , token, value); 
               }

               else if(char.IsWhiteSpace(Current)){
                    var start = _position;

                    while(char.IsWhiteSpace(Current)) {
                         Next();
                    }

                    var lenght = _position - start;

                    var token = _text.Substring(start, lenght);
                    
                    int.TryParse(token, out var value);
                    return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start , token, null); 
               }

               else if(Current == '+') 
               {
                    return new SyntaxToken(SyntaxKind.PlusToken,_position++,"+",null);
               }
               else if(Current == '-') 
               {
                    return new SyntaxToken(SyntaxKind.MinusToken,_position++,"-",null);
               }
               
               else if(Current == '*') 
               {
                    return new SyntaxToken(SyntaxKind.StarToken,_position++,"*",null);
               }

               else if(Current == '/') 
               {
                    return new SyntaxToken(SyntaxKind.ForwordSlashToken,_position++,"/",null);
               }
               else if(Current == '(') 
               {
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken,_position++,")",null);
               }
               else if(Current == ')') 
               {
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken,_position++,"(",null);
               }

               return new SyntaxToken(SyntaxKind.BadToken, _position++,_text.Substring(_position -1, 1),null);
          }
     }

     class Parser{

          private readonly SyntaxToken[] _tokens;
          private int _position; 

          public Parser(string text){

               var lexer = new Lexer(text);
               var tokens = new List<SyntaxToken>();  

               SyntaxToken token;

               do{
                    token = lexer.NextToken();
                    if(token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadToken){
                         tokens.Add(token);
                    }
               } while(token.Kind == SyntaxKind.EndOfFileToken);
               _tokens = tokens.ToArray();
          }

          private SyntaxToken Peek(int offset) {
               int index = _position + offset;

               if(index >= _tokens.Length){
                    return _tokens[_tokens.Length -1];
               }
               return _tokens[index];
          }

          private SyntaxToken Current => Peek(0);
     }
}  