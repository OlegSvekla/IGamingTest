﻿using System.Linq.Expressions;

namespace IGamingTest.Core.Helpers;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> False<T>() => x => false;

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));
        var combined = Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            ),
            parameter
        );
        return combined;
    }
}
