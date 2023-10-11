using System;

namespace GameCore.Entity
{
    public interface IEntityStepController : IDisposable
    {
        event Action<int> OnCompleteStep;
        void StarStep();
    }
}