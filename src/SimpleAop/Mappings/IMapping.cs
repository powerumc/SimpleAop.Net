using System;
using SimpleAop.Proxies;

namespace SimpleAop.Mappings
{
    public interface IMapping<TInput, TReturn>
    {
        IMappingReturn<TInput, TReturn> Map(TInput input);
        IMappingReturn<TInput, TReturn> Map(TInput input, IMappingReturn<TInput, TReturn> @return);
        IMappingReturn<TInput, TReturn> MapDefault();
        TReturn GetMappingValue(TInput input);
        bool ContainsKey(TInput input);
    }

    public interface IMappingReturn<TInput, TReturn> :
        IValuable<TReturn>,
        IMapping<TInput, TReturn>
    {
        IMapping<TInput, TReturn> Return(TReturn @return);
    }
    
    public sealed class MappingReturn<TInput, TReturn> : IMappingReturn<TInput, TReturn>
	{
		private IMapping<TInput, TReturn> MappingObject;

		public MappingReturn(IMapping<TInput, TReturn> mappingObject)
		{
			this.MappingObject = mappingObject;
		}

		public IMapping<TInput, TReturn> Return(TReturn @return)
		{
			this.Value = @return;

			return this.MappingObject;
		}

		public TReturn Value { get; internal set; }

		public IMappingReturn<TInput, TReturn> Map(TInput input)
		{
			this.MappingObject.Map(input, this);

			return this;
		}

		public IMappingReturn<TInput, TReturn> Map(TInput input, IMappingReturn<TInput, TReturn> @return)
		{
			return this.MappingObject.Map(input, @return);
		}

		public IMappingReturn<TInput, TReturn> MapDefault()
		{
			throw new NotSupportedException(nameof(MapDefault));
		}

		public TReturn GetMappingValue(TInput input)
		{
			throw new NotSupportedException(nameof(GetMappingValue));
		}

		public bool ContainsKey(TInput input)
		{
			throw new NotImplementedException(nameof(ContainsKey));
		}
	}
}