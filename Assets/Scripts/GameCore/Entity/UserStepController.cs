using System;

namespace GameCore.Entity
{
    public class UserStepController : IEntityStepController
    {
        public event Action<int> OnCompleteStep;
        public void StarStep()
        {
            throw new NotImplementedException();
        }
    }
}