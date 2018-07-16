using System;

namespace SimpleAop.Mappings
{
    public class Mapping<TReturn> : Mapping<object, TReturn>
    {
        public Mapping() : base()
        {
        }

        public Mapping(Type typeofReturnObject) : base(typeofReturnObject)
        {
        }
    }

    public class Mapping : Mapping<object, object>
    {
        public Mapping() : base()
        {
        }

        public Mapping(Type typeofReturnObject) : base(typeofReturnObject)
        {
        }
    }
}