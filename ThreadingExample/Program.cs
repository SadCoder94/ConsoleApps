namespace ThreadingExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class Semaphore
    {
        //OG
        //private object _mutex = new object;

        //NEW
        private object _mutex = new object();
        private int _currAvail;
        public Semaphore(int capacity)
        {
            //OG

            //NEW
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));//NON-NEGEtive check

            _currAvail = capacity;
        }
        public void Wait()
        {

            //OG
            //lock (_mutex)
            //{
            //    if (_currAvail == 0) Monitor.Wait(_mutex);
            //    _currAvail--;
            //}

            //NEW
            lock (_mutex)
            {
                while (_currAvail == 0) // Use while loop to handle spurious wakeups
                {
                    Monitor.Wait(_mutex);//method releases the lock on an object and blocks the current thread until it reacquires the lock.
                    _currAvail--;
                }
            }
        }
        public void Signal()
        {
            lock (_mutex)
            {
                _currAvail++;
                Monitor.Pulse(_mutex);//sends a signal to a thread waiting in the queue for a change in the locked object’s state.
            }
        }
    }
}
