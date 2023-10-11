using System;
using System.Linq;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.TimerService;
using Random = UnityEngine.Random;

namespace GameCore.Entity
{
    public class BotStepController : IEntityStepController, IDisposable
    {
        private IGameMechanicsService _gameMechanicsService;
        private ITimerService _timerService;

        private float ComputerThingDelay = 1f;
        private TimerHandle _timer;

        public event Action<int> OnCompleteStep;

        public BotStepController(IGameMechanicsService gameMechanicsService, ITimerService timerService)
        {
            _gameMechanicsService = gameMechanicsService;
            _timerService = timerService;
        }
        
        public void StarStep()
        {
            int cellIndex = GetFreeCellIndex();
            
            _timer = _timerService.OnComplete(ComputerThingDelay, () =>
            {
                OnCompleteStep?.Invoke(cellIndex);
            });
        }
        

        private int GetFreeCellIndex()
        {
            var freeCells = _gameMechanicsService.GetFreeCells();
            return freeCells[Random.Range(0, freeCells.Count())];
        }

        public void Dispose()
        {
            _timer?.Break();
        }
    }
}