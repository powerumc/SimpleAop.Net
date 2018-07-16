using System;
using System.Collections.Generic;
using System.Linq;
using SimpleAop.Mappings;

namespace SimpleAop.Proxies.Mappings
{
    public class LazyMapping<TInput, TReturn> : Mapping<Func<TInput, bool>, Func<TInput, TReturn>>
    {
        protected LazyMapping()
        {
        } 

        protected LazyMapping(Type typeofReturnObject) : base(typeofReturnObject)
        {
        }


        protected override void InitializeMapping() { }

        protected override Func<TInput, TReturn> ReturnMappedValue(IMappingReturn<Func<TInput, bool>, Func<TInput, TReturn>> @return)
        {
            return base.ReturnMappedValue(@return);
        }

        public virtual TReturn GetMappingValue(TInput input)
        {
            var result = Mapper.FirstOrDefault(o => o.Key(input)).Value;

            if (result == null)
            {
                if (this.IsMappedDefault == true)
                {
                    return this.ReturnMappedValue(base.DefaultReturnObject)(input);
                }
                else
                {
                    throw new KeyNotFoundException(nameof(input));
                }
            }

            return this.ReturnMappedValue(result)(input);
        }
    }

    public class LazyMapping<TReturn> : LazyMapping<object, TReturn>
    {
        protected LazyMapping()
            : base()
        {
        }

        protected LazyMapping(Type typeofReturnObject)
            : base(typeofReturnObject)
        {
        }

        protected override void InitializeMapping() { }
    }
}