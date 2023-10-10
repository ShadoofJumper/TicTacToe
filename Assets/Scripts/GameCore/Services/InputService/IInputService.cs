using System;

namespace GameCore.Services.InputService
{
    public interface IInputService
    {
        event Action<int> OnClickCell;
    }
}