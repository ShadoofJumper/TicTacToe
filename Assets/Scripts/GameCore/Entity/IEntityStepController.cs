using System;

namespace GameCore.Entity
{
    public interface IEntityStepController
    {
        event Action<int> OnCompleteStep;
        void StarStep();
    }
}