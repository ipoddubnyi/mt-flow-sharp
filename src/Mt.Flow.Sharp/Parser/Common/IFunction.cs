
namespace Mt.Flow.Sharp.Parser.Common
{
    public interface IFunction
    {
        IValue Execute(params IValue[] args);
    }
}
