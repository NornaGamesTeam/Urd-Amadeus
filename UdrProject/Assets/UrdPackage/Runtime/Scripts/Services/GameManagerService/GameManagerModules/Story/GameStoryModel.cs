namespace Urd.Story
{
    public class GameStoryModel
    {
        public StorySteps CurrentStoryStep { get; private set; }

        public void NextStoryStep()
        {
            CurrentStoryStep++;
        }

        public void NewGame()
        {
            CurrentStoryStep = StorySteps.InitialStep;
        }
    }
}