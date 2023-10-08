using System;

namespace GameCore.Entity
{
    public interface IEntityStepController
    {
        event Action<int, int> OnCompleteStep;
        void StarStep();
    }
}