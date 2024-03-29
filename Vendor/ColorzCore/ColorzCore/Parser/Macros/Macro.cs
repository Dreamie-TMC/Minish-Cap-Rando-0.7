﻿using System.Collections.Generic;
using ColorzCore.DataTypes;
using ColorzCore.Lexer;

namespace ColorzCore.Parser.Macros
{
    internal class Macro : IMacro
    {
        private readonly IList<Token> _body;
        private readonly Dictionary<string, int> _idToParamNum;

        public Macro(IList<Token> parameters, IList<Token> body)
        {
            _idToParamNum = new Dictionary<string, int>();
            for (var i = 0; i < parameters.Count; i++) _idToParamNum[parameters[i].Content] = i;
            this._body = body;
        }

        /***
         *   Precondition: parameters.Count = max(keys(idToParamNum))
         */
        public IEnumerable<Token> ApplyMacro(Token head, IList<IList<Token>> parameters, ImmutableStack<Closure> scopes)
        {
            foreach (var t in _body)
                if (t.Type == TokenType.IDENTIFIER && _idToParamNum.ContainsKey(t.Content))
                    foreach (var t2 in parameters[_idToParamNum[t.Content]])
                        yield return t2;
                else
                    yield return new Token(t.Type, head.Location, t.Content);
        }
    }
}
