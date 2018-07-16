using PS.Plot.FrameBasic.Module_Common.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_Common.Interface
{
    public interface IValidateInput
    {
        void onExtractInputValue();
        bool onValidateInputValue(out string Message);

    }

    public interface IValidateInput2
    {
        void onExtractInputValue();
        Validator onValidateInputValue();
    }

    public interface IInitialUI
    {
        void onCreateComponet();
        void onInitialUI();
    }

}
