﻿//ref: Prism 4.0
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Hdc
{
    [SuppressMessage("Microsoft.Design", "CA1001",
         Justification = "Calling the End method, which is part of the contract of using an IAsyncResult, releases the IDisposable.")]
    public class AsyncResult<T> : IAsyncResult
    {
        private AsyncCallback asyncCallback;
        private ManualResetEvent waitHandle;
        private object lockObject = new object();

        public AsyncResult(AsyncCallback asyncCallback, object asyncState)
        {
            this.asyncCallback = asyncCallback;
            this.AsyncState = asyncState;
        }

        public object AsyncState
        {
            get;
            private set;
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                lock (this.lockObject)
                {
                    if (this.waitHandle == null)
                    {
                        this.waitHandle = new ManualResetEvent(this.IsCompleted);
                    }
                }

                return this.waitHandle;
            }
        }

        public bool CompletedSynchronously
        {
            get;
            private set;
        }

        public bool IsCompleted
        {
            get;
            private set;
        }

        public T Result
        {
            get;
            private set;
        }

        public static void End(IAsyncResult asyncResult)
        {
            var localResult = asyncResult as AsyncResult<T>;

            if (!localResult.IsCompleted)
            {
                localResult.AsyncWaitHandle.WaitOne();
            }

            if (localResult.waitHandle != null)
            {
                localResult.waitHandle.Close();
            }
        }

        public void SetComplete(T result, bool completedSynchronously)
        {
            this.Result = result;

            if (completedSynchronously)
            {
                this.CompletedSynchronously = true;
                this.IsCompleted = true;
            }
            else
            {
                lock (this.lockObject)
                {
                    this.IsCompleted = true;
                    if (this.waitHandle != null)
                    {
                        this.waitHandle.Set();
                    }
                }
            }

            if (this.asyncCallback != null)
            {
                this.asyncCallback(this);
            }
        }
    }
}