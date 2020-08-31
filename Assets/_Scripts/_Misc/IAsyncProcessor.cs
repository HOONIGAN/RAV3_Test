using System.Collections;

namespace PopovMaks.RAV3_Test
{
    public interface IAsyncProcessor
    {
        void StartRoutine(IEnumerator routine);
    }
}