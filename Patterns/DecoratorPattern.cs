using System;

namespace Patterns
{
    class DecoratorPattern
    {
        static void Main(string[] args)
        {
            IDataSource source = new FileDataSource("somefile");
            source.Write("Data");

            DataSourceDecorator dataSource = new CompressionWrapper(source);
            Console.WriteLine(source.Read());
            dataSource.Write("Compressed");
            Console.WriteLine(source.Read());

            dataSource = new EncryptionWrapper(source);
            dataSource.Write("Encrypted");
            Console.WriteLine(source.Read());

        }
    }

    interface IDataSource
    {
        string Read();
        void Write(string data);
    }

    class FileDataSource : IDataSource
    {
        private string _filename;
        private string data;
        public FileDataSource(string filename)
        {
            _filename = filename;
        }
        public string Read()
        {
            return data;
        }

        public void Write(string data)
        {
            this.data = data;
        }
    }

    abstract class DataSourceDecorator : IDataSource
    {
        private IDataSource _source;
        public DataSourceDecorator(IDataSource source)
        {
            _source = source;
        }
        public virtual string Read()
        {
            return _source.Read();
        }

        public virtual void Write(string data)
        {
            _source.Write(data);
        }
    }

    class CompressionWrapper: DataSourceDecorator
    {
        public CompressionWrapper(IDataSource source): base(source)
        {
        }

        public override string Read()
        {
            return base.Read();
        }

        public override void Write(string data)
        {
            base.Write(base.Read()+data);
        }
    }

    class EncryptionWrapper : DataSourceDecorator
    {
        public EncryptionWrapper(IDataSource source) : base(source)
        {
        }

        public override string Read()
        {
            return base.Read();
        }

        public override void Write(string data)
        {
            base.Write(base.Read() + data);
        }
    }
}
