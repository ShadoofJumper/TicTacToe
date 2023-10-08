using System;

namespace GameCore.Entity
{
    public interface IEntityStepController
    {
        void StarStep();
        event Action<int, int> OnCompleteStep;
    }
}