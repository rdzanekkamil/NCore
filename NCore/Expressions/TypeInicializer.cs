using System.Linq.Expressions;
using System.Reflection;

namespace NCore.Expressions
{
    public static class TypeInitializer
    {
        public static T InitializeInstance<T>() => (T)GetInstance<TypeToIgnore>(typeof(T), null);

        public static object GetInstance(this Type type) => GetInstance<TypeToIgnore>(type, null);

        public static T GetInstanceGeneric<T>(this Type type) => (T)type.GetInstance(type);

        public static object GetInstance<TArg>(this Type type, TArg argument) 
            => GetInstance<TArg, TypeToIgnore>(type, argument, null);

        public static TResult GetInstanceGeneric<TArg, TResult>(this Type type, TArg argument) 
            => (TResult)type.GetInstance(type, argument);

        public static object GetInstance<TArg1, TArg2>(this Type type, TArg1 argument1, TArg2 argument2)
        {
            return GetInstance<TArg1, TArg2, TypeToIgnore>(
                type, argument1, argument2, null);
        }

        public static TResult GetInstanceGeneric<TArg1, TArg2, TResult>(this Type type, TArg1 argument1, TArg2 argument2) 
            => (TResult)type.GetInstance(type, argument1, argument2);

        public static object GetInstance<TArg1, TArg2, TArg3>(this Type type, TArg1 argument1, TArg2 argument2, TArg3 argument3)
            => InstanceCreationFactory<TArg1, TArg2, TArg3>
                .CreateInstanceOf(type, argument1, argument2, argument3);

        public static TResult GetInstanceGeneric<TArg1, TArg2, TArg3, TResult>(this Type type,
                                                                               TArg1 argument1,
                                                                               TArg2 argument2,
                                                                               TArg3 argument3) 
            => (TResult)type.GetInstance(argument1, argument2, argument3);

        private class TypeToIgnore
        {
        }

        private static class InstanceCreationFactory<TArg1, TArg2, TArg3>
        {
            private static readonly 
                Dictionary<Type, Func<TArg1, TArg2, TArg3, object>> 
                    _instanceCreationMethods = 
                        new Dictionary<
                            Type, Func<TArg1, TArg2, TArg3, object>>();
    
            public static object CreateInstanceOf(Type type, TArg1 arg1, TArg2 arg2, TArg3 arg3)
            {
                CacheInstanceCreationMethodIfRequired(type);
    
                return _instanceCreationMethods[type]
                    .Invoke(arg1, arg2, arg3);
            }
    
            private static void CacheInstanceCreationMethodIfRequired(Type type)
            {
                if (_instanceCreationMethods.ContainsKey(type))
                {
                    return;
                }
    
                var argumentTypes = new[] 
                {
                    typeof(TArg1), typeof(TArg2), typeof(TArg3) 
                };

                Type[] constructorArgumentTypes = argumentTypes
                    .Where(t => t != typeof(TypeToIgnore))
                    .ToArray();
    
                var constructor = type.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    CallingConventions.HasThis,
                    constructorArgumentTypes,
                    new ParameterModifier[0]);
    
                var lamdaParameterExpressions = new[]
                {
                    Expression.Parameter(typeof(TArg1), "param1"),
                    Expression.Parameter(typeof(TArg2), "param2"),
                    Expression.Parameter(typeof(TArg3), "param3")
                };

                var constructorParameterExpressions = 
                    lamdaParameterExpressions
                        .Take(constructorArgumentTypes.Length)
                        .ToArray();

                var constructorCallExpression = Expression
                    .New(constructor, constructorParameterExpressions);

                var constructorCallingLambda = Expression
                    .Lambda<Func<TArg1, TArg2, TArg3, object>>(
                        constructorCallExpression, 
                        lamdaParameterExpressions)
                    .Compile();
    
                _instanceCreationMethods[type] = constructorCallingLambda;
            }
        }
    }

}