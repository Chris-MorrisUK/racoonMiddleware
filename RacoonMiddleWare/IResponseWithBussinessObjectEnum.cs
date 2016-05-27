using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacoonMiddleWare
{
	public interface IResponseWithBussinessObjectEnum
	{
		void SetOutputList(IEnumerable<IPopulateFromBO> output);
	}
}
