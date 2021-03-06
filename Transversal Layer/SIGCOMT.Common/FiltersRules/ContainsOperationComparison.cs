﻿using System.Linq.Expressions;
using System.Reflection;

namespace SIGCOMT.Common.FiltersRules
{
    public class ContainsOperationComparison : BaseOperationComparison, IOperationComparison
    {
        public Expression GetOperationComparison<T>(ParameterExpression parameterExpression, string itemField, Expression expressionValue)
           where T : class
        {
            MethodInfo miBeginWith = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(GetMemberAccessLambda<T>(parameterExpression, itemField), miBeginWith, expressionValue);
        }
    }
}