using System;
using System.Collections.Generic;
using ShapesGame.Factories;
using ShapesGame.Quiz.Server;
using ShapesGame.Services.Player;
using ShapesGame.Services.Quiz;
using ShapesGame.Services.Victory;

namespace ShapesGame.Quiz.States
{
    public class QuizStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;

        public QuizStateMachine(IPlayerProvider playerProvider,
            IUIFactory uiFactory,
            IQuizServer quizServer,
            IAnswersStorage answersStorage,
            IVictoryService victoryService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(CollectRightAnswersState)] = new CollectRightAnswersState(this, playerProvider),
                [typeof(ErrorState)] = new ErrorState(this, uiFactory),
                [typeof(FinishState)] = new FinishState(),
                [typeof(LoadAnswersState)] = new LoadAnswersState(this, quizServer),
                [typeof(QuizGameState)] = new QuizGameState(this, answersStorage, uiFactory),
                [typeof(SendAnswersState)] = new SendAnswersState(this, quizServer),
                [typeof(VictoryState)] = new VictoryState(this, victoryService)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
      
            var state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}