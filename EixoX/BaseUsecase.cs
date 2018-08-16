using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX
{
    public abstract class BaseUsecase<T>
    {
        protected abstract void ExecuteFlow(UsecaseResult result);

        public void PreExecute(UsecaseResult result) { result.ResultType = UsecaseResultType.Success; }
        public void PostExecute(UsecaseResult result) {  }

        public UsecaseResult Execute()
        {
            UsecaseResult result = new UsecaseResult();
            try
            {
                PreExecute(result);
                if (result.ResultType != UsecaseResultType.Success)
                    return result;

                if (!RestrictionAspect<T>.Instance.Validate(this))
                {
                    result.Message = "Os campos estão inválidos";
                    result.ResultType = UsecaseResultType.Restrictions_Failed;
                    return result;
                }

                ExecuteFlow(result);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Message = ex.Message;
            }
            finally
            {
                PostExecute(result);
            }

            return result;
        }
    }
}
