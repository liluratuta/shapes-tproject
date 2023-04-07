using UnityEngine;

namespace ShapesGame.StaticData
{
    [CreateAssetMenu(menuName = "Game/StaticData/QuizData", fileName = "QuizData")]
    public class QuizStaticData : ScriptableObject
    {
        public string WrongColor;
        public string RightColor;
    }
}