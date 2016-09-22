using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Helpers
{
    public class ThreadQueue : IDisposable
    {
        private readonly Thread _thread;
        private readonly Queue<Action> _actionsQueue = new Queue<Action>();
        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(false);

        private readonly object _syncRoot = new object();

        private volatile bool _disposed;
        private bool _complited;

        public ThreadQueue()
        {
            _thread = new Thread(Process);
            _thread.Start();
        }

        public void Execute(Action action)
        {
            if (_disposed)
                throw new Exception("ThreadQueue is disposed. Action can't be runned.");

            lock (_syncRoot)
            {
                _actionsQueue.Enqueue(action);
                _manualResetEvent.Set();
                _complited = false;
            }
        }

        void Process()
        {
            while (!_disposed)
            {
                _manualResetEvent.WaitOne();

                while (_actionsQueue.Count > 0)
                {
                    Action action;
                    lock (_syncRoot)
                        action = _actionsQueue.Dequeue();

                    if (action != null)
                        action();
                }

                lock (_syncRoot)
                {
                    if (_actionsQueue.Count != 0) continue;

                    _manualResetEvent.Reset();
                    _complited = true;
                }
            }
        }

        public IEnumerator WaitForComplete()
        {
            while (!_complited)
                yield return null;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            _thread.Abort();
        }
    }
}