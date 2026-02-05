using System;
using System.Collections.Generic;

namespace Feng.Json
{
    public class FJsonParse
    {
        private SymbolTable symbolTable = null;

        private static FJsonParse instance = null;

        public static FJsonBase Parese(string text)
        {
            return Parese(text, null, null);
        }

        public static FJsonBase Parese(string text, JsonCreateHandler JsonObjCreate, object obj)
        {
            if (instance == null)
            {
                instance = new FJsonParse();
            }
            SymbolTable symbol = Lexer.GetSymbolTable(text);
            instance.Init(JsonObjCreate, obj);
            return instance.ParseTable(symbol);
        }

        private void Init(JsonCreateHandler jsonobjcreate, object obj)
        {
            JsonCreate = jsonobjcreate;
            JsonObj = obj;
        }

        public event JsonCreateHandler JsonCreate = null;
        private object JsonObj = null;
        private void OnJsonObjCreate(FJsonBase json)
        {
            if (JsonCreate != null)
            {
                JsonCreate(json, JsonObj);
            }
        }

        public delegate void JsonCreateHandler(FJsonBase json, object obj);

        private FJsonBase ParseTable(SymbolTable table)
        {
            symbolTable = table;
            FJsonBase jsonobj = Parse_E_Rule(null);
            return jsonobj;
        }

        private FJsonBase Parse_E_Rule(FJsonBase pobj)
        {
            Token token = symbolTable.Peek();
            if (token == Token.End)
                return null;
            if (token.Type == TokenType.LBRACKET)
            {
                token = symbolTable.Pop();

                FJsonArray objres = new FJsonArray();
                token = symbolTable.Peek();
                if (token.Type != TokenType.RBRACKET)
                {
                    FJsonBase obj = Parse_T_Rule(objres);
                    objres.Add(obj);
                    token = symbolTable.Peek();
                    if (token == Token.End)
                    {
                        Error(token, 1001, "Not Finish");
                    }
                    while (token.Type == TokenType.COMMA)
                    {
                        token = symbolTable.Pop();
                        obj = Parse_T_Rule(objres);
                        objres.Add(obj);
                        token = symbolTable.Peek();
                    }
                }
                if (token.Type != TokenType.RBRACKET)
                {
                    Error(token, 1002, "Not Finish Symbol Invalid");
                }
                token = symbolTable.Pop();
                OnJsonObjCreate(objres);
                return objres;
            }
            else
            {
                return Parse_T_Rule(pobj);
            }
        }

        private FJsonBase Parse_T_Rule(FJsonBase pobj)
        {
            Token token = symbolTable.Peek();
            if (token == Token.End)
                return null;
            if (token.Type == TokenType.LBRACE)
            {
                token = symbolTable.Pop();
                FJson objres = new FJson();

                token = symbolTable.Peek();
                if (token.Type != TokenType.RBRACE)
                {
                    FJsonItem obj = Parse_F_Rule(objres);
                    objres.Add(obj);
                    token = symbolTable.Peek();
                    if (token == Token.End)
                    {
                        Error(token, 3001, "Not Finish");
                    }
                    while (token.Type == TokenType.COMMA)
                    {
                        token = symbolTable.Pop();
                        obj = Parse_F_Rule(objres);
                        objres.Add(obj);
                        token = symbolTable.Peek();
                    }
                }
                if (token.Type != TokenType.RBRACE)
                {
                    Error(token, 3002, "Not Finish");
                }
                token = symbolTable.Pop();
                OnJsonObjCreate(objres);
                return objres;
            }
            else
            {
                token = symbolTable.Peek();
                if (token.Type > 9)
                {
                    return Parse_E_Rule(pobj);
                }
                token = symbolTable.Pop();
                FJsonBase jsonObj = new FJsonValue() { Value = token.ToValue(), Parent = pobj };

                OnJsonObjCreate(jsonObj);
                return jsonObj;
            }
        }

        private FJsonItem Parse_F_Rule(FJsonBase pobj)
        {
            if (symbolTable.Peek() == Token.End)
            {
                return null;
            }
            Token token = symbolTable.Peek();
            if (token.Type != TokenType.STRING)
            {
                Error(token, 5004, "Symbol Invalid");
            }
            token = symbolTable.Pop();
            string key = token.Value;
            if (symbolTable.Peek() == Token.End)
            {
                Error(token, 5005, "Accident  Finish");
            }
            token = symbolTable.Peek();
            if (token.Type != TokenType.COLON)
            {
                Error(token, 5006, "Symbol Invalid");
            }
            token = symbolTable.Pop();
            FJsonItem obj = new FJsonItem();
            obj.Key = key;
            FJsonBase value = Parse_E_Rule(pobj);
            obj.Value = value; 
            return obj;

        }

        private void Error(Token token, int errorcode, string error)
        {
            throw new Exception("Key:【" + token.Value + "】 Row:" + token.Index + " Column:" + token.Column + " ErrorCode:" + errorcode + "." + error);
        }
    }
}
