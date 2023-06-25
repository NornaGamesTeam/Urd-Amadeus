using MBT;
using Urd.Character;

namespace Urd.AI
{
    public class AIVariableCharacterController : Variable<CharacterControllerNoModel>
    {
        protected override bool ValueEquals(CharacterControllerNoModel val1, CharacterControllerNoModel val2)
        {
            return val1.CharacterModel == val2.CharacterModel;
        }
    }
    [System.Serializable]
    public class AIReferenceCharacterController : VariableReference<AIVariableCharacterController, CharacterControllerNoModel>
    {
        public AIReferenceCharacterController(VarRefMode mode = VarRefMode.EnableConstant)
        {
            SetMode(mode);
        }
        
        protected override bool isConstantValid
        {
            get { return constantValue != null; }
        }

        public CharacterControllerNoModel Value
        {
            get
            {
                return (useConstant)? constantValue : this.GetVariable().Value;
            }
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                }
                else
                {
                    this.GetVariable().Value = value;
                }
            }
        }
    }
}
