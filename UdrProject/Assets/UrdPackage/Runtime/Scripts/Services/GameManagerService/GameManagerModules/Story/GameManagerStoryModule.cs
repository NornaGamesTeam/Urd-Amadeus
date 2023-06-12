using System;
using Urd.Story;

namespace Urd.GameManager
{
    public class GameManagerStoryModule
    {
        public StorySteps CurrentStoryStep { get; private set; }

        public event Action OnStoryStepChanged;
        
        public GameManagerStoryModule()
        {
            
        }

        public void NextStoryStep()
        {
            CurrentStoryStep++;
            OnStoryStepChanged?.Invoke();
        }
        
    }
}
