﻿using System;
using System.Threading.Tasks;

namespace FailableResult.NetCore
{
    public class FailureResult<TResult, TFailure> : IFailableResult<TResult, TFailure>
    {
        public static IFailableResult<TResult, TFailure> Create(TFailure failure) => new FailureResult<TResult, TFailure>(failure);

        public static Task<IFailableResult<TResult, TFailure>> CreateAsync(TFailure failure) => Task.FromResult(Create(failure));

        public TFailure Failure { get; }

        private FailureResult(TFailure failure)
        {
            Failure = failure;
        }

        public T Handle<T>(Func<TResult, T> onSuccess, Func<TFailure, T> onFailure)
        {
            if (onFailure == null) throw new ArgumentNullException(nameof(onFailure));

            return onFailure(Failure);
        }
    }
}
