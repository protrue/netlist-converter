using System.Collections.Generic;
using VerilogNetlistModel;

namespace NetlistConverter.Converter
{
    public interface IInstanceTransformer
    {
        List<Instance> TryTransform(Instance instance, TransformationContext context);
    }
}
