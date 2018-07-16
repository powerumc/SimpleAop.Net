using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleAop.Mappings
{
    public class Mapping<TInput, TReturn> : IMapping<TInput, TReturn>
	{
		private readonly Type typeofReturnObject;

		protected IMappingReturn<TInput, TReturn> DefaultReturnObject { get; private set; }
		protected bool IsMappedDefault => this.DefaultReturnObject != null;
		protected IDictionary<TInput, IMappingReturn<TInput, TReturn>> Mapper { get; } = new Dictionary<TInput, IMappingReturn<TInput, TReturn>>();

		public Mapping()
			: this(typeof(MappingReturn<TInput, TReturn>))
		{
		}

		public Mapping(Type typeofReturnObject)
		{
			this.typeofReturnObject = typeofReturnObject;

			this.InitializeMapping();
		}

		protected virtual void InitializeMapping() { }


		protected virtual TReturn ReturnMappedValue(IMappingReturn<TInput, TReturn> @return)
		{
			return @return.Value;
		}

		public virtual IMappingReturn<TInput, TReturn> Map(TInput input)
		{
			var @return = (IMappingReturn<TInput, TReturn>)Activator.CreateInstance(this.typeofReturnObject, this);
			this.Mapper.Add(input, @return);

			return @return;
		}

		public IMappingReturn<TInput, TReturn> Map(TInput input, IMappingReturn<TInput, TReturn> @return)
		{
			this.Mapper.Add(input, @return);

			return @return;
		}

		public IMappingReturn<TInput, TReturn> MapDefault()
		{
			this.DefaultReturnObject = (IMappingReturn<TInput, TReturn>)Activator.CreateInstance(this.typeofReturnObject, this);

			return this.DefaultReturnObject;
		}

		public virtual TReturn GetMappingValue(TInput input)
		{
			if(input == null)
				throw new ArgumentNullException(nameof(input));

			var result = Mapper.FirstOrDefault(o => o.Key.Equals(input)).Value;

			if (result == null)
			{
				if (this.IsMappedDefault == true)
				{
					return this.ReturnMappedValue(this.DefaultReturnObject);
				}
				else
				{
					throw new KeyNotFoundException(nameof(input));
				}
			}

			return this.ReturnMappedValue(result);
		}

		public bool ContainsKey(TInput input)
		{
			return this.Mapper.ContainsKey(input);
		}
	}
}